using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAvatarManager : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            //set the material  of the player's avatar to the material that the player chose in the avatar editor
            if (AvatarManager.currentMaterial != null)
                GetComponent<Renderer>().material = AvatarManager.currentMaterial;
            else
            {
                Debug.Log("AvatarManager.currentMaterial is null");
            }
        }
        
    }
}
