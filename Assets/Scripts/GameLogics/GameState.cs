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

	// Camera Manager
	private MultiCameraManager cameraManager;	// Controls the generation of multiple cameras
	[SerializeField]
	private MultiCameraManager cameraManagerPrefab;	// prefab for camera Manager

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
        LobbyManager lobbyManager = LobbyManager.instance;
        if (lobbyManager != null)
        {         // Come from Lobby
            setupPlayers();
            setupCameras();
            Destroy(lobbyManager);
        }
        else                              // Stand Alone Scene
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
    }

    private void setupCameras()
    {
		if( this.cameraManagerPrefab != null ){
			// Generate Manager based on prefab
			this.cameraManager = GameObject.Instantiate( this.cameraManagerPrefab, this.transform );
			// Set players to be tracked
			this.cameraManager.setPlayers( new ArrayList( this.players ) );
		} else {
			Debug.LogError("Camera Manager prefab not found");
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
