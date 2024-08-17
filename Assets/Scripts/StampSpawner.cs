using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StampSpawner : MonoBehaviour
{
    public GameObject[] stampPrefabs;  // スタンプのプレハブ配列
    public Transform spawnPoint;  // スタンプの生成位置
    public float initialSpawnInterval = 0.5f;  // 最初の生成間隔
    private float spawnInterval;
    private int totalStamps = 10;
    private int correctStampCount = 5;
    private int currentStampCount = 0;
    private int remainStampCount = 0;

    private List<GameObject> stampQueue = new List<GameObject>();
    private GameObject currentStamp;  // 現在表示されているスタンプ
    private bool isSpaceKeyPressed = false;
    private StampChecker stampChecker;

    [SerializeField] TextMeshProUGUI remainStampCountText;

    void Start()
    {
        spawnInterval = initialSpawnInterval;
        remainStampCount = totalStamps;
        stampChecker = FindObjectOfType<StampChecker>();
        PrepareStampQueue();
        StartCoroutine(SpawnStamps());
    }

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
            remainStampCountText.text = remainStampCount.ToString();

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

            // 次のスタンプのためにスペースキーのフラグをリセット
            isSpaceKeyPressed = false;

            //spawnInterval *= 0.95f;  // 徐々に速くする
        }
    }

    void SpawnStamp()
    {
        currentStamp = Instantiate(stampQueue[currentStampCount], spawnPoint.position, Quaternion.identity, spawnPoint);

        // StampChecker に現在のスタンプを設定
        stampChecker.SetCurrentStamp(currentStamp);
        Debug.Log("スタンプを生成しました: " + currentStamp.name);
    }

    public void OnSpaceKeyPressed()
    {
        isSpaceKeyPressed = true;
    }
}
