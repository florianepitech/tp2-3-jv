using System.Collections;
using System.Collections.Generic;
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
            else
            {

                Debug.Log("AvatarManager.currentMaterial is null");
            }

            int playerNumber = GetComponent<Spawn>().PlayerNumber;
            if (playerNumber == 1)
            {
                setMaterialNameServerRpc(GetComponent<Renderer>().material.name, playerNumber);
                setMaterialColorServerRpc(GetComponent<Renderer>().material.GetColor("_Color"), playerNumber);
                if (GetComponent<Renderer>().material.HasProperty("_SecondaryColor"))
                    Game.player1SecondaryColor.Value = GetComponent<Renderer>().material.GetColor("_SecondaryColor");
                else Game.player1SecondaryColor.Value = Color.white;
                Debug.Log("Game.player1Name.Value: " + Game.player1MaterialName.Value);

            }
            else if (playerNumber == 2)
            {
                setMaterialNameServerRpc(GetComponent<Renderer>().material.name, playerNumber);
                setMaterialColorServerRpc(GetComponent<Renderer>().material.GetColor("_Color"), playerNumber);
                if (GetComponent<Renderer>().material.HasProperty("_SecondaryColor"))
                    Game.player2SecondaryColor.Value = GetComponent<Renderer>().material.GetColor("_SecondaryColor");
                else
                    Game.player2SecondaryColor.Value = Color.white;
                Debug.Log("Game.player2Name.Value: " + Game.player2MaterialName.Value);

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
                    Debug.Log("------------------");
                    Debug.Log("material.name: " + material.name);
                    Debug.Log("Game.player1MaterialName.Value: " + Game.player1MaterialName.Value);
                    if (material.name == Game.player1MaterialName.Value)
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
                Debug.Log("Game.player2Color.Value: " + Game.player2Color.Value);
                foreach (var material in avatarMaterials)
                {
                    Debug.Log("------------------");
                    Debug.Log("material.name: " + material.name);
                    Debug.Log("Game.player2MaterialName.Value: " + Game.player2MaterialName.Value);
                    if (material.name == Game.player2MaterialName.Value)
                    {
                        Debug.Log("selected material : " + material.name);
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
    void setMaterialColorServerRpc(Color color, int playerNumber)
    {
        if (playerNumber == 1)
        {
            Game.player1Color.Value = color;
        }
        else
        {
            Game.player2Color.Value = color;
        }
    }
}