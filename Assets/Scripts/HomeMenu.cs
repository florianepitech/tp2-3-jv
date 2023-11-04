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
        Debug.Log("Main menu loaded");
        //if Game scene is loaded not reload it
        if (GameObject.Find("NetworkManager") == null)
        {
            Debug.Log("Loading Game scene");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game",
                UnityEngine.SceneManagement.LoadSceneMode.Additive);
            
        }
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
            //destroy the main menu
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            networkManager.StartHost();
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
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene("JoinGameMenu", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        
        //don't destroy the Game scene but replace the HomeMenu scene with JoinGameMenu scene
    }
    
}
