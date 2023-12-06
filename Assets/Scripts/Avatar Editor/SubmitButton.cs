using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    public void changeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeMenu");
    }
}
