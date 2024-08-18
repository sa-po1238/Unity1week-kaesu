using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI remainStampCountText;
    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
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
        scoreText.text = score.ToString();
    }

}
