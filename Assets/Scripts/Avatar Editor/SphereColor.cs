using UnityEngine;

public class SphereColor : MonoBehaviour
{
    public FlexibleColorPicker colorPicker;
    public FlexibleColorPicker colorPicker2;
    // Update is called once per frame
    void FixedUpdate()
    {
        //set the color of the sphere to the color of the color picker
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", colorPicker.color);
        rend.material.SetColor("_SecondaryColor", colorPicker2.color);
        //I want the half of the sphere to be white and the other half to be the color of the color picker
        
        
    }
}
