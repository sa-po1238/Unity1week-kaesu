using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject menuButton;
    [SerializeField] Sprite menuButtonSprite;
    [SerializeField] Sprite backButtonSprite;

    private bool isMenuPanelActive = false;

    public bool IsMenuPanelActive
    {
        get { return isMenuPanelActive; }
    }

    void Start()
    {
        
    }

    void Update()
    {
        // メニューが開いているときはスペースキーの入力を無視する
        if (isMenuPanelActive && Input.GetKeyDown(KeyCode.Space))
        {
            // スペースキーの入力を無視
            return;
        }
    }

    public void OnClickMenuButton()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        if (isMenuPanelActive)
        {
            GetComponent<Image>().sprite = menuButtonSprite;
            menuPanel.SetActive(false);
            isMenuPanelActive = false;
        }
        else
        {
            menuPanel.SetActive(true);
            GetComponent<Image>().sprite = backButtonSprite;
            isMenuPanelActive = true;
        }

        // ボタンからフォーカスを外す
        EventSystem.current.SetSelectedGameObject(null);
    }
}
