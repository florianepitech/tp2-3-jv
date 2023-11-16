

using Unity.Netcode;
using UnityEngine;

public class CustomNetworkManager : NetworkBehaviour
{
    public GameObject PrefabToSpawn;
    // This a spawn manager that will spawn all players in the same location
    
    public override void OnNetworkSpawn()
    {
        // This is called on the client when the object is spawned
        // We want to spawn the player prefab
        //Spawn the player prefab
        //get Spawn GameObject and set the ball to spawn at the spawn point
        
        GameObject spawnObject = GameObject.Find("Spawn");
        if (spawnObject == null)
        {
            Debug.LogError("Spawn object not found");
            return;
        }
        GameObject playerInstance = Instantiate(PrefabToSpawn, transform.position, transform.rotation);
        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(OwnerClientId);
        playerInstance.transform.position =  spawnObject.transform.position;
    }
} 