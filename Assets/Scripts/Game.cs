using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class Game : NetworkBehaviour
{
    public static GameType gameType;
    public IReadOnlyList<NetworkClient> connectedClients = new List<NetworkClient>();

    // Start is called before the first frame update
    void Start()
    {
        var networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        switch (gameType)
        {
            case GameType.JoinGame:
                networkManager.GetComponent<UnityTransport>().ConnectionData.Address = JoinGame.HostInputFieldValue;
                networkManager.StartClient();
                break;
            case GameType.HostGame:
                networkManager.StartHost();
                break;
            default:
                gameType = GameType.HostGame;
                Debug.Log("Invalid game type, starting host game by default...");
                networkManager.StartHost();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for pause menu
        var openPauseMenu = KeyboardEvent.IsEscape();
        if (openPauseMenu && !PauseMenu.IsOpen)
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }

        // Game loop
        if (connectedClients.Count != 1)
        {
            Debug.Log("Waiting for another player to join...");
            return;
        }

        var firstPlayer = connectedClients[0];
        setShootBarPosition(firstPlayer);
    }

    private void FixedUpdate()
    {
        //only the server can get the number of connected clients
        if (IsServer)
            getPlayersConnectedServerRpc();
    }

    private void setShootBarPosition(NetworkClient networkClient)
    {
        // Get the ShootBar cylinder
        var shootBar = GameObject.Find("ShootBarContainer");
        if (shootBar == null)
        {
            Debug.Log("ShootBar not found");
            return;
        }

        // Set the ShootBar position to the networkClient position
        shootBar.transform.position = networkClient.PlayerObject.transform.position;

        /*
         * Compute rotation
         */

        // Define the pivot point as the bottom of the cylinder
        var pivotPoint = shootBar.transform.position;

        // HORIZONTAL ROTATION

        // Calculate the rotation amount based on your desired input or method
        var rotation = KeyboardEvent.TurnCrossHairDirectionHorizontal();
        // Rotate the ShootBar around the Y-axis with respect to the pivot point
        shootBar.transform.RotateAround(pivotPoint, Vector3.up, rotation);

        // VERTICAL ROTATION
        var rotationVerticalInput = KeyboardEvent.TurnCrossHairDirectionVertical();
        var rotationBar = shootBar.transform.rotation;

        var newXRotation = rotationBar.eulerAngles.x + rotationVerticalInput;

        shootBar.transform.rotation = Quaternion.Euler(
            newXRotation,
            rotationBar.eulerAngles.y,
            rotationBar.eulerAngles.z
        );
    }

    [ServerRpc]
    void getPlayersConnectedServerRpc()
    {
        connectedClients = NetworkManager.Singleton.ConnectedClientsList;
    }
}