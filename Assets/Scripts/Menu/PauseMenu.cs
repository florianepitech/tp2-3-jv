using System;
using System.Collections;
using System.Collections.Generic;
using keyboard;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        IsOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        var toClose = KeyboardEvent.GetKeyUp(KeyMovement.PauseMenu);
        if (toClose && IsOpen)
        {
            Debug.Log("Close pause menu");
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("PauseMenu");
        }
    }

    private void OnDestroy()
    {
        IsOpen = false;
    }

    public void OnClickResumeButton()
    {
        IsOpen = false;
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("PauseMenu");
    }
    
    public void OnClickExitGameButton()
    {
        Application.Quit();   
    }
    
}
