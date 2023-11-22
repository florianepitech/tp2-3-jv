using keyboard;
using Unity.Netcode;
using UnityEngine;

public class ShootController : NetworkBehaviour
{
    public Rigidbody sphereRigidbody;
    private float maxShootForce = 50f; // Current selected shoot force
    private bool shotTaken = false;
    private float stopThreshold = 0.3f; // Velocity threshold for stopping

    void Update()
    {
        if (IsLocalPlayer)
        {
            // Increase shoot force
            // if (KeyboardEvent.IsPressed(KeyMovement.IncreasePower) && !shotTaken)
            // {
            //     currentShootForce = Mathf.Min(currentShootForce + Time.deltaTime * 10f, maxShootForce);
            // }

            // Take a shot
            if (KeyboardEvent.GetKey(KeyMovement.Shoot) && !shotTaken)
            {
                ShootClientRpc();
                shotTaken = true; // Prevents further increase in power or re-shooting
                Game.BallIsMovement = true;
            }

            // Check if the ball's velocity is below the threshold
            if (sphereRigidbody == null)
                return;
            if (shotTaken && sphereRigidbody.velocity.magnitude < stopThreshold)
            {
                sphereRigidbody.velocity = Vector3.zero;
                sphereRigidbody.angularVelocity = Vector3.zero;
                Game.BallIsMovement = false;
            }
        }
    }

    [ClientRpc]
    void ShootClientRpc()
    {
        if (IsLocalPlayer)
        {
            var shootBarContainer = GameObject.Find("ShootBarContainer");
            if (!shootBarContainer.active)
                return;

            sphereRigidbody = gameObject.GetComponent<Rigidbody>();
            var shootBarContainerTransform = GameObject.Find("ShootBarContainer").transform;
            PowerBar.SetRun(false);
            var currentShootForce = PowerBar.GetPower() * maxShootForce / 100f;
            sphereRigidbody.AddForce(shootBarContainerTransform.forward * currentShootForce, ForceMode.Impulse);
            HideShootBarServerRpc();
        }
    }
    
    [ServerRpc]
    void HideShootBarServerRpc()
    {
        HideShootBarClientRpc(); // Call the client RPC from the server RPC
    }

    
    [ServerRpc (RequireOwnership = false)]
    public void ShowShootBarServerRpc()
    {
        ShowShootBarClientRpc(); // Call the client RPC from the server RPC
    }
    
    [ClientRpc]
    void HideShootBarClientRpc()
    {
        var shootBarContainer = GameObject.Find("ShootBarContainer");
        if (shootBarContainer != null)
        {
            shootBarContainer.SetActive(false); // Hide the shoot bar on all clients
        }
    }
    
    [ClientRpc]
    void ShowShootBarClientRpc()
    {
        var shootBarContainer = GameObject.Find("ShootBarContainer");
        if (shootBarContainer != null)
        {
            Debug.Log("Show shoot bar");
            shootBarContainer.SetActive(true); // Hide the shoot bar on all clients
        }
    }
}