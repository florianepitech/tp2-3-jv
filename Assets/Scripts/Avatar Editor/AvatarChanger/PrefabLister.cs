using UnityEngine;
using System.Collections.Generic;

public class PrefabLister : MonoBehaviour
{
    public string folderPath = "Prefabs/Avatar"; // Set your folder path inside Resources

    public List<GameObject> GetPrefabsFromFolder()
    {
        List<GameObject> prefabs = new List<GameObject>();

        // Load all GameObjects (prefabs) from the specified folder in Resources
        Object[] loadedPrefabs = Resources.LoadAll(folderPath, typeof(GameObject));
        foreach (Object obj in loadedPrefabs)
        {
            if (obj is GameObject prefab)
            {
                prefabs.Add(prefab);
            }
        }
        
        //sort the list alphabetically
        prefabs.Sort((x, y) => string.Compare(x.name, y.name));
        return prefabs;
    }
}