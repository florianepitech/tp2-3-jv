using DefaultNamespace.Player;
using keyboard;
using Unity.Netcode;
using UnityEngine;

public class ShootController : NetworkBehaviour
{
    public Rigidbody sphereRigidbody;
    public GameObject shootBarContainer;
    private float maxShootForce = 50f; // Current selected shoot force
    private float stopThreshold = 0.4f; // Velocity threshold for stopping
    private int playerNumber;
    void Update()
    {
        if (playerNumber == 0)
            playerNumber = gameObject.GetComponent<Spawn>().PlayerNumber;
        
        
        if (IsOwner)
        {
            if (Game.playerTurn.Value != gameObject.GetComponent<Spawn>().PlayerNumber)
            {
                
                if (shootBarContainer.activeSelf)
                {
                    Debug.Log("Hiding shoot bar");
                    shootBarContainer.GetComponent<ShootBar>().ToggleShootBarVisibilityOnAllClientsServerRpc(false);
                    PowerBar.SetRun(false);
                }
                return;
            } else if (!shootBarContainer.activeSelf) {
                Debug.Log("Showing shoot bar");
                if (playerNumber == 0)
                    return;
                if (playerNumber == 1)
                {
                    Game.player1Shootings.Value = false;
                } else if (playerNumber == 2) {
                    Game.player2Shootings.Value = false;
                }
                shootBarContainer.GetComponent<ShootBar>().ToggleShootBarVisibilityOnAllClientsServerRpc(true);
            }
            else
            {
                if (!PowerBar.GetRun())
                    PowerBar.SetRun(true);
            }
            
            if (KeyboardEvent.GetKey(KeyMovement.Shoot))
            {
                if (playerNumber == 1)
                {
                    if (Game.player1Shootings.Value)
                        return;
                } else if (playerNumber == 2) {
                    if (Game.player2Shootings.Value)
                        return;
                } else if (playerNumber == 0) {
                    return;
                }
                 
                Debug.Log("Shoot");
                
                //get my playerNumber
                if (playerNumber == 0)
                    return;
                if (playerNumber == 1)
                {
                    setValueShootingServerRpc(1);
                } else if (playerNumber == 2) {
                    setValueShootingServerRpc(2);
                }
                PowerBar.SetRun(false);
                ShootServerRpc(PowerBar.GetPower());
                 // Prevents further increase in power or re-shooting
            }
        }
    }
    
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
    
    [ServerRpc]
    void setValueShootingServerRpc(int value)
    {
        if (value == 1)
        {
            Game.player1Shootings.Value = true;
        }
        else if (value == 2)
        {
            Game.player2Shootings.Value = true;
        }
        else
        {
            Debug.Log("Error in setValueShooting");
        }
    }
}