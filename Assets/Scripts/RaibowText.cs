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
        //check if the textmeshprogui is null
        if (textMeshProUGUI == null)
        {
            Debug.LogError("TextMeshProUGUI is null");
            yield break;
        }
        while (true)
        {
            //random color
            textMeshProUGUI.color = new Color(Random.value, Random.value, Random.value, 1f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
