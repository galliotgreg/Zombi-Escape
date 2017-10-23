using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static GameState instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private int nbPlayers = 1;

    private GameObject[] players;

    public int NbPlayers
    {
        get
        {
            return nbPlayers;
        }

        set
        {
            nbPlayers = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        setupPlayers();
        setupCameras();
        Destroy(LobbyManager.instance);
    }

    private void setupCameras()
    {
        if (players.Length == 4)
        {
            GameObject cameraObject = Instantiate(LobbyManager.instance.CameraPrefab);
            CameraTracking cameraTracking = cameraObject.GetComponent<CameraTracking>();
            cameraTracking.TrackedPlayer = players[0];
            Camera camera = cameraObject.GetComponent<Camera>();
            camera.rect = new Rect(0, 0, 0.499f, 0.499f);
            AudioListener audioListener = cameraObject.GetComponent<AudioListener>();
            audioListener.enabled = false;

            cameraObject = Instantiate(LobbyManager.instance.CameraPrefab);
            cameraTracking = cameraObject.GetComponent<CameraTracking>();
            cameraTracking.TrackedPlayer = players[1];
            camera = cameraObject.GetComponent<Camera>();
            camera.rect = new Rect(0.5f, 0, 0.499f, 0.499f);
            audioListener = cameraObject.GetComponent<AudioListener>();
            audioListener.enabled = false;

            cameraObject = Instantiate(LobbyManager.instance.CameraPrefab);
            cameraTracking = cameraObject.GetComponent<CameraTracking>();
            cameraTracking.TrackedPlayer = players[2];
            camera = cameraObject.GetComponent<Camera>();
            camera.rect = new Rect(0.5f, 0.5f, 0.499f, 0.499f);
            audioListener = cameraObject.GetComponent<AudioListener>();
            audioListener.enabled = false;

            cameraObject = Instantiate(LobbyManager.instance.CameraPrefab);
            cameraTracking = cameraObject.GetComponent<CameraTracking>();
            cameraTracking.TrackedPlayer = players[3];
            camera = cameraObject.GetComponent<Camera>();
            camera.rect = new Rect(0, 0.5f, 0.499f, 0.499f);
        }
    }

    private void setupPlayers()
    {
        PlayerSpawner[] playerSpawners = GameObject.FindObjectsOfType<PlayerSpawner>();
        this.players = new GameObject[playerSpawners.Length];
        foreach (PlayerSpawner spawner in playerSpawners)
        {
            //Create the player according to the values of the lobbyPlayer
            LobbyPlayer lobbyPlayer = LobbyManager.instance.LobbyPlayers[spawner.PlayerId].GetComponent<LobbyPlayer>();
            GameObject player = Instantiate(LobbyManager.instance.PlayerPrefab);
            PlayerKeyBoardInput input = player.GetComponent<PlayerKeyBoardInput>();
            input.PlayerId = lobbyPlayer.PlayerId;
            input.KeyMapping = lobbyPlayer.KeyMap;

            //Locate the player
            player.transform.position = spawner.transform.position;
            this.players[spawner.PlayerId] = player;
        }
        this.nbPlayers = this.players.Length;
    }

    // Update is called once per frame
    void Update () {
        handleGameOver();
	}

    private void handleGameOver()
    {
        PlayerModel playerModel;
        foreach (GameObject player in  players)
        {
            playerModel = player.GetComponent<PlayerModel>();
            if (playerModel.LifePoints > 0)
            {
                return;
            }
        }

        NavigationManager.instance.LoadScene("gameOverScene");
    }
}
