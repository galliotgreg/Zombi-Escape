using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractPoint : MonoBehaviour {

    private int nbPlayersInRange = 0;
    private float range = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        updatePlayersInRange();

		if (this.nbPlayersInRange == GameState.instance.NbPlayers)
        {
            NavigationManager.instance.LoadScene("victoryScene");
        }
	}

    private void updatePlayersInRange()
    {
        int playersInRange = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < this.range)
            {
                playersInRange++;
            }
        }

        this.nbPlayersInRange = playersInRange;
    }
}
