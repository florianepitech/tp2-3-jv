using Unity.Netcode;
using UnityEngine;


public class SpawnManager : NetworkBehaviour
{
    public GameObject PrefabToSpawn;
    public static NetworkVariable<int> playersSpawned = new(0);
    private int playersApproved = 0;

    private void Awake()
    {
        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback += ConnectionApprovalCallback;
            //NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton)
        {
            //NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.ConnectionApprovalCallback -= ConnectionApprovalCallback;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        
        if (IsServer)
        {
            //SpawnPlayerServerRpc(clientId);
        }
    }
    void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        /* you can use this method in your project to customize one of more aspects of the player
         * (I.E: its start position, its character) and to perform additional validation checks. */
        Debug.Log("ConnectionApprovalCallback");
        if (playersApproved< 2)
        {
            response.Approved = true;
            response.CreatePlayerObject = true;
            response.Position = new Vector3(0,0,0);
            playersApproved++;
        }
        else
        {
            response.Approved = false;
        }
    }
    
    Vector3 GetPlayerSpawnPosition()
    {
        GameObject spawnObject = GameObject.Find("Spawn");
        if (spawnObject == null)
        {
            Debug.LogError("Spawn object not found");
            return new Vector3(0,0,0);
        }
         
        GameObject spawnObject2 = GameObject.Find("Spawn 2");
        if (spawnObject2 == null)
        {
            Debug.LogError("Spawn object not found");
            return new Vector3(0,0,0);
                
        }

        if (playersSpawned.Value == 0)
        {
            playersSpawned.Value++;
            return spawnObject.transform.position;
        }
        
        playersSpawned.Value++;
        return spawnObject2.transform.position;
        
            
    }

//     private bool IsServer
//     {
//         get { return NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost; }
//     }
//
//     [ServerRpc]
//     private void SpawnPlayerServerRpc(ulong clientId)
//     {
//         GameObject spawnObject = GameObject.Find("Spawn");
//         if (spawnObject == null)
//         {
//             Debug.LogError("Spawn object not found");
//             return;
//         }
//         
//         GameObject spawnObject2 = GameObject.Find("Spawn 2");
//         if (spawnObject2 == null)
//         {
//             Debug.LogError("Spawn object not found");
//             return;
//         }
//
//         GameObject playerInstance = Instantiate(PrefabToSpawn);
//         playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
//         playerInstance.SetActive(false);
//         Debug.Log("playerSpawned " + playersSpawned.Value);
//         if (playersSpawned.Value == 0)
//         {
//             playerInstance.SetActive(true);
//             playerInstance.transform.position = spawnObject.transform.position;
//             playersSpawned.Value++;
//         }
//
//         if (playersSpawned.Value == 1)
//         {
//             Debug.Log("Spawn at spawn 2");
//             playerInstance.SetActive(true);
//             playerInstance.transform.position = spawnObject.transform.position;
//         }
//         
//     }
    

}