using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Main menu loaded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnQuitButtonPressed()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }

    public void OnPlayButtonPressed()
    {
        Debug.Log("Play button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
    public void OnKeyboardSettingsButtonPressed()
    {
        Debug.Log("Keyboard settings button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("KeyboardSettings");
    }
    
}
