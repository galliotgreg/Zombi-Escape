using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour {
    [SerializeField]
    private int playerId;
    [SerializeField]
    private InputManager.KeyMapping keyMap;

    public int PlayerId
    {
        get
        {
            return playerId;
        }

        set
        {
            playerId = value;
        }
    }

    public InputManager.KeyMapping KeyMap
    {
        get
        {
            return keyMap;
        }

        set
        {
            keyMap = value;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
