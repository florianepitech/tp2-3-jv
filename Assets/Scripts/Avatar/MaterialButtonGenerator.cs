using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialButtonGenerator : MonoBehaviour
{
    private List<Material> avatarMaterials = new List<Material>();
    public GameObject buttonPrefab;
    private MaterialLister materialLister;


    private void Start()
    {
        materialLister = GetComponent<MaterialLister>();
        avatarMaterials = materialLister.GetMaterialsFromFolder();
        //for each material in avatarMaterials instantiate a button
        
        var Xoffset = 0;
        var Yoffset = 0;
        foreach (var material in avatarMaterials)
        {
            //instantiates a button as a child of the parent object and a offset at each iteration
            GameObject newButton = Instantiate(buttonPrefab, transform);
            newButton.transform.localPosition = new Vector3(Xoffset, Yoffset, 0);
            //change text mesh pro text to the name of the material
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = material.name;
            Xoffset += 200;
            if (Xoffset > 600)
            {
                Yoffset -= 100;
                Xoffset = 0;
            }
            newButton.GetComponent<Button>().onClick.AddListener(() => OnMaterialButtonClicked(material));
        }
    }
    
    void OnMaterialButtonClicked(Material material)
    {
        //set the material of the avatar to the material of the button clicked
        GameObject.Find("Sphere").GetComponent<Renderer>().material = material;
        AvatarManager.currentMaterial = material;
    }
    
    
    
}
