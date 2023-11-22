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
    private bool canBeStopped = false;
    
    
    void Start()
    {
        if (IsLocalPlayer)
        {
            //sphereRigidbody = gameObject.GetComponent<Rigidbody>();
            //get the child
            //shootBarContainer = gameObject.transform.GetChild(0).gameObject;
        }
    }

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
                Debug.Log("Shoot");
                shotTaken = true;
                ShootServerRpc(PowerBar.GetPower());
                 // Prevents further increase in power or re-shooting
            }
            
            if (sphereRigidbody.velocity.magnitude > stopThreshold)
            {
                canBeStopped = true;
            }

            // Check if the ball's velocity is below the threshold
            if (sphereRigidbody == null)
                return;
            if (shotTaken && sphereRigidbody.velocity.magnitude < stopThreshold && canBeStopped)
            {
                Debug.Log("Stop");
                sphereRigidbody.velocity = Vector3.zero;
                sphereRigidbody.angularVelocity = Vector3.zero;
               // Game.CallNextTurn();
                shotTaken = false;
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

        sphereRigidbody.AddForce(shootDirection * currentShootForce, ForceMode.Impulse);
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
        if (shootBarContainer != null)
        {
            shootBarContainer.SetActive(false); // Hide the shoot bar on all clients
        }
    }
    
    [ClientRpc]
    void ShowShootBarClientRpc()
    {
        if (shootBarContainer != null)
        {
            Debug.Log("Show shoot bar");
            shootBarContainer.SetActive(true); // Hide the shoot bar on all clients
        }
    }
}