using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    public static Material currentMaterial;
    public List<Material> avatarMaterials = new List<Material>();
    MaterialLister materialLister;

    private void Start()
    {
        materialLister = GetComponent<MaterialLister>();
        avatarMaterials = materialLister.GetMaterialsFromFolder();
    }
    
}
