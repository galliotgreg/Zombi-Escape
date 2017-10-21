﻿using System;
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
    void Start () {

        this.players = GameObject.FindGameObjectsWithTag("Player");
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
