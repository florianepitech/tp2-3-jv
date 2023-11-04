using Unity.Netcode;
using UnityEngine;

public class ShootController : NetworkBehaviour
{
    public Rigidbody sphereRigidbody;
    public float shootForce = 10f;

    void Update()
    {
        if (IsLocalPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            ShootClientRpc();
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
        }
    }
}