using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    public static Material currentMaterial;
    public static Color currentColor;
    public static Color secondaryColor;
    public List<Material> avatarMaterials = new List<Material>();
    MaterialLister materialLister;

    private void Start()
    {
        materialLister = GetComponent<MaterialLister>();
        avatarMaterials = materialLister.GetMaterialsFromFolder();
        if (currentMaterial != null)
        {
           GameObject sphere = GameObject.Find("Sphere");
           sphere.GetComponent<Renderer>().material = currentMaterial;
           Debug.Log("currentColor: " + currentColor);
           sphere.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
           sphere.GetComponent<Renderer>().material.SetColor("_SecondaryColor", secondaryColor);
        }
    }
    
}
