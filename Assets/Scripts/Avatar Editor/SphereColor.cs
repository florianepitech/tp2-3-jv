using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColor : MonoBehaviour
{
    FlexibleColorPicker colorPicker;
    void Start()
    {
        colorPicker = GameObject.Find("ColorPicker").GetComponent<FlexibleColorPicker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set the color of the sphere to the color of the color picker
        GetComponent<Renderer>().material.color = colorPicker.color;
        //I want the half of the sphere to be white and the other half to be the color of the color picker
        
        
    }
}
