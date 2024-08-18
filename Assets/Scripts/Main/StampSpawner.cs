using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StampSpawner : MonoBehaviour
{
    public GameObject[] stampPrefabs;  // スタンプのプレハブ配列
    public Transform spawnPoint;  // スタンプの生成位置
    private float initialSpawnInterval = 1.0f;  // 最初の生成間隔
    private float spawnInterval;
    private int totalStamps = 45;
    private int correctStampCount = 30;
    private int currentStampCount = 0;
    private int remainStampCount = 0;
    private int score = 0;

    private List<GameObject> stampQueue = new List<GameObject>();
    private GameObject currentStamp;  // 現在表示されているスタンプ
    private bool isSpaceKeyPressed = false;
    private StampChecker stampChecker;
    private GameManager gameManager;

    [SerializeField] GameObject speedUpText;

    private float tempo = 0.2f;

    void Start()
    {
        if (SelectStage.stageNumber == 0)
        {
            initialSpawnInterval = 1.0f;
        }
        else if (SelectStage.stageNumber == 1)
        {
            initialSpawnInterval = 0.888f;
        }
        else if (SelectStage.stageNumber == 2)
        {
            initialSpawnInterval = 0.80f;
        }
        Debug.Log(initialSpawnInterval.ToString());
        spawnInterval = initialSpawnInterval;
        remainStampCount = totalStamps;

        stampChecker = FindObjectOfType<StampChecker>();
        gameManager = FindObjectOfType<GameManager>();
        
        stampChecker.SetScore(correctStampCount);
        PrepareStampQueue();

        // 2小節後にスタンプ生成を開始
        if (SelectStage.stageNumber == 0)
        {
            StartCoroutine(StartSpawn(4.0f));
        }
        else if (SelectStage.stageNumber == 1)
        {
            StartCoroutine(StartSpawn(3.555f));
        }
        else if (SelectStage.stageNumber == 2)
        {
            StartCoroutine(StartSpawn(3.2f));
        }
    }

    IEnumerator StartSpawn(float waitTime)
    {
        Debug.Log("スタンプ生成を開始します");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("スタンプ生成を開始します");
        
        // スタンプ生成のコルーチンを開始
        StartCoroutine(SpawnStamps());
    }

    // スタンプの生成順番を決定
    void PrepareStampQueue()
    {
        // ｢北山｣をcorrectStampCount個生成
        for (int i = 0; i < correctStampCount; i++)
        {
            stampQueue.Add(stampPrefabs[0]);  // ｢北山｣は配列の最初にあると仮定
        }

        // ｢比山｣を8個追加（配列の1番目にあると仮定）
        for (int i = 0; i < 8; i++)
        {
            stampQueue.Add(stampPrefabs[1]);
        }

        // ｢北出｣を4個追加（配列の2番目にあると仮定）
        for (int i = 0; i < 4; i++)
        {
            stampQueue.Add(stampPrefabs[2]);
        }

        // 残りの3つのスタンプを1つずつ追加（配列の3番目以降にあると仮定）
        for (int i = 3; i < stampPrefabs.Length; i++)
        {
            stampQueue.Add(stampPrefabs[i]);
        }

        // リストをランダムに並べ替え
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

            if (currentStampCount == 20)
            {
                speedUpText.SetActive(true);

                // テンポ調整
                AudioManager.instance_AudioManager.TempoAdjustBGM(1.25f);
                
                spawnInterval *= 0.8f;
            }

            if (currentStampCount == 22)
            {
                speedUpText.SetActive(false);
            }

            if (currentStampCount == 30)
            {
                speedUpText.SetActive(true);

                // テンポ調整
                AudioManager.instance_AudioManager.TempoAdjustBGM(1.5f);
                
                spawnInterval *= 0.8f;
            }

            if (currentStampCount == 32)
            {
                speedUpText.SetActive(false);
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
