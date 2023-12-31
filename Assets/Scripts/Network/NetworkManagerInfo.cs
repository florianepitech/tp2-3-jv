using System;
using System.Linq;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking.Transport;

public class NetworkManagerController : NetworkBehaviour
{
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (IsServer)
        {
            //print the number of connected clients
            //list the connected clients
            var connectedClients = NetworkManager.Singleton.ConnectedClientsList;
            if (connectedClients.Count > 0)
            {
                Debug.Log("Connected clients: " + connectedClients.Count);
                foreach (var client in connectedClients)
                {
                    Debug.Log("Client: " + client.ClientId);
                }
            }
        }

    }
    
}