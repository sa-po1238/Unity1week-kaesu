using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBGMVolume : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        // AudioManagerスクリプトがアタッチされたオブジェクトを探す
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager != null)
        {
            // 関数を呼び出す
            audioManager.SetBGMVolume();

        }

    }
}
