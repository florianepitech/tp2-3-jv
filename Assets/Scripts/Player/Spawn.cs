using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawn : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log("Spawned");
        
        GameObject spawnObject = GameObject.Find("Spawn");
         if (spawnObject == null)
         {
             Debug.LogError("Spawn object not found");
             return;
         }
         
         GameObject spawnObject2 = GameObject.Find("Spawn 2");
         if (spawnObject2 == null)
         {
             Debug.LogError("Spawn object not found");
             return;
         }

         if (SpawnManager.playersSpawned == 0)
         {
             this.transform.position = spawnObject.transform.position;
             SpawnManager.playersSpawned++;
         }
         else
         {
             this.transform.position = spawnObject2.transform.position;
             SpawnManager.playersSpawned++;
         }
        
        
    }
    
    
    

}
