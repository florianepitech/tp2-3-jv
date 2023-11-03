using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HomeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Main menu loaded");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Additive);
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
        //load the scene but don't switch to it yet
        
        //get the network manager from Game scene in DontDestroyOnLoad
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        if (networkManager != null)
        {
            Debug.Log("Network manager found");
            networkManager.StartHost();
            //destroy the main menu
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Network manager not found");
        }
    }
    
    public void OnKeyboardSettingsButtonPressed()
    {
        Debug.Log("Keyboard settings button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("KeyboardSettings");
    }

    public void OnJoinGameButtonPressed()
    {
        Debug.Log("Join game button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("JoinGameMenu");
    }
    
}
