using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static GameType gameType;

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
                Debug.LogError("Invalid game type");
                throw new ArgumentOutOfRangeException();
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
        // Check for shoot bar
        spawnShootBar();
    }

    private void spawnShootBar()
    {
        // Get the ShootBar cylinder
        var shootBar = GameObject.Find("ShootBar");
        if (shootBar == null)
        {
            Debug.Log("ShootBar not found");
            return;
        }
        float rotation = KeyboardEvent.TurnCrossHairDirection();
        shootBar.transform.Rotate(0, 0, rotation);
    }
    
}