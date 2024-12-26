using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HowtoButton : MonoBehaviour
{
    [SerializeField] GameObject howtoPanel;
    [SerializeField] GameObject howtoButton;
    [SerializeField] HowtoManager howtoManager;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnClickHowtoButton()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        howtoManager.HowtoPanelActive();
    }
}
