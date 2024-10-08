using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampChecker : MonoBehaviour
{
    private GameObject currentStamp;
    private string correctStampName = "Kitayama(Clone)";
    private List<GameObject> gettedStamps = new List<GameObject>();

    public int score = 0;

    private StampSpawner stampSpawner;
    private MenuButton menuButton;

    void Start()
    {
        stampSpawner = FindObjectOfType<StampSpawner>();
        menuButton = FindObjectOfType<MenuButton>();
    }

    void Update()
    {
        // メニューが開いていない場合のみスペースキーの入力を受け付ける
        if (!menuButton.IsMenuPanelActive && Input.GetKeyDown(KeyCode.Space))
        {
            CheckStamp();
        }
    }

    public void SetScore(int correctStampCount)
    {
        score = correctStampCount;
    }

    public void SetCurrentStamp(GameObject stamp)
    {
        currentStamp = stamp;
    }

    void CheckStamp()
    {
        if (currentStamp != null)
        {
            if (currentStamp.name == correctStampName)
            {
                // 正しいスタンプでスペースキーが押された場合
                Debug.Log("正解: 正しいスタンプでスペースキーが押されました！");
            }
            else
            {
                // 間違ったスタンプでスペースキーが押された場合
                Debug.Log("ミス: 間違ったスタンプでスペースキーが押されました！");
                score--;
            }
            gettedStamps.Add(currentStamp);
            Destroy(currentStamp);
            
            stampSpawner.OnSpaceKeyPressed();
            AudioManager.instance_AudioManager.PlaySE(1);
        }
    }

    public void PassStamp(GameObject stamp)
    {
        if (stamp != null)
        {
            if (stamp.name != correctStampName)
            {
                Debug.Log("正解: 間違ったスタンプが見逃されました！");
            }
            else
            {
                Debug.Log("ミス: 正しいスタンプが見逃されました！");
                score--;
            }
        }
    }

    public List<GameObject> GetgettedStamps()
    {
        return gettedStamps;
    }
}
