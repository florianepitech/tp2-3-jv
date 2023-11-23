using DefaultNamespace.Player;
using keyboard;
using Unity.Netcode;
using UnityEngine;

public class ShootController : NetworkBehaviour
{
    public Rigidbody sphereRigidbody;
    public GameObject shootBarContainer;
    private float maxShootForce = 50f; // Current selected shoot force
    private bool shotTaken = false;
    private float stopThreshold = 0.3f; // Velocity threshold for stopping
    private bool canBeStopped;
    private int playerNumber;
    
    void Update()
    {
        if (IsOwner)
        {
            if (Game.playerTurn.Value != gameObject.GetComponent<Spawn>().PlayerNumber)
            {
                if (shootBarContainer.activeSelf)
                {
                    Debug.Log("Hiding shoot bar");
                    shootBarContainer.GetComponent<ShootBar>().ToggleShootBarVisibilityOnAllClientsServerRpc(false);
                }
                return;
            } else if (!shootBarContainer.activeSelf) {
                Debug.Log("Showing shoot bar");
                shootBarContainer.GetComponent<ShootBar>().ToggleShootBarVisibilityOnAllClientsServerRpc(true);
            }
            
            if (KeyboardEvent.GetKey(KeyMovement.Shoot) && !shotTaken)
            {
                Debug.Log("Shoot");
                shotTaken = true;
                PowerBar.SetRun(false);
                ShootServerRpc(PowerBar.GetPower());
                 // Prevents further increase in power or re-shooting
            }
            
            if (sphereRigidbody.velocity.magnitude > stopThreshold)
            {
                canBeStopped = true;
            }

            // Check if the ball's velocity is below the threshold
            if (sphereRigidbody == null)
            {
                Debug.Log("Sphere rigidbody is null");
                return;
            }

            if (shotTaken && sphereRigidbody.velocity.magnitude < stopThreshold && canBeStopped)
            {
                StopSphereServerRpc();
            }
        }
    }

    // [ClientRpc]
    // void ShootClientRpc()
    // {
    //     if (IsLocalPlayer)
    //     {
    //         Debug.Log("ShootClientRpc");
    //         if (!shootBarContainer.activeSelf)
    //             return;
    //         var shootBarContainerTransform = shootBarContainer.transform;
    //         PowerBar.SetRun(false);
    //         var currentShootForce = PowerBar.GetPower() * maxShootForce / 100f;
    //         sphereRigidbody.AddForce(shootBarContainerTransform.forward * currentShootForce, ForceMode.Impulse);
    //         //HideShootBarServerRpc();
    //     }
    
    [ServerRpc]
    void ShootServerRpc(int power)
    {
        Game.playerTurn.Value = Game.playerTurn.Value = 0;
        
        Debug.Log("ShootServerRpc called");
        if (!shootBarContainer.activeSelf)
        {
            Debug.Log("Shoot bar is not active");
            return;
        }

        var currentShootForce = power * maxShootForce / 100f;
        Debug.Log($"Applying force: {currentShootForce}");

        var shootDirection = shootBarContainer.transform.forward;
        Debug.Log($"Shoot direction: {shootDirection}");
        shootBarContainer.GetComponent<ShootBar>().ToggleShootBarVisibilityOnAllClientsServerRpc(false);

        sphereRigidbody.AddForce(shootDirection * currentShootForce, ForceMode.Impulse);
    }
    
    
    [ServerRpc (RequireOwnership = false)]
    void StopSphereServerRpc()
    {
        if (sphereRigidbody != null && shotTaken)
        {
            Debug.Log("StopSphereServerRpc");
            sphereRigidbody.velocity = Vector3.zero;
            sphereRigidbody.angularVelocity = Vector3.zero;
            shotTaken = false;
        }
        
    }

    [ClientRpc]
    void NotifyStopSphereClientRpc()
    {
        if (!IsServer && sphereRigidbody != null) // Check if not the server to avoid double execution
        {
            Debug.Log("NotifyStopSphereClientRpc");
            sphereRigidbody.velocity = Vector3.zero;
            sphereRigidbody.angularVelocity = Vector3.zero;
        }
    }
}