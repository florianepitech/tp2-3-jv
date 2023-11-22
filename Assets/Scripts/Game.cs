using System;
using System.Collections.Generic;
using keyboard;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class Game : NetworkBehaviour
{
    public static GameType gameType;
    public IReadOnlyList<NetworkClient> connectedClients = new List<NetworkClient>();
    public static List<GameObject> obstacles = new List<GameObject>();
    public static int passedObstacles = 0;
    
    private static NetworkVariable<bool> IsGameStarted = new(false);
    private static NetworkVariable<FixedString512Bytes> GameInfoMessage = new("");
    private static NetworkVariable<int> playerTurn = new(1);
    public static bool BallIsMovement = false;
    bool switchTurn = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //get all gameobject with the tag "Obstacle"
        var obstaclesArray = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obstacle in obstaclesArray)
        {
            obstacles.Add(obstacle);
        }
        
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
        if (IsServer)
            UpdatePlayerHost();
        else if (IsClient)
            UpdatePlayerClient();
        UpdateAllPlayer();
    }

    private void UpdatePlayerHost()
    {
        if (connectedClients.Count != 2)
        {
            GameInfoMessage.Value = "Waiting for another player to join...";
            return;
        }

        if (!IsGameStarted.Value)
        {
            IsGameStarted.Value = true;
            PowerBar.SetRun(true);
        }
        // if (BallIsMovement)
        //     switchTurn = false;
        // if (!BallIsMovement && !switchTurn)
        // {
        //     playerTurn.Value = playerTurn.Value == 1 ? 2 : 1;
        //     GameInfoMessage.Value = "Player " + playerTurn.Value + " turn";
        //     // Set the shoot bar visible and set the position to the player position
        //     var player = connectedClients[playerTurn.Value - 1];
        //     setShootBarPosition(player);
        //     var shootController = player.PlayerObject.GetComponent<ShootController>();
        //     shootController.ShowShootBarServerRpc();
        //     switchTurn = true;
        // }
    }
    
    private void UpdatePlayerClient()
    {
        
    }

    private void UpdateAllPlayer()
    {
        // Check for pause menu
        var openPauseMenu = KeyboardEvent.GetKeyUp(KeyMovement.PauseMenu);
        if (openPauseMenu && !PauseMenu.IsOpen)
        {
            PauseMenu.IsOpen = true;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }

        // Check for shoot bar activation
        if (IsGameStarted.Value)
        {
            var player = connectedClients[playerTurn.Value - 1];
            setShootBarPosition(player);
        }
        
        // Update the game info message
        var gameInfoMessage = GameObject.FindWithTag("GameInfo");
        if (gameInfoMessage == null)
            return;
        var gameInfoMessageText = gameInfoMessage.GetComponent<TextMeshProUGUI>();
        if (gameInfoMessageText != null)
            gameInfoMessageText.text = GameInfoMessage.Value.ToString();
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
        
        // If the ShootBar is not display do nothin
        if (shootBar.activeSelf == false)
            return;
        
        // Set the ShootBar position to the networkClient position
        shootBar.transform.position = networkClient.PlayerObject.transform.position;

        /*
         * Compute rotation
         */

        // Define the pivot point as the bottom of the cylinder
        var pivotPoint = shootBar.transform.position;

        // HORIZONTAL ROTATION

        // Calculate the rotation amount based on your desired input or method
        var rotation = TurnCrossHairDirectionHorizontal();
        // Rotate the ShootBar around the Y-axis with respect to the pivot point
        shootBar.transform.RotateAround(pivotPoint, Vector3.up, rotation);

        // VERTICAL ROTATION
        var rotationVerticalInput = TurnCrossHairDirectionVertical();
        var rotationBar = shootBar.transform.rotation;

        var newXRotation = rotationBar.eulerAngles.x + rotationVerticalInput;

        shootBar.transform.rotation = Quaternion.Euler(
            newXRotation,
            rotationBar.eulerAngles.y,
            rotationBar.eulerAngles.z
        );
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
    void getPlayersConnectedServerRpc()
    {
        connectedClients = NetworkManager.Singleton.ConnectedClientsList;
    }
}