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
    public static IReadOnlyList<NetworkClient> connectedClients = new List<NetworkClient>();
    public static List<GameObject> obstacles = new List<GameObject>();
    public static int passedObstacles = 0;
    private static NetworkVariable<bool> IsGameStarted = new(false);
    private static NetworkVariable<FixedString512Bytes> GameInfoMessage = new("");
    public static NetworkVariable<int> playerTurn = new(2);
    private float timer = 0f;
    

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
        }  if (playerTurn.Value == 2) {
            GameInfoMessage.Value = "Player 2 turn";
        } else if (playerTurn.Value == 1) {
            GameInfoMessage.Value = "Player 1 turn";
        }

        if (!IsGameStarted.Value)
        {
            IsGameStarted.Value = true;
            PowerBar.SetRun(true);
        }
    }
    
    public static void CallNextTurn()
    {
        var game = GameObject.Find("Terrain").GetComponent<Game>();
        game.NextTurn();
    }
    
    public void NextTurn()
    {
       // playerTurn.Value = playerTurn.Value == 1 ? 2 : 1;
       // GameInfoMessage.Value = "Player " + playerTurn.Value + " turn";
        // Set the shoot bar visible and set the position to the player position
        //var player = connectedClients[playerTurn.Value - 1];
        //setShootBarPosition(player);
        //var shootController = player.PlayerObject.GetComponent<ShootController>();
        //shootController.ShowShootBarServerRpc();
    }
    
    
    private void UpdatePlayerClient()
    {
        PowerBar.SetRun(true);
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
        
        // Update the game info message
        var gameInfoMessage = GameObject.FindWithTag("GameInfo");
        if (gameInfoMessage == null)
            return;
        var gameInfoMessageText = gameInfoMessage.GetComponent<TextMeshProUGUI>();
        if (gameInfoMessageText != null)
        {
            Debug.Log("GameInfoMessage: " + GameInfoMessage.Value);
            gameInfoMessageText.text = GameInfoMessage.Value.ToString();
        }
    }
    
    private void FixedUpdate()
    {
        
        //Say hello every 5 seconds below
        // if (!IsServer)
        //     return;
        // timer += Time.deltaTime;
        //
        // if (timer >= 3f)
        // {
        //     playerTurn.Value = playerTurn.Value == 1 ? 2 : 1;
        //     timer = 0f;
        // }
        
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