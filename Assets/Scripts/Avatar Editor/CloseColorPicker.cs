using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseColorPicker : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void closeColorPicker()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
