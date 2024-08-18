using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;

    private bool isMenuPanelActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickMenuButton()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        if (isMenuPanelActive)
        {
            menuPanel.SetActive(false);
            isMenuPanelActive = false;
        }
        else
        {
            menuPanel.SetActive(true);
            isMenuPanelActive = true;
        }
    }
}
