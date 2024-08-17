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

    void Start()
    {
        spawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnStamps());
    }

    IEnumerator SpawnStamps()
    {
        while (currentStampCount < totalStamps)
        {
            SpawnStamp();
            currentStampCount++;
            yield return new WaitForSeconds(spawnInterval);
            //spawnInterval *= 0.95f;  // 徐々に速くする
        }
    }

    void SpawnStamp()
    {
        if (currentStamp != null)
        {
            Destroy(currentStamp);
        }

        int randomIndex = Random.Range(0, stampPrefabs.Length);
        currentStamp = Instantiate(stampPrefabs[randomIndex], spawnPoint.position, Quaternion.identity, spawnPoint);
        Debug.Log("スタンプを生成しました！");

        StampChecker stampChecker = FindObjectOfType<StampChecker>();
        if (stampChecker != null)
        {
            stampChecker.SetCurrentStamp(currentStamp);
        }
        else
        {
            Debug.LogError("StampChecker が見つかりませんでした！");
        }
    }
}
