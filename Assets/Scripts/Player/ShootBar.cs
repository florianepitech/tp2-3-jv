using keyboard;

namespace DefaultNamespace.Player
{
    using UnityEngine;
    using Unity.Netcode;

    public class ShootBar : NetworkBehaviour
    {
        public GameObject shootBar; // Reference to the shoot bar UI
        private float horizontalRotationSpeed = 100f; // Adjust as needed
        private float verticalRotationSpeed = 100f;

        void Start()
        {
           //get the shootbar
           if (IsLocalPlayer)
           {
               //the shootbar is a child of the player get the child
             //  shootBar = gameObject;
           }
        }

        void Update()
        {
            if (IsLocalPlayer && shootBar.activeSelf)
            {
                UpdateRotation();
            }
        }


        void FixedUpdate()
        {
            if (IsLocalPlayer && shootBar.activeSelf)
            {
                // Correctly get the NetworkObjectId of the parent NetworkObject
                var playerNetworkObjectId = transform.parent.GetComponent<NetworkObject>().NetworkObjectId;
                setShootBarPositionServerRpc(playerNetworkObjectId);
            }
            
           
        }
        
        
        
        private void UpdateRotation()
        {
            // Calculate rotation based on input
            var horizontalRotationInput = TurnCrossHairDirectionHorizontal() * horizontalRotationSpeed * Time.deltaTime;
            var verticalRotationInput = TurnCrossHairDirectionVertical() * verticalRotationSpeed * Time.deltaTime;
            
            UpdateRotationServerRpc(horizontalRotationInput, verticalRotationInput);
            
            // Debug.Log("Horizontal rotation input: " + horizontalRotationInput);
            // // HORIZONTAL ROTATION
            // shootBar.transform.Rotate(Vector3.up, horizontalRotationInput, Space.World);
            //
            // Debug.Log("Vertical rotation input: " + verticalRotationInput);
            //
            // Debug.Log("Shoot bar rotation: " + shootBar.transform.rotation);
            // // VERTICAL ROTATION
            // // You might want to clamp the X rotation to prevent unrealistic rotation
            // shootBar.transform.Rotate(Vector3.right, verticalRotationInput, Space.World);
        }
        
        private int TurnCrossHairDirectionHorizontal()
        {
            var result = 0;
            if (KeyboardEvent.GetKey(KeyMovement.CrossHairLeft))
                result += 1;
            if (KeyboardEvent.GetKey(KeyMovement.CrossHairRight))
                result -= 1;
            return result;
        }
    
        private int TurnCrossHairDirectionVertical()
        {
            var result = 0;
            if (KeyboardEvent.GetKey(KeyMovement.CrossHairUp))
                result += 1;
            if (KeyboardEvent.GetKey(KeyMovement.CrossHairDown))
                result -= 1;
            return result;
        }
        
        [ServerRpc]
        public void UpdateRotationServerRpc(float horizontalRotationInput, float verticalRotationInput)
        {
            //apply the rotation on the server
            shootBar.transform.Rotate(Vector3.up, horizontalRotationInput, Space.World);
            //shootBar.transform.Rotate(Vector3.right, verticalRotationInput, Space.World);
            var rotationBar = shootBar.transform.rotation;

            var newXRotation = rotationBar.eulerAngles.x + verticalRotationInput;

            shootBar.transform.rotation = Quaternion.Euler(
                newXRotation,
                rotationBar.eulerAngles.y,
                rotationBar.eulerAngles.z
            );
            //broadcast the rotation to all clients
            UpdateRotationClientRpc(horizontalRotationInput, verticalRotationInput);
        }

        [ClientRpc]
        public void UpdateRotationClientRpc(float horizontalRotationInput, float verticalRotationInput)
        {
            //apply the rotation on the client
            shootBar.transform.Rotate(Vector3.up, horizontalRotationInput, Space.World);
            var rotationBar = shootBar.transform.rotation;

            var newXRotation = rotationBar.eulerAngles.x + verticalRotationInput;

            shootBar.transform.rotation = Quaternion.Euler(
                newXRotation,
                rotationBar.eulerAngles.y,
                rotationBar.eulerAngles.z
            );
        }


        [ServerRpc]
        public void setShootBarPositionServerRpc(ulong playerNetworkObjectId)
        {
            // Find the player's NetworkObject
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerNetworkObjectId, out NetworkObject player))
            {
                // Set the shootbar position to the player position
                shootBar.transform.position = player.transform.position;
                // Broadcast the position to all clients
                setShootBarPositionClientRpc(playerNetworkObjectId);
            }
            else
            {
                Debug.LogError("Player not found");
            }
        }

        [ClientRpc]
        public void setShootBarPositionClientRpc(ulong playerNetworkObjectId)
        {
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerNetworkObjectId, out NetworkObject player))
            {
                // Set the shootbar position to the player position
                shootBar.transform.position = player.transform.position;
            }
        }
        
        [ServerRpc]
        public void ShowShootBarServerRpc()
        {
            ShowShootBarClientRpc(); // Call the client RPC from the server RPC
        }
        
        [ClientRpc]
        void ShowShootBarClientRpc()
        {
            if (shootBar != null)
            {
                shootBar.SetActive(true); // Show the shoot bar on all clients
            }
            else
            {
                Debug.LogError("Shoot bar not found");
            }
        }
        
        [ServerRpc]
        public void HideShootBarServerRpc()
        {
            HideShootBarClientRpc(); // Call the client RPC from the server RPC
        }
        
        [ClientRpc]
        void HideShootBarClientRpc()
        {
            if (shootBar != null)
            {
                shootBar.SetActive(false); // Hide the shoot bar on all clients
            }
        }
        
        
        [ServerRpc (RequireOwnership = false)]
        public void ToggleShootBarVisibilityOnAllClientsServerRpc(bool isVisible)
        {
            ToggleShootBarVisibilityOnAllClientsClientRpc(isVisible);
        }

        [ClientRpc]
        public void ToggleShootBarVisibilityOnAllClientsClientRpc(bool isVisible)
        {
            if (shootBar != null)
            {
                shootBar.SetActive(isVisible);
            }
        }

    }

}