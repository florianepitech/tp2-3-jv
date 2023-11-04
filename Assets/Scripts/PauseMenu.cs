using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var toClose = KeyboardEvent.IsEscape();
        if (toClose)
        {
            OnClickResumeButton();
        }
    }

    public void OnClickResumeButton()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("PauseMenu");
    }
    
    public void OnClickExitGameButton()
    {
        Application.Quit();   
    }
    
}
