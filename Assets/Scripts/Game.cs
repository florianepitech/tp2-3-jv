using System;
using System.Collections.Generic;
using keyboard;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;



public class Game : NetworkBehaviour
{
    public static GameType gameType;
    public static IReadOnlyList<NetworkClient> connectedClients = new List<NetworkClient>();
    public static List<GameObject> obstacles = new List<GameObject>();
    public static NetworkVariable<int> PassedObstaclesPlayer1 = new(0);
    public static NetworkVariable<int> PassedObstaclesPlayer2 = new(0);
    private static NetworkVariable<bool> IsGameStarted = new(false);
    public static NetworkVariable<FixedString512Bytes> GameInfoMessage = new("");
    public static NetworkVariable<int> playerTurn = new(3);
    public static NetworkVariable<int> previousPlayerTurn = new(1);
    private float timer = 0f;
    public static NetworkVariable<bool> player1Shootings = new(false);
    public static NetworkVariable<bool> player2Shootings = new(false);
    private Vector3  cachedTransformPlayer1;
    private int transformCounterPlayer1 = 0;
    private Vector3  cachedTransformPlayer2;
    private int transformCounterPlayer2 = 0;
    
    private GameMusicManager _gameMusicManager;
    private VfxPool _vfxPool;
    public AudioClip jingleEndGame;

    // Start is called before the first frame update
    void Start()
    {
        _gameMusicManager = gameObject.GetComponent<GameMusicManager>();
        _vfxPool = gameObject.GetComponent<VfxPool>();
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
                Debug.Log("Try to join game: " + JoinGame.HostInputFieldValue + ":" + JoinGame.PortInputFieldValue + "...");
                networkManager.GetComponent<UnityTransport>().ConnectionData.Address = JoinGame.HostInputFieldValue;
                networkManager.GetComponent<UnityTransport>().ConnectionData.Port = ushort.Parse(JoinGame.PortInputFieldValue);
                bool resultConnect = networkManager.StartClient();
                if (!resultConnect)
                {
                    Debug.LogError("Failed to connect to server");
                    break;
                }
                Debug.Log("Connected to server");
                break;
            case GameType.HostGame:
                bool start = networkManager.StartHost();
                if (!start)
                {
                    Debug.LogError("Failed to start host");
                }
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
        // If P is pressed use VfxPool to spawn the Vfx at the position of the first player
        if (Input.GetKeyDown(KeyCode.P))
        {
            _vfxPool.SpawnStartGame(connectedClients[0].PlayerObject.transform.position);
          //  _vfxPool.SpawnEndGame(connectedClients[0].PlayerObject.transform.position);
        }
        if (IsServer || IsHost)
        {
            UpdatePlayerHost();
        } else if (IsClient)
        {
            UpdatePlayerClient();
        }

        if (IsLocalPlayer)
        {
            UpdateLocalPlayer();
        }

        UpdateAllPlayer();
        
    }


    private void UpdateLocalPlayer()
    {
        
        
    }
    

    private void UpdatePlayerHost()
    {
        CheckEndGameFinish();
        if (connectedClients.Count == 2 && playerTurn.Value == 3)
        {
            playerTurn.Value = 1;
            previousPlayerTurn.Value = 1;
        }
        
        if (connectedClients.Count != 2)
        {
            Debug.Log(connectedClients.Count);
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
    }

    private void UpdateAllPlayer()
    {
        var targetSwitchMusic = obstacles.Count - 1;
        if (PassedObstaclesPlayer1.Value == targetSwitchMusic || PassedObstaclesPlayer2.Value == targetSwitchMusic)
        {
            _gameMusicManager.PlayTrack2();
        }
        
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
        {
            HandleSwitchingTurnServerRpc();
            KeepPlayerInPlaceServerRpc();
            getPlayersConnectedServerRpc();
        }

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


    [ServerRpc]
    void KeepPlayerInPlaceServerRpc()
    {
        if (connectedClients.Count != 2)
            return;
        if (playerTurn.Value == 2)
        {
            Debug.Log("KeepPlayerInPlaceServerRpc");
            // //tp the player to the last position
             var player = connectedClients[0];
             var sphere = player.PlayerObject.transform.GetChild(0).gameObject;
             var sphereRigidbody = sphere.GetComponent<Rigidbody>();
             sphereRigidbody.transform.position = cachedTransformPlayer1;
             var clientID = player.ClientId;
            KeepPlayerInPlaceClientRpc(clientID, cachedTransformPlayer1);
        }
        
        if (playerTurn.Value == 1)
        {
            // //tp the player to the last position
             var player = connectedClients[1];
             var sphere = player.PlayerObject.transform.GetChild(0).gameObject;
             var sphereRigidbody = sphere.GetComponent<Rigidbody>();
             sphereRigidbody.transform.position = cachedTransformPlayer2;
             var clientID = player.ClientId;
            KeepPlayerInPlaceClientRpc(clientID, cachedTransformPlayer2);
        }
    }
    
    [ClientRpc]
    void KeepPlayerInPlaceClientRpc(ulong clientId, Vector3 transform)
    {
        // Find the player and its sphere based on clientId
        NetworkClient playerClient = null;
        foreach (var client in connectedClients)
        {
            if (client.ClientId == clientId)
            {
                playerClient = client;
                break;
            }
        }

        if (playerClient == null)
        {
            return;
        }

        var sphere = playerClient.PlayerObject.transform.GetChild(0).gameObject;
        var sphereRigidbody = sphere.GetComponent<Rigidbody>();

        // Stop the sphere's motion
        if (sphereRigidbody != null)
        {
            sphereRigidbody.velocity = Vector3.zero;
            sphereRigidbody.angularVelocity = Vector3.zero;
            sphereRigidbody.transform.position = transform;
        } else {
            Debug.Log("sphereRigidbody is null");
        }
    }
    
    [ServerRpc]
    void HandleSwitchingTurnServerRpc()
    {
        if (IsGameFinished)
        {
            return;
        }
        if (player1Shootings.Value)
        {
            //get the game object of the player
            var player = connectedClients[0];
            var sphere = player.PlayerObject.transform.GetChild(0).gameObject;
            var sphereRigidbody = sphere.GetComponent<Rigidbody>();
            //Debug.Log(sphereRigidbody.transform.position);
            //check if for 1 second the ball is not moving
            //get the transform of the sphere
            float positionTolerance = 0.2f;
            float distance = Vector3.Distance(sphereRigidbody.transform.position, cachedTransformPlayer1);

            if (distance <= positionTolerance)
            {
                transformCounterPlayer1++;
            }
            else
            {
                cachedTransformPlayer1 = sphereRigidbody.transform.position;
                transformCounterPlayer1 = 0;
            }
            if (transformCounterPlayer1 > 50)
            {
                player1Shootings.Value = false;
                playerTurn.Value = previousPlayerTurn.Value == 1 ? 2 : 1;
                previousPlayerTurn.Value = playerTurn.Value;
                transformCounterPlayer1 = 0;
            }
        }
        if (player2Shootings.Value)
        {
            //get the game object of the player
            var player = connectedClients[1];
            var sphere = player.PlayerObject.transform.GetChild(0).gameObject;
            var sphereRigidbody = sphere.GetComponent<Rigidbody>();
            //check if for 1 second the ball is not moving
            //get the transform of the sphere
            float positionTolerance = 0.2f;
            float distance = Vector3.Distance(sphereRigidbody.transform.position, cachedTransformPlayer2);

            if (distance <= positionTolerance)
            {
                transformCounterPlayer2++;
            }
            else
            {
                cachedTransformPlayer2 = sphereRigidbody.transform.position;
                transformCounterPlayer2 = 0;
            }
            if (transformCounterPlayer2 > 50)
            {
                player2Shootings.Value = false;
                playerTurn.Value = previousPlayerTurn.Value == 1 ? 2 : 1;
                previousPlayerTurn.Value = playerTurn.Value;
                transformCounterPlayer2 = 0;
            }
        }
    }
    
    // End Game Detection
    
    public bool IsGameFinished = false;

    private double _endGameTime = -1.0;

    private bool _hasSwitchedScene = false;

    private void CheckEndGameFinish()
    {
        int _numberOfObstacles = obstacles.Count;
        if (_numberOfObstacles <= 0)
        {
            Debug.Log("Impossible to find obstacles");
            return;
        }
        // Check if player 1 has win
        if (PassedObstaclesPlayer1.Value >= _numberOfObstacles && !IsGameFinished)
        {
            GameInfoMessage.Value = "Player 1 win";
            _endGameTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            IsGameFinished = true;
            playerTurn.Value = 0;
            PlayEndGameJingle();
            Debug.Log("Player 1 win");
        } else if (PassedObstaclesPlayer2.Value >= _numberOfObstacles && !IsGameFinished)
        {
            GameInfoMessage.Value = "Player 2 win";
            _endGameTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            IsGameFinished = true;
            playerTurn.Value = 0;
            PlayEndGameJingle();
            PlayParticleWin();
            Debug.Log("Player 2 win");
        }
        // check if 10 seconds are passed since the end of the game
        if (_endGameTime == -1.0)
            return;
        if (!(DateTimeOffset.Now.ToUnixTimeMilliseconds() - _endGameTime >= 10000))
            return;
        GameInfoMessage.Value = "";
        if (!_hasSwitchedScene)
            NetworkManager.SceneManager.LoadScene("HomeMenu", LoadSceneMode.Single);
        _hasSwitchedScene = true;
    }
    
    private void PlayEndGameJingle()
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = jingleEndGame;
        audioSource.loop = false;
        audioSource.volume = (float)MusicVolume.getMusicVolume(MusicType.VFX) / 100;
        audioSource.Play();
    }

    private void PlayParticleWin()
    {
        foreach (var player in connectedClients)
        {
            var position = player.PlayerObject.transform.position;
            _vfxPool.SpawnStartGame(position);
        }
    }
    
}