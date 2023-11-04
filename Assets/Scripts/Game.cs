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
            case GameType.JOIN_GAME:
                networkManager.GetComponent<UnityTransport>().ConnectionData.Address = JoinGame.HostInputFieldValue;
                // networkManager.GetComponent<UnityTransport>().ConnectionData.Port = ushort.Parse(JoinGame.PortInputFieldValue);
                networkManager.StartClient();
                break;
            case GameType.HOST_GAME:
                networkManager.StartHost();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var openPauseMenu = KeyboardEvent.IsEscape();
        if (openPauseMenu)
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
    }
}