using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ObstacleTrigger : NetworkBehaviour
{
    // Start is called before the first frame update
    bool passed_player1 = false;
    bool passed_player2 = false;
    
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerNumber = other.gameObject.GetComponent<Spawn>().PlayerNumber;
            if (playerNumber == 1 && passed_player1)
            {
                return;
            }
            else if (playerNumber == 2 && passed_player2)
            {
                return;
            }
            if (playerNumber > 2)
                return;
            if (playerNumber == 0)
                return;
            //execute only once
            audioSource.Play(); // Play the sound
            if (IsServer)
                UpdateLedColorServerRpc(playerNumber);
            
            if (playerNumber == 1)
            {
                passed_player1 = true;
            }
            else if (playerNumber == 2)
            {
                passed_player2 = true;
            }
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
        {
            Debug.Log("Passed obstacle player 1");
            Game.PassedObstaclesPlayer1.Value++;
        }
        else if (playerNumber == 2)
        {
            Debug.Log("Passed obstacle player 2");
            Game.PassedObstaclesPlayer2.Value++;
        }

        // Update color on all clients
        UpdateLedColorClientRpc(playerNumber);
    }

    [ClientRpc]
    private void UpdateLedColorClientRpc(int playerNumber, ClientRpcParams rpcParams = default)
    {
        UpdateLedColor(playerNumber);
    }

    
}