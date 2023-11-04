using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaibowText : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    void Start()
    {
        StartCoroutine(RainbowText());
    }
    
    IEnumerator RainbowText()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        while (true)
        {
            //random color
            textMeshProUGUI.color = new Color(Random.value, Random.value, Random.value);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
