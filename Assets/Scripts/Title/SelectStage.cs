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

        // stageNumber:0 Easy, 1 Normal, 2 Hard
        switch (stageNumber)
        {
            case 0:
                Debug.Log("Easyステージを選択");
                break;
            case 1:
                Debug.Log("Normalステージを選択");
                break;
            case 2:
                Debug.Log("Hardステージを選択");
                break;
        }

        SceneManager.LoadScene("Main");

    }
}
