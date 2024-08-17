using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampSpawner : MonoBehaviour
{
    public GameObject[] stampPrefabs;  // スタンプのプレハブ配列
    public Transform spawnPoint;  // スタンプの生成位置
    public float initialSpawnInterval = 1.0f;  // 最初の生成間隔
    private float spawnInterval;
    private int totalStamps = 30;
    private int currentStampCount = 0;
    
    private GameObject currentStamp;
    private bool isSpaceKeyPressed = false;
    private StampChecker stampChecker;


    void Start()
    {
        spawnInterval = initialSpawnInterval;
        stampChecker = FindObjectOfType<StampChecker>();
        StartCoroutine(SpawnStamps());
    }

    IEnumerator SpawnStamps()
    {
        while (currentStampCount < totalStamps)
        {
            SpawnStamp();
            currentStampCount++;

            yield return new WaitForSeconds(spawnInterval);
            if (!isSpaceKeyPressed)
            {
                stampChecker.PassStamp(currentStamp);
            }

            // 次のはんこ生成前に現在のはんこを削除
            Destroy(currentStamp);

            // フラグリセット
            isSpaceKeyPressed = false;

            // 生成間隔を短くする
            //spawnInterval *= 0.95f;
        }
    }

    void SpawnStamp()
    {
        int randomIndex = Random.Range(0, stampPrefabs.Length);
        currentStamp = Instantiate(stampPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        if (stampChecker != null)
        {
            stampChecker.SetCurrentStamp(currentStamp);
        }
        else
        {
            Debug.LogError("StampChecker が見つかりませんでした！");
        }
    }

    public void OnSpaceKeyPressed()
    {
        isSpaceKeyPressed = true;
    }
}
