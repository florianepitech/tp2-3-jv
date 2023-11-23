using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ObstacleTrigger : NetworkBehaviour
{
    // Start is called before the first frame update
    bool passed = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (passed)
            {
                return;
            }
            var playerNumber = other.gameObject.GetComponent<Spawn>().PlayerNumber;
            if (playerNumber > 2)
                return;
            if (playerNumber == 0)
                return;
            UpdateLedColorServerRpc(playerNumber);
            
            passed = true;
        }
    }

    /**
     * Update the color of the led of the player who passed the obstacle
     */
    private void UpdateLedColor(int playerNumber)
    {
        Debug.Log("UpdateLedColor");
        
        // if (playerNumber == 1)
        //     Game.PassedObstaclesPlayer1.Value++;
        // else if (playerNumber == 2)
        //     Game.PassedObstaclesPlayer2.Value++;
        // else
        // {
        //     Debug.LogError("Invalid player number");
        //     return;
        // }
        var index = playerNumber - 1;
        var sphere = transform.GetChild(index).gameObject;
        // Update the color to green
        sphere.GetComponent<Renderer>().material.color = Color.green;
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void UpdateLedColorServerRpc(int playerNumber)
    {
        // Server-side logic to update passed obstacles count
        if (playerNumber == 1)
            Game.PassedObstaclesPlayer1.Value++;
        else if (playerNumber == 2)
            Game.PassedObstaclesPlayer2.Value++;

        // Update color on all clients
        UpdateLedColorClientRpc(playerNumber);
    }

    [ClientRpc]
    private void UpdateLedColorClientRpc(int playerNumber, ClientRpcParams rpcParams = default)
    {
        UpdateLedColor(playerNumber);
    }

    
}