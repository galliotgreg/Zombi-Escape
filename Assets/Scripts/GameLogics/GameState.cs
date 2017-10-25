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

	private ArrayList players;				// players' gameobjects

	// Group Values
	private PlayerGroupModel playerGroup;

	// Camera Manager
	private GameObject cameraManager;	// Controls the generation of multiple cameras
	[SerializeField]
	private GameObject cameraManagerPrefab;	// prefab for camera Manager

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

    public ArrayList Players
    {
        get
        {
            return players;
        }

        set
        {
            players = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        LobbyManager lobbyManager = LobbyManager.instance;
		this.playerGroup = new PlayerGroupModel();

        if (lobbyManager != null)
        {         // Come from Lobby
            setupPlayers();
            setupCameras();
            Destroy(lobbyManager);
        }
        else                              // Stand Alone Scene
        {
			Players = new ArrayList(GameObject.FindGameObjectsWithTag("Player"));
			nbPlayers = Players.Count;
			setupCameras();
        }
    }

    private void setupCameras()
    {
		if( this.cameraManagerPrefab != null ){
			// Generate Manager based on prefab
			this.cameraManager = GameObject.Instantiate( this.cameraManagerPrefab, this.transform );

			// set PlayerGroup to general HUD
			this.cameraManager.GetComponentInChildren<HUDGeneral_Controller>().setPlayerGroup( this.playerGroup );

			// Set players to be tracked
			MultiCameraManager cManager = this.cameraManager.GetComponent<MultiCameraManager>();
			if( cManager != null ){
				ArrayList validPlayers = new ArrayList();
				// Checking valid players
				foreach( GameObject player in this.Players ){
					if( player != null && player.GetComponent<PlayerKeyBoardInput>().KeyMapping != InputManager.KeyMapping.Disabled ){
						validPlayers.Add(player);
					}
				}
				cManager.setPlayers( validPlayers );
			}
			else{
				Debug.LogError("Camera Manager does not has a MultiCameraManager");
			}
		} else {
			Debug.LogError("Camera Manager prefab not found");
		}
    }

    private void setupPlayers()
    {
        PlayerSpawner[] playerSpawners = GameObject.FindObjectsOfType<PlayerSpawner>();
		this.Players = new ArrayList();
        foreach (PlayerSpawner spawner in playerSpawners)
        {
            //Create the player according to the values of the lobbyPlayer
            LobbyPlayer lobbyPlayer = LobbyManager.instance.LobbyPlayers[spawner.PlayerId].GetComponent<LobbyPlayer>();
			// Creating only enabled players
			if( lobbyPlayer.KeyMap != InputManager.KeyMapping.Disabled ){
	            GameObject player = Instantiate(LobbyManager.instance.PlayerPrefab);
	            PlayerKeyBoardInput input = player.GetComponent<PlayerKeyBoardInput>();
	            input.PlayerId = lobbyPlayer.PlayerId;
	            input.KeyMapping = lobbyPlayer.KeyMap;

				// Associating groupPlayer
				player.GetComponent<PlayerBehaviour>().PlayerGroup = this.playerGroup;
				player.GetComponent<PlayerBehaviour>().setPlayerId( lobbyPlayer.PlayerId );
				player.GetComponent<PlayerBehaviour>().setPlayerName( lobbyPlayer.PlayerName );

	            //Locate the player
	            player.transform.position = spawner.transform.position;
				this.Players.Add( player );
			}
        }
		this.nbPlayers = this.Players.Count;
    }

    // Update is called once per frame
    void Update () {
        handleGameOver();
	}

    private void handleGameOver()
    {
        PlayerModel playerModel;
        foreach (GameObject player in  Players)
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
