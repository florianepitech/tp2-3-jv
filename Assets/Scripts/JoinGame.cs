using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour
{
    private string _hostInputFieldValue = "";
    private string _portInputFieldValue = "";
    
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
        Debug.Log("Ask to join " + _hostInputFieldValue + ":" + _portInputFieldValue);
        
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
