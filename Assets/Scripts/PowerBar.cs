using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    private TMP_Text _text;
    private static float _power;
    private static bool start = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _power = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!start)
            return;
        _power += 0.5f;
        if (_power > 100)
        {
            _power = 0;
        }
        _text.text = "Power: " + _power;
    }
    
    public static float GetPower()
    {
        return _power;
    }
 
    public static void SetRun(bool value)
    {
        start = value;
    }
    
}
