using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressTextPlayer1 : MonoBehaviour
{
    // Start is called before the first frame update
    //get obstacle static list
    
    private TextMeshProUGUI textMeshProUGUI;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //get the specific TextMeshProUGUI component
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = Game.PassedObstaclesPlayer1.Value.ToString();
    }
}
