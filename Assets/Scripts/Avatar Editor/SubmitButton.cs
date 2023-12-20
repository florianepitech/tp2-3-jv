using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    public void changeScene()
    {
        AvatarManager.currentMaterial = GameObject.Find("Sphere").GetComponent<Renderer>().material;
        AvatarManager.currentColor = GameObject.Find("Sphere").GetComponent<Renderer>().material.GetColor("_Color");
        AvatarManager.secondaryColor = GameObject.Find("Sphere").GetComponent<Renderer>().material.GetColor("_SecondaryColor");
        AvatarManager.prefabName = GameObject.Find("AvatarChangerManager").GetComponent<AvatarChanger>().prefabName;
        UnityEngine.SceneManagement.SceneManager.LoadScene("HomeMenu");
    }
}
