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
	private UnityEngine.UI.Dropdown[] controlDropdowns;
	private string teamName = "";

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

	public string TeamName {
		get {
			return teamName;
		}
	}

    private bool isOnLobbyPhase = true;

    // Use this for initialization
    void Start () {
        commonPanel = GameObject.FindObjectOfType<CommonPanel>();
		GameObject.Find("btn_start").GetComponent<UnityEngine.UI.Button>().interactable = false;
	}

	public void playersReady(){
		PlayerPanel[] playerPanels = GameObject.FindObjectsOfType<PlayerPanel>();
		foreach( PlayerPanel p in playerPanels ){
			p.SetReady();
		}
		NavigationManager.instance.LoadScene(commonPanel.MapName);
		this.teamName = GameObject.Find ("txt_teamName").GetComponent<UnityEngine.UI.InputField> ().text;
	}
	
	// Update is called once per frame
	void Update () {
		// Verify if some player is enabled
		if( somePlayerEnabled() ){
			GameObject.Find("btn_start").GetComponent<UnityEngine.UI.Button>().interactable = true;
		}
    }

	private bool somePlayerEnabled()
	{
		foreach (UnityEngine.UI.Dropdown dropd in controlDropdowns)
		{
			if ( dropd != null && dropd.value != 0 )
			{
				return true;
			}
		}
		return false;
	}

    public void Kill()
    {
        Destroy(this);
    }
}
