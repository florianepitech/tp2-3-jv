using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarChanger : MonoBehaviour
{
    public List<GameObject> avatarPrefabs = new List<GameObject>();
    private PrefabLister prefabLister;
    private int currentAvatarIndex = 0;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    public Button nextButton;
    public Button previousButton;
    public string prefabName;

    void Start()
    {
        prefabLister = GetComponent<PrefabLister>();
        avatarPrefabs = prefabLister.GetPrefabsFromFolder();
        Debug.Log("Found " + avatarPrefabs.Count + " avatars");
        
        if (nextButton != null)
            nextButton.onClick.AddListener(ChangeToNextAvatar);

        if (previousButton != null)
            previousButton.onClick.AddListener(ChangeToPreviousAvatar);
    }
    
    void ChangeToNextAvatar()
    {
        currentAvatarIndex = (currentAvatarIndex + 1) % avatarPrefabs.Count;
        ChangeAvatar();
    }

    void ChangeToPreviousAvatar()
    {
        if (currentAvatarIndex == 0)
            currentAvatarIndex = avatarPrefabs.Count - 1;
        else
            currentAvatarIndex--;

        ChangeAvatar();
    }

    void ChangeAvatar()
    {
        GameObject prefab = avatarPrefabs[currentAvatarIndex];
        MeshFilter prefabMeshFilter = prefab.GetComponent<MeshFilter>();
        MeshRenderer prefabMeshRenderer = prefab.GetComponent<MeshRenderer>();
        
        GameObject sphere = GameObject.Find("Sphere");
        MeshFilter sphereMeshFilter = sphere.GetComponent<MeshFilter>();
        MeshRenderer sphereMeshRenderer = sphere.GetComponent<MeshRenderer>();

        if (sphereMeshFilter != null && sphereMeshRenderer != null && prefabMeshFilter != null && prefabMeshRenderer != null)
        {
            sphereMeshFilter.mesh = prefabMeshFilter.sharedMesh;
            sphereMeshRenderer.materials = prefabMeshRenderer.sharedMaterials;
        }
        prefabName = prefab.name;
        currentAvatarIndex = (currentAvatarIndex + 1) % avatarPrefabs.Count;
    }
}