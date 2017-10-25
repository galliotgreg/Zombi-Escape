using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour {

	[SerializeField]
	private GameObject player = null;
	// Name
	[SerializeField]
	private Text playerNameText = null;
	// Life
	[SerializeField]
	private GameObject playerLifeImage = null;
	// Bqttery
	[SerializeField]
	private GameObject playerBatteryImage = null;
	// Killed Zombies
	[SerializeField]
	private Text playerKilledZombiesText = null;
	// Bullets
	[SerializeField]
	private Text playerBulletsInGunText = null;
	[SerializeField]
	private Text playerBulletsOutGunText = null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			PlayerModel playerModel = player.GetComponent<PlayerModel>();
			// Name
			playerNameText.text = playerModel.PlayerName;
			// Life
			playerLifeImage.GetComponent<RectTransform>().anchorMax = new Vector2( playerModel.LifePoints/(float)playerModel.LifePoints_Max, playerLifeImage.GetComponent<RectTransform>().anchorMax.y );
			// Battery
			playerBatteryImage.GetComponent<RectTransform>().anchorMax = new Vector2( playerModel.LightBattery_current/(float)playerModel.LightBattery_max, playerBatteryImage.GetComponent<RectTransform>().anchorMax.y );
			// Killed Zombies
			playerKilledZombiesText.text = playerModel.PlayerKilledZombies.ToString();
			// Bullets
			playerBulletsInGunText.text = playerModel.NbBullets_in_gun.ToString();
			playerBulletsOutGunText.text = "/"+playerModel.NbBullets.ToString();
		} else {
			Debug.LogError ( "HUD : player not set" );
		}
	}

	public void setPlayer( GameObject newPlayer ){
		this.player = newPlayer;
	}
}
