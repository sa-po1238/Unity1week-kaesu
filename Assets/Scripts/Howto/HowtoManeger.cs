using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HowtoManager : MonoBehaviour
{
    [SerializeField] GameObject howtoPanel;

    // この下あんまりわからない
    private bool isHowtoPanelActive = false;

    public bool IsHowtoPanelActive
    {
        get { return isHowtoPanelActive; }
    }

    void Start()
    {
        // 初期状態でパネルを非表示にする
        howtoPanel.SetActive(false);
        isHowtoPanelActive = false;
    }

    void Update()
    {
        // Howtoが開いているときはスペースキーの入力を無視する
        if (isHowtoPanelActive && Input.GetKeyDown(KeyCode.Space))
        {
            // スペースキーの入力を無視
            return;
        }

        // HowtoPanelの状態をデバッグログに出力
        Debug.Log($"HowtoPanel Active State: {howtoPanel.activeSelf}");
    }

    // HowtoPanelが表示されるとき（あそびかたボタンが押されたとき）
    public void HowtoPanelActive()
    {
        Debug.Log("HowtoPanelActiveが呼び出されました");
        howtoPanel.SetActive(true);
        isHowtoPanelActive = true;
    }

}
