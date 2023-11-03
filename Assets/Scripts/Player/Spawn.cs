using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawn : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            SpawnPlayerServerRpc();
        }
    }
    
    [ServerRpc]
    void SpawnPlayerServerRpc()
    {
        transform.position = new Vector3(0, 2, 0);
    }
    

}
