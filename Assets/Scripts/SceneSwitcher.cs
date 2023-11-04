using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //load the network manager and switch to the menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
