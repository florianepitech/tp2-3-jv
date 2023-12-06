using UnityEngine;
using System.Collections.Generic;

public class MaterialLister : MonoBehaviour
{
    public string folderPath = "Materials/Avatar"; // Set your folder path inside Resources

    public List<Material> GetMaterialsFromFolder()
    {
        List<Material> materials = new List<Material>();

        // Load all Materials from the specified folder in Resources
        Object[] loadedMaterials = Resources.LoadAll(folderPath, typeof(Material));
        foreach (Object obj in loadedMaterials)
        {
            if (obj is Material material)
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
            Debug.Log("Found Material: " + mat.name);
        }
    }
}