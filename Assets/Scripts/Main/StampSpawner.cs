using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject gaugePrefab;    // ゲージのプレハブ

    private GameObject currentGauge;    // 現在表示されているゲージ
    private Image gaugeFillImage;       // ゲージのFill Image

    private float gaugeTimer = 0.0f;
    

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

    void Update()
    {
        // ゲージの更新
        if (currentGauge != null && gaugeFillImage != null)
        {
            gaugeTimer += Time.deltaTime;
            
            float fillAmount = 1f - (gaugeTimer / spawnInterval);
            gaugeFillImage.fillAmount = Mathf.Clamp01(fillAmount);

            if (gaugeTimer >= spawnInterval)
            {
                gaugeTimer = 0.0f;
                Destroy(currentGauge);
                currentGauge = null;
            }
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
                currentStamp = null;
            }

            if (currentGauge != null)
            {
                Destroy(currentGauge);
                currentGauge = null;
            }

            if (currentStampCount == 20)
            {
                speedUpText.SetActive(true);
                AudioManager.instance_AudioManager.PlaySE(4);

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
                AudioManager.instance_AudioManager.PlaySE(4);

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
        

        // 円形のゲージを生成し、スタンプの子オブジェクトとして設定
        currentGauge = Instantiate(gaugePrefab, currentStamp.transform.position, Quaternion.Euler(0, 180, 0), currentStamp.transform);
        gaugeFillImage = currentGauge.GetComponentInChildren<Image>();
        if (gaugeFillImage != null)
        {
            gaugeFillImage.fillAmount = 1.0f;   // ゲージをフルに設定
        }

        gaugeTimer = 0.0f;

        AudioManager.instance_AudioManager.PlaySE(2);
    }

    public void OnSpaceKeyPressed()
    {
        isSpaceKeyPressed = true;
    }
}
