using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HowtoButton : MonoBehaviour
{
    [SerializeField] GameObject howtoPanel;
    [SerializeField] GameObject howtoButton;
    [SerializeField] GameObject howtoCloseButton;

    private bool isHowtoPanelActive = false;

    public bool IsHowtoPanelActive
    {
        get { return isHowtoPanelActive; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Howtoが開いているときはスペースキーの入力を無視する
        if (isHowtoPanelActive && Input.GetKeyDown(KeyCode.Space))
        {
            // スペースキーの入力を無視
            return;
        }
    }

    public void OnClickHowtoButton()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        if (!isHowtoPanelActive)
        {
            SetActiveHowto(true);
        }

        OnDisable();
    }

    public void OnClickHowtoCloseButton()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        if (isHowtoPanelActive)
        {
            SetActiveHowto(false);
        }

        OnDisable();
    }

    private void OnDisable()
    {
        // オブジェクトが非アクティブになったときにフォーカスを外す
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void SetActiveHowto(bool isActive)
    {
        howtoPanel.SetActive(isActive);
        howtoCloseButton.SetActive(isActive);
        isHowtoPanelActive = isActive;
    }
}
