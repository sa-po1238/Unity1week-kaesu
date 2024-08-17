using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampChecker : MonoBehaviour
{
    private GameObject currentStamp;
    private string correctStampName = "Kitayama(Clone)";
    private List<GameObject> passedStamps = new List<GameObject>();

    void Start()
    {
        FindObjectOfType<StampSpawner>().OnSpaceKeyPressed();
    }

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
                Debug.Log("正解: 北山を取った");
                Destroy(currentStamp);
            }
            else
            {
                // 間違ったスタンプでスペースキーが押された場合
                Debug.Log("ミス: 間違いはんこを取っちゃった");
            }
        }
    }

    public void PassStamp(GameObject stamp)
    {
        if (stamp.name != correctStampName)
        {
            Debug.Log("正解: 間違いはんこ見逃せた");
        }
        else
        {
            Debug.Log("ミス: 北山見逃した");
        }
        passedStamps.Add(stamp);
    }

    public List<GameObject> GetPassedStamps()
    {
        return passedStamps;
    }
}
