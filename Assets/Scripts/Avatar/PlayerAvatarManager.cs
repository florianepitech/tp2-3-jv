using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class PlayerAvatarManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // void FixedUpdate()
    // {
    //     if (IsLocalPlayer)
    //     {
    //         
    //         //rename the player gameobject to the player's name Me
    //         //this.gameObject.name = "Me";
    //         //set the material  of the player's avatar to the material that the player chose in the avatar editor
    //         if (AvatarManager.currentMaterial != null)
    //             GetComponent<Renderer>().material = AvatarManager.currentMaterial;
    //         else
    //         {
    //             Debug.Log("AvatarManager.currentMaterial is null");
    //         }
    //         //get my player number
    //             // SerializableMaterial serializableMaterial = new SerializableMaterial(GetComponent<Renderer>().material);
    //             // int playerNumber = GetComponent<Spawn>().PlayerNumber;
    //             //
    //             // if (playerNumber == 1)
    //             //     Game.player1Material.Value = serializableMaterial;
    //             // else if (playerNumber == 2)
    //             //     Game.player2Material.Value = serializableMaterial;
    //     } //if not the local player and is the other player 
    //
    //     if (!IsLocalPlayer)
    //     {
    //         if(AvatarManager.currentMaterial != null)
    //             GetComponent<Renderer>().material = AvatarManager.currentMaterial;
    //     }
    //     
    // }
}
