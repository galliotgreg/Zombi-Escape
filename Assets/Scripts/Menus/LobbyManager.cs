using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static LobbyManager instance = null;

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
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject cameraPrefab;
    [SerializeField]
    private GameObject[] lobbyPlayers = new GameObject[4];
    [SerializeField]
    private bool[] readyTab = { false, false, false, false };

    private CommonPanel commonPanel;

    public GameObject PlayerPrefab
    {
        get
        {
            return playerPrefab;
        }

        set
        {
            playerPrefab = value;
        }
    }

    public GameObject[] LobbyPlayers
    {
        get
        {
            return lobbyPlayers;
        }

        set
        {
            lobbyPlayers = value;
        }
    }

    public bool[] ReadyTab
    {
        get
        {
            return readyTab;
        }

        set
        {
            readyTab = value;
        }
    }

    public GameObject CameraPrefab
    {
        get
        {
            return cameraPrefab;
        }

        set
        {
            cameraPrefab = value;
        }
    }

    private bool isOnLobbyPhase = true;

    // Use this for initialization
    void Start () {
        commonPanel = GameObject.FindObjectOfType<CommonPanel>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isOnLobbyPhase && arePlayerReady())
        {
            isOnLobbyPhase = false;
            NavigationManager.instance.LoadScene(commonPanel.MapName);
        }
    }

    private bool arePlayerReady()
    {
        foreach (bool readyState in readyTab)
        {
            if (!readyState)
            {
                return false;
            }
        }
        return true;
    }
}
