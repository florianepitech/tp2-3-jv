using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;
public class JoinGame : MonoBehaviour
{
    public static string HostInputFieldValue = "";
    public static string PortInputFieldValue = "";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
    public void OnPlayButtonPressed()
    {
        // Get the text input
        Debug.Log("Ask to join " + HostInputFieldValue + ":" + PortInputFieldValue);
        
        if (HostInputFieldValue == "")
        {
            Debug.Log("Host input field is empty");
            return;
        }
        // Load Game scene
        Game.gameType = GameType.JoinGame;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnBackButtonPressed()
    {
        Debug.Log("Keyboard settings back button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeMenu");
    }
    
    public void OnHostInputFieldChanged(String value)
    {
        Debug.Log("Host input field changed to " + value);
        HostInputFieldValue = value;
    }
    
    public void OnPortInputFieldChanged(String value)
    {
        Debug.Log("Port input field changed to " + value);
        PortInputFieldValue = value;
    }
    
}
