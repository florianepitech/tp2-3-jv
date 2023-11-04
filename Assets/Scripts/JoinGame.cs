using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;
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
        
        if (_hostInputFieldValue == "")
        {
            Debug.Log("Host input field is empty");
            return;
        }
        // Get the network manager
        var networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        
        // Set the network address
        networkManager.GetComponent<UnityTransport>().ConnectionData.Address = _hostInputFieldValue;
        networkManager.StartClient();
        //unloads the JoinGameMenu scene
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("JoinGameMenu");
        


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
