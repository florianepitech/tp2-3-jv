using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    TMP_Text _text;
    private static int _power;
    
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _power = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _power++;
        if (_power > 100)
        {
            _power = 0;
        }
        _text.text = "Power: " + _power;
    }
    
    public static int GetPower()
    {
        return _power;
    }
    
}
