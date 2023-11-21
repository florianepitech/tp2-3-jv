using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    private TMP_Text _text;
    private static float _power;
    private static bool _start = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _power = 0;
    }

    void FixedUpdate()
    {
        if (!_start)
            return;
        _power += 1f;
        if (_power > 100)
        {
            _power = 0;
        }
        _text.text = "Power: " + (int)_power;
    }
    
    public static int GetPower()
    {
        return (int)_power;
    }
    
    public static void SetRun(bool value)
    {
        _start = value;
    }
    
}
