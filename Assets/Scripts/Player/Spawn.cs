using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Netcode;
using UnityEngine;

public class Spawn : NetworkBehaviour
{
    public  int PlayerNumber = 0;
    
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
         
         if (SpawnManager.playersSpawned.Value == 0)
         {
             this.transform.position = spawnObject.transform.position;
             PlayerNumber = 1;
             SpawnManager.playersSpawned.Value++;
             Debug.Log(SpawnManager.playersSpawned.Value + " 1");
         }
         else if (PlayerNumber == 0)
         {
             this.transform.position = spawnObject2.transform.position;
             PlayerNumber = 2;
             SpawnManager.playersSpawned.Value++;
                Debug.Log(SpawnManager.playersSpawned.Value + " 2");
         }
        
        
    }
    
    
    

}
