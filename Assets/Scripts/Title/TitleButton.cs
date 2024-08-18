using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickTitleButton()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        SceneManager.LoadScene("Title");
    }
}
