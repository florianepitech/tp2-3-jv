using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour
{
    private string _hostInputFieldValue = "";
    private string _portInputFieldValue = "";
    
    // Start is called before the first frame update
    void Start()
    {
        //if the HomeMenu is still load unload it
        if (GameObject.Find("HomeMenu") != null)
        {
            Debug.Log("Unloading HomeMenu");
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("HomeMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
    public void OnPlayButtonPressed()
    {
        // Get the text input
        Debug.Log("Ask to join " + _hostInputFieldValue + ":" + _portInputFieldValue);
        
        //get the network manager from Game scene in DontDestroyOnLoad
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        if (networkManager != null)
        {
            Debug.Log("Network manager found");
            networkManager.StartClient();
        }
        else
        {
            Debug.Log("Network manager not found");
        }
        
        
    }

    public void OnBackButtonPressed()
    {
        Debug.Log("Keyboard settings back button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeMenu");
    }
    
    public void OnHostInputFieldChanged(String value)
    {
        Debug.Log("Host input field changed to " + value);
        _hostInputFieldValue = value;
    }
    
    public void OnPortInputFieldChanged(String value)
    {
        Debug.Log("Port input field changed to " + value);
        _portInputFieldValue = value;
    }
    
}
