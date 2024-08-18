using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampSpawner : MonoBehaviour
{
    public GameObject[] stampPrefabs;  // スタンプのプレハブ配列
    public Transform spawnPoint;  // スタンプの生成位置
    public float initialSpawnInterval = 0.32f;  // 最初の生成間隔
    private float spawnInterval;
    private int totalStamps = 10;
    private int correctStampCount = 5;
    private int currentStampCount = 0;
    private int remainStampCount = 0;
    private int score = 0;

    private List<GameObject> stampQueue = new List<GameObject>();
    private GameObject currentStamp;  // 現在表示されているスタンプ
    private bool isSpaceKeyPressed = false;
    private StampChecker stampChecker;
    private GameManager gameManager;

    private float tempo = 0.2f;

    void Start()
    {
        spawnInterval = initialSpawnInterval;
        remainStampCount = totalStamps;

        stampChecker = FindObjectOfType<StampChecker>();
        gameManager = FindObjectOfType<GameManager>();
        
        stampChecker.SetScore(correctStampCount);
        PrepareStampQueue();
        StartCoroutine(SpawnStamps());
    }

    // スタンプの生成順番を決定
    void PrepareStampQueue()
    {
        // ｢北山｣をcorrectStampCount個生成
        for (int i = 0; i < correctStampCount; i++)
        {
            // ｢北山｣は配列の一個目にしておく
            stampQueue.Add(stampPrefabs[0]);
        }

        // それ以外のスタンプをランダムに生成
        for (int i = 0; i < totalStamps - correctStampCount; i++)
        {
            int randomIndex = Random.Range(1, stampPrefabs.Length);
            stampQueue.Add(stampPrefabs[randomIndex]);
        }

        // リストをランダム並べ替え
        for (int i = 0; i < stampQueue.Count; i++)
        {
            GameObject temp = stampQueue[i];
            int randomIndex = Random.Range(i, stampQueue.Count);
            stampQueue[i] = stampQueue[randomIndex];
            stampQueue[randomIndex] = temp;
        }
    }

    IEnumerator SpawnStamps()
    {
        while (currentStampCount < totalStamps)
        {
            SpawnStamp();
            currentStampCount++;
            remainStampCount--;

            // 残りスタンプ数を更新
            gameManager.ShowRemainStampCount(remainStampCount);

            // スペースキーが押されていないかを確認
            yield return new WaitForSeconds(spawnInterval);
            if (!isSpaceKeyPressed && currentStamp != null)
            {
                stampChecker.PassStamp(currentStamp);
            }

            // 次のスタンプ生成前に現在のスタンプを破壊
            if (currentStamp != null)
            {
                Destroy(currentStamp);
            }

            if (currentStampCount == 5)
            {
                // テンポ調整
                AudioManager.instance_AudioManager.TempoAdjustBGM(0.2f);
                spawnInterval *= 0.8f;
            }

            // 次のスタンプのためにスペースキーのフラグをリセット
            isSpaceKeyPressed = false;
        }

        // スタンプ生成が終了したら結果を表示
        gameManager.ShowResult(stampChecker.score);
        AudioManager.instance_AudioManager.StopBGM();
        AudioManager.instance_AudioManager.PlaySE(3);
    }

    void SpawnStamp()
    {
        currentStamp = Instantiate(stampQueue[currentStampCount], spawnPoint.position, Quaternion.identity, spawnPoint);

        // StampChecker に現在のスタンプを設定
        stampChecker.SetCurrentStamp(currentStamp);
        Debug.Log("スタンプを生成しました: " + currentStamp.name);

        AudioManager.instance_AudioManager.PlaySE(2);
    }

    public void OnSpaceKeyPressed()
    {
        isSpaceKeyPressed = true;
    }
}
