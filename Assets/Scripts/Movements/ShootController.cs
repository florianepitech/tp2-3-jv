using keyboard;
using Unity.Netcode;
using UnityEngine;

public class ShootController : NetworkBehaviour
{
    public Rigidbody sphereRigidbody;
    public float shootForce = 10f;
    public float stopTime = 10f; // Adjust this value as needed
    private bool isShooting = false;
    private float shotTime;


    void Update()
    {
        if (IsLocalPlayer && KeyboardEvent.GetKey(KeyMovement.Shoot))
        {
            ShootClientRpc();
        }
        
        if (isShooting && Time.time - shotTime > stopTime)
        {
            StopSphere();
        }
    }

    [ClientRpc]
    void ShootClientRpc()
    {
        // Ensure that the shooting logic is only executed on the local player's client.
        if (IsLocalPlayer)
        {
            var shootBarContainer = GameObject.Find("ShootBarContainer");
            if (shootBarContainer.active == false)
                return;
            // Apply a force to the sphere's Rigidbody to simulate shooting.
            sphereRigidbody = gameObject.GetComponent<Rigidbody>();
            //allow to shoot in the direction of the camera
            //get the transform of the ShootBarContainer
            var shootBarContainerTransform = GameObject.Find("ShootBarContainer").transform;
            sphereRigidbody.AddForce(shootBarContainerTransform.forward * shootForce, ForceMode.Impulse);
            // Get ShootBarContainer and not display it
            shootBarContainer.SetActive(false);
                
            isShooting = true;
            shotTime = Time.time;
            
        }
    }
    
    void StopSphere()
    {
        // You can stop the sphere by counteracting its velocity or applying force in the opposite direction.
        // For example, to stop it gradually, you can apply an opposing force:
        Debug.Log("StopSphere called");
        sphereRigidbody.AddForce(-sphereRigidbody.velocity, ForceMode.Force);
        // To stop it instantly, you can set its velocity to zero:
        // sphereRigidbody.velocity = Vector3.zero;

        // Reset the shooting state.
        isShooting = false;
    }
}