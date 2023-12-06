using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class MaterialLister : MonoBehaviour
{
    public string folderPath = "Materials/Avatar"; // Set your folder path here

    public List<Material> GetMaterialsFromFolder()
    {
        List<Material> materials = new List<Material>();

        // Get all asset paths in the folder and subfolders
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + folderPath, "*.*", SearchOption.AllDirectories);
        foreach (string fileName in fileEntries)
        {
            string assetPath = "Assets" + fileName.Replace(Application.dataPath, "").Replace('\\', '/');
            Material material = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
            
            if (material != null)
            {
                materials.Add(material);
            }
        }

        return materials;
    }

    void Start()
    {
        List<Material> materialsInFolder = GetMaterialsFromFolder();
        foreach (Material mat in materialsInFolder)
        {
            Debug.Log("Find Material: " + mat.name);
        }
    }
}