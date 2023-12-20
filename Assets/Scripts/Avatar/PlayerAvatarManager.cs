using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;


public class PlayerAvatarManager : NetworkBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            if (AvatarManager.currentMaterial != null)
                GetComponent<Renderer>().material = AvatarManager.currentMaterial;
           

            int playerNumber = GetComponent<Spawn>().PlayerNumber;
            if (playerNumber == 1)
            {
                setMaterialNameServerRpc(GetComponent<Renderer>().material.name, playerNumber);
                Color secondColor = Color.white;
                if (GetComponent<Renderer>().material.HasProperty("_SecondaryColor"))
                    secondColor = GetComponent<Renderer>().material.GetColor("_SecondaryColor");
                setMaterialColorServerRpc(GetComponent<Renderer>().material.GetColor("_Color"), 
                    secondColor, playerNumber);
            }
            else if (playerNumber == 2)
            {
                setMaterialNameServerRpc(GetComponent<Renderer>().material.name, playerNumber);
                Color secondColor = Color.white;
                if (GetComponent<Renderer>().material.HasProperty("_SecondaryColor"))
                    secondColor = GetComponent<Renderer>().material.GetColor("_SecondaryColor");
                setMaterialColorServerRpc(GetComponent<Renderer>().material.GetColor("_Color"), 
                    secondColor, playerNumber);
            }
        }

        if (!IsLocalPlayer)
        {
            int playerNumber = GetComponent<Spawn>().PlayerNumber;
            List<Material> avatarMaterials = new List<Material>();
            avatarMaterials = GetComponent<MaterialLister>().GetMaterialsFromFolder();
            if (playerNumber == 1)
            {
                foreach (var material in avatarMaterials)
                {
                    if (material.name == parseMaterialName(Game.player1MaterialName.Value.ToString()))
                    {
                        GetComponent<Renderer>().material = material;
                    }
                }

                if (Game.player1Color.Value != null)
                    GetComponent<Renderer>().material.SetColor("_Color", Game.player1Color.Value);
                if (Game.player1SecondaryColor.Value != null)
                    GetComponent<Renderer>().material.SetColor("_SecondaryColor", Game.player1SecondaryColor.Value);



            }
            else if (playerNumber == 2)
            {
                foreach (var material in avatarMaterials)
                {
                    if (material.name == parseMaterialName(Game.player2MaterialName.Value.ToString()))
                    {
                        GetComponent<Renderer>().material = material;
                    }
                }

                if (Game.player2Color.Value != null)
                    GetComponent<Renderer>().material.SetColor("_Color", Game.player2Color.Value);
                if (Game.player2SecondaryColor.Value != null)
                    GetComponent<Renderer>().material.SetColor("_SecondaryColor", Game.player2SecondaryColor.Value);

            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void setMaterialNameServerRpc(string materialName, int playerNumber)
    {
        if (playerNumber == 1)
        {
            Game.player1MaterialName.Value = materialName;
        }
        else
        {
            Game.player2MaterialName.Value = materialName;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void setMaterialColorServerRpc(Color color, Color color2, int playerNumber)
    {
        if (playerNumber == 1)
        {
            Game.player1Color.Value = color;
            Game.player1SecondaryColor.Value = color2;
        }
        else
        {
            Game.player2Color.Value = color;
            Game.player2SecondaryColor.Value = color2;
        }
    }
    g// Remove all (Instance) from the name
    string parseMaterialName(string materialName)
    {
        
        
        string[] words = materialName.Split(' ');
        string newMaterialName = "";
        foreach (var word in words)
        {
            if (word != "(Instance)")
            {
                newMaterialName += word + " ";
            }
        }

        newMaterialName = newMaterialName.Remove(newMaterialName.Length - 1);
        return newMaterialName;
    }
}