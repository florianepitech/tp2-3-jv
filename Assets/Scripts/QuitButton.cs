using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private static readonly Color DefaultColor = Color.white;
    
    private static readonly Color HoverColor = Color.red;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = DefaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = HoverColor;
    }
    
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = DefaultColor;
    }
    
    private void OnMouseDown()
    {
        Application.Quit();
    }
    
}
