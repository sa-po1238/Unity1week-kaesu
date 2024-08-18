using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject selectButtons;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject logo;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance_AudioManager.PlayBGM(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        startButton.SetActive(false);
        logo.SetActive(false);
        selectButtons.SetActive(true);
    }
}
