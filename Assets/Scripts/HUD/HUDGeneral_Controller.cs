﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDGeneral_Controller : MonoBehaviour {

	private PlayerGroupModel playerGroup = null;
	private WavesManager wavesManager = null;

	// Heals
	[SerializeField]
	private Text playerGroupHealsText = null;
	[SerializeField]
	private Text playerTeamNameText = null;
	[SerializeField]
	private Text playerGroupWaveTimeText = null;
	[SerializeField]
	private Text playerGroupWaveAmountText = null;
	[SerializeField]
	private Text playerGroupScoreText = null;
	[SerializeField]
	private Text playerGroupHighScoreText = null;
	[SerializeField]
	private Text playerGroupHighScoreTeamText = null;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (playerGroup != null) {
			// Lifes
			playerGroupHealsText.text = this.playerGroup.getNlifes().ToString();
			// TeamName
			playerTeamNameText.text = this.playerGroup.getTeamName();
			// Wave
			playerGroupWaveTimeText.text = ((int)(this.wavesManager.getNextWaveTime()/60)).ToString("00")+":"+((int)(this.wavesManager.getNextWaveTime()%60)).ToString("00");
			playerGroupWaveAmountText.text = ((int)this.wavesManager.getNextWaveAmountZombies ()).ToString();
			// Score
			playerGroupScoreText.text = ((int)this.playerGroup.getTotalScore()).ToString();
			// HighScore
			playerGroupHighScoreText.text = ((int)PlayerPrefs.GetInt(GameState.HighScorePlayerPrefName,0)).ToString();
			playerGroupHighScoreTeamText.text = PlayerPrefs.GetString(GameState.HighScoreTeamPlayerPrefName,"-");
		} else {
			Debug.LogError ( "HUD : playerGroup not set" );
		}
	}

	public void setPlayerGroup( PlayerGroupModel newPlayerGroup ){
		this.playerGroup = newPlayerGroup;
	}
	public void setWavesManager( WavesManager newWavesManager ){
		this.wavesManager = newWavesManager;
	}
}