using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject colorPicker;
    public void displayColorPicker()
    {
        colorPicker.SetActive(true);
    }
}
