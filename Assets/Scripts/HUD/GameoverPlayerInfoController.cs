using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverPlayerInfoController : MonoBehaviour {

	PlayerBehaviour player;

	public PlayerBehaviour Player {
		get {
			return player;
		}
		set {
			player = value;
			updateValues();
		}
	}

	// Player
	public Text playerName = null;
	public Text playerPartialScore = null;
	public Text playerFinalScore = null;
	// Team stats
	public Text playerBullets = null;
	public Text playerPrecision = null;
	public Text playerPrecisionMultiplier = null;
	public Text playerBatteries = null;
	public Text playerBatteriesTime = null;
	public Text playerDied = null;
	public Text playerDiedMultiplier = null;
	public Text playerZombies = null;
	public Text playerZombiesMultiplier = null;
	public Text playerHero = null;
	public Text playerHeroMultiplier = null;
	public Text playerSaved = null;
	public Text playerAutoHeal = null;
	public Text playerAutoHealMultiplier = null;
	// Image to change background color
	public Image background;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateValues(){
		PlayerModel pModel = player.Model;

		// Updating HighScore
		if( PlayerPrefs.GetInt( GameState.PlayerHighScorePlayerPrefName, 0 ) < ((int)pModel.getPlayerFinalScore()) )
		{
			PlayerPrefs.SetInt( GameState.PlayerHighScorePlayerPrefName, ((int)pModel.getPlayerFinalScore()) );
			PlayerPrefs.SetString( GameState.PlayerHighScoreNamePlayerPrefName, pModel.PlayerName );
		}

		// Player Color
		Color playerColor = new Color ( (pModel.PlayerId==1||pModel.PlayerId==3?1:0.6f), (pModel.PlayerId==2||pModel.PlayerId==3?1:0.6f), (pModel.PlayerId==0?1:0.6f), 0.9f);
		background.color = playerColor;
		// Score
		playerName.text = pModel.PlayerName;
		playerPartialScore.text = Mathf.FloorToInt(pModel.getPlayerPartialScore()).ToString();
		playerFinalScore.text = Mathf.FloorToInt(pModel.getPlayerFinalScore()).ToString();
		// Multiplier Color
		Color plus = new Color(0f,0.2f,0f);
		Color minus = new Color(0.2f,0f,0f);
		// Stats
		playerBullets.text = pModel.STAT_bulletsFired.ToString();
		// precision
		float precision = (pModel.STAT_bulletsFired>0?pModel.STAT_bulletsHits/(float)pModel.STAT_bulletsFired:0);
		playerPrecision.text = Mathf.FloorToInt(precision*100).ToString()+"%";
		playerPrecisionMultiplier.text = "(x 1000) = "+(1000*precision).ToString();
		if( precision > 0 ){
			playerPrecisionMultiplier.color = plus;
		}
		// Battery
		playerBatteries.text = pModel.STAT_batteryUsed.ToString();
		playerBatteriesTime.text = Mathf.FloorToInt(pModel.STAT_batteryTime/60).ToString()+":"+Mathf.FloorToInt(pModel.STAT_batteryTime%60).ToString("00");
		// Killed
		playerDied.text = pModel.STAT_nbDead.ToString();
		playerDiedMultiplier.text = "(x -250) = "+(-250*pModel.STAT_nbDead).ToString();
		if( pModel.STAT_nbDead > 0 ){
			playerDiedMultiplier.color = minus;
		}
		playerZombies.text = pModel.PlayerKilledZombies.ToString();
		playerZombiesMultiplier.text = "(x 100) = "+(100*pModel.PlayerKilledZombies).ToString();
		if( pModel.PlayerKilledZombies > 0 ){
			playerZombiesMultiplier.color = plus;
		}
		playerHero.text = pModel.STAT_healedSomeone.ToString();
		playerHeroMultiplier.text = "(x 150) = "+(150*pModel.STAT_healedSomeone).ToString();
		if( pModel.STAT_healedSomeone > 0 ){
			playerHeroMultiplier.color = plus;
		}
		playerSaved.text = pModel.STAT_beHealed.ToString();
		playerAutoHeal.text = pModel.STAT_selfHeal.ToString();
		playerAutoHealMultiplier.text = "(x -50) = "+(-50*pModel.STAT_selfHeal).ToString();
		if( pModel.STAT_selfHeal > 0 ){
			playerAutoHealMultiplier.color = minus;
		}
	}
}
