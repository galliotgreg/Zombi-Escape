using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour {

	[SerializeField]
	private GameObject player = null;
	[SerializeField]
	private GameObject playerLifeImage = null;
	[SerializeField]
	private Text playerLifeCounterText = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			PlayerModel playerModel = player.GetComponent<PlayerModel>();
			playerLifeImage.GetComponent<RectTransform>().anchorMax = new Vector2( playerModel.LifePoints/(float)playerModel.LifePoints_Max, playerLifeImage.GetComponent<RectTransform>().anchorMax.y );
		} else {
			Debug.LogError ( "HUD : player not set" );
		}
	}

	public void setPlayer( GameObject newPlayer ){
		this.player = newPlayer;
	}
}
