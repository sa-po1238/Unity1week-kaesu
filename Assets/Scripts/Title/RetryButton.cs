using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRetryButton()
    {
        AudioManager.instance_AudioManager.PlaySE(0);
        SceneManager.LoadScene("Main");
    }
}
