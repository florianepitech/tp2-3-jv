using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
    
    public void OnAvatarEditorButtonPressed()
    {
        SceneManager.LoadScene("Avatar");
    }
    

    public void OnPlayButtonPressed()
    {
        Game.gameType = GameType.HostGame;
        SceneManager.LoadScene("Game");
    }
    
    public void OnKeyboardSettingsButtonPressed()
    {
        Debug.Log("Keyboard settings button pressed");
        SceneManager.LoadScene("KeyboardSettings");
    }

    public void OnJoinGameButtonPressed()
    {
        Debug.Log("Join game button pressed");
        //unload the HomeMenu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("JoinGameMenu");
        
        
        //don't destroy the Game scene but replace the HomeMenu scene with JoinGameMenu scene
    }
    
}
