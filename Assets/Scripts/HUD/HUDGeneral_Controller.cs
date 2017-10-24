using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDGeneral_Controller : MonoBehaviour {

	private PlayerGroupModel playerGroup = null;

	// Heals
	[SerializeField]
	private Text playerGroupHealsText = null;
	[SerializeField]
	private Text playerGroupTimeText = null;
	[SerializeField]
	private Text playerGroupScoreText = null;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (playerGroup != null) {
			playerGroupHealsText.text = this.playerGroup.getNlifes().ToString();
		} else {
			Debug.LogError ( "HUD : playerGroup not set" );
		}
	}

	public void setPlayerGroup( PlayerGroupModel newPlayerGroup ){
		this.playerGroup = newPlayerGroup;
	}
}
