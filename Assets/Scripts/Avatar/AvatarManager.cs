using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    public static Material currentMaterial;
    public static Color currentColor;
    public static Color secondaryColor;
    public static string prefabName;
    public List<Material> avatarMaterials = new List<Material>();
    public List<GameObject> avatarPrefabs = new List<GameObject>();
    MaterialLister materialLister;
    PrefabLister prefabLister;
   

    private void Start()
    {
        materialLister = GetComponent<MaterialLister>();
        avatarMaterials = materialLister.GetMaterialsFromFolder();
        prefabLister = GetComponent<PrefabLister>();
        avatarPrefabs = prefabLister.GetPrefabsFromFolder();
        if (currentMaterial != null)
        {
           GameObject sphere = GameObject.Find("Sphere");
           //get the mesh renderer and mesh filter in avatarPrefabs with the same name as prefabName
           foreach (var prefab in avatarPrefabs)
           {
               if (prefab.name == prefabName)
               {
                   MeshRenderer prefabMeshRenderer = prefab.GetComponent<MeshRenderer>();
                   MeshFilter prefabMeshFilter = prefab.GetComponent<MeshFilter>();

                   if (prefabMeshRenderer != null && prefabMeshFilter != null)
                   {
                       sphere.GetComponent<MeshFilter>().mesh = prefabMeshFilter.sharedMesh;
                       sphere.GetComponent<MeshRenderer>().materials = prefabMeshRenderer.sharedMaterials;
                       break;
                   }
               }
           }
           sphere.GetComponent<Renderer>().material = currentMaterial;
           Debug.Log("currentColor: " + currentColor);
           sphere.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
           sphere.GetComponent<Renderer>().material.SetColor("_SecondaryColor", secondaryColor);
           //get mesh renderer and mesh filter from sphere
           
           
        }
    }
    
}
