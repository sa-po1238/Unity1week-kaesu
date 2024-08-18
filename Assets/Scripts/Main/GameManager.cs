using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI remainStampCountText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] GameObject resultPanel;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance_AudioManager.TempoAdjustBGM(1.0f);
        AudioManager.instance_AudioManager.PlayBGM(SelectStage.stageNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRemainStampCount(int remainStampCount)
    {
        remainStampCountText.text = remainStampCount.ToString();
    }

    public void ShowResult(int score)
    {
        resultPanel.SetActive(true);
        scoreText.text = score.ToString();
        if (score >= 30)
        {
            resultText.text = "S";
        }
        else if (score >= 25)
        {
            resultText.text = "A";
        }
        else if (score >= 20)
        {
            resultText.text = "B";
        }
        else
        {
            resultText.text = "C";
        }
    }

}
