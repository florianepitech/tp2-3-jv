using System.Collections;
using System.Collections.Generic;
using keyboard;
using UnityEngine;
using Unity.Netcode;

public class Movement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 currentInput;

    private void Update()
    {
        if (IsLocalPlayer)
        {
            float horizontalInput = KeyboardEvent.TurnCrossHairDirectionVertical(); 
            float verticalInput = Input.GetAxis("Vertical");
            
            
            currentInput = new Vector3(horizontalInput, 0, verticalInput).normalized;

            // Send the updated input to the server
            SubmitInputToServerRpc(currentInput);
        }
    }

    private void FixedUpdate()
    {
        if (IsServer)
        {
            HandleMovement(currentInput);
        }
    }

    [ServerRpc]
    void SubmitInputToServerRpc(Vector3 input)
    {
        // Server receives client input and applies it
        currentInput = input;
    }

    private void HandleMovement(Vector3 input)
    {
        // Apply movement on the server
        Vector3 moveDirection = input * (moveSpeed * Time.fixedDeltaTime);
        transform.position += moveDirection;

        // Update the position on clients
        RpcUpdatePositionOnClientsClientRpc(transform.position);
    }

    [ClientRpc]
    void RpcUpdatePositionOnClientsClientRpc(Vector3 newPosition)
    {
        // Update the position on all clients
        transform.position = newPosition;
    }
}