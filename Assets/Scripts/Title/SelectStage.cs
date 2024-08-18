using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
   public static int stageNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStageButton(int selectNumber)
    {
        stageNumber = selectNumber;

        // stageNumber:1 Easy, 2 Normal, 3 Hard
        switch (stageNumber)
        {
            case 1:
                Debug.Log("Easyステージを選択");
                break;
            case 2:
                Debug.Log("Normalステージを選択");
                break;
            case 3:
                Debug.Log("Hardステージを選択");
                break;
        }

        SceneManager.LoadScene("Main");

    }
}
