using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampChecker : MonoBehaviour
{
    private GameObject currentStamp;
    private string correctStampName = "Kitayama(Clone)";
    private List<GameObject> passedStamps = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckStamp();
        }
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
                Debug.Log("ミス: 正しいスタンプでスペースキーが押されました！");
            }
            else
            {
                // 間違ったスタンプでスペースキーが押された場合
                Debug.Log("正解: 間違ったスタンプでスペースキーが押されました！");
                Destroy(currentStamp);
            }
        }
    }

    public void PassStamp(GameObject stamp)
    {
        passedStamps.Add(stamp);
    }

    public List<GameObject> GetPassedStamps()
    {
        return passedStamps;
    }
}
