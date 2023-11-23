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
            UpdateLedColor(playerNumber);
            Game.passedObstacles++;
            passed = true;
        }
    }

    /**
     * Update the color of the led of the player who passed the obstacle
     */
    private void UpdateLedColor(int playerNumber)
    {
        // Get the sphere child game object
        var index = playerNumber - 1;
        var sphere = transform.GetChild(index).gameObject;
        // Update the color to green
        sphere.GetComponent<Renderer>().material.color = Color.green;
    }
    
}