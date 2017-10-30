using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneController : MonoBehaviour {

	// GameManager
	public NavigationManager navigation = null;

	// High Score - Team
	public Text groupHighScoreName = null;
	public Text groupHighScoreValue = null;
	// High Score - Player
	public Text playerHighScoreName = null;
	public Text playerHighScoreValue = null;

	// Team
	public Text teamName = null;
	public Text teamScore = null;
	// Team stats
	public Text teamBullets = null;
	public Text teamPrecision = null;
	public Text teamBatteries = null;
	public Text teamLifes = null;
	public Text teamZombies = null;

	// player Info prefab to be instantiated for each player
	public GameObject playerInfoPrefab = null;
	public GameObject playerInfoContainer = null;

	// Use this for initialization
	void Start () {
		// Updating Values
		updateValues();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void clearHighScores()
	{
		PlayerPrefs.DeleteKey( GameState.GroupHighScorePlayerPrefName );
		PlayerPrefs.DeleteKey( GameState.GroupHighScoreTeamPlayerPrefName );
		PlayerPrefs.DeleteKey( GameState.PlayerHighScorePlayerPrefName );
		PlayerPrefs.DeleteKey( GameState.PlayerHighScoreNamePlayerPrefName );

		updateValuesHighScore();
	}
	public void quitScene()
	{
		// Destroy Game State
		Destroy( GameoverState.instance.gameObject );
		// Loading Menu
		navigation.LoadScene("menuScene");
	}

	public void updateValuesHighScore(){
		// High Values
		groupHighScoreValue.text = PlayerPrefs.GetInt( GameState.GroupHighScorePlayerPrefName, 0 ).ToString();
		groupHighScoreName.text = PlayerPrefs.GetString( GameState.GroupHighScoreTeamPlayerPrefName, "" );
		playerHighScoreValue.text = PlayerPrefs.GetInt( GameState.PlayerHighScorePlayerPrefName, 0 ).ToString();
		playerHighScoreName.text = PlayerPrefs.GetString( GameState.PlayerHighScoreNamePlayerPrefName, "" );
	}

	void updateValues()
	{
		PlayerGroupModel playerGroup = GameoverState.instance.PlayerGroup;

		// Evaluating High Score
		if( PlayerPrefs.GetInt( GameState.GroupHighScorePlayerPrefName, 0 ) < ((int)playerGroup.getFinalScore()) )
		{
			PlayerPrefs.SetInt( GameState.GroupHighScorePlayerPrefName, ((int)playerGroup.getFinalScore()) );
			PlayerPrefs.SetString( GameState.GroupHighScoreTeamPlayerPrefName, playerGroup.getTeamName() );
		}

		// Team
		teamName.text = playerGroup.getTeamName();
		teamScore.text = ((int)playerGroup.getFinalScore()).ToString();
		// Team stats
		teamBullets.text = playerGroup.getBulletsFired().ToString();
		teamPrecision.text = Mathf.FloorToInt(playerGroup.getPrecision()*100).ToString()+"%";
		teamBatteries.text = playerGroup.getUsedBatteries().ToString();
		teamLifes.text = playerGroup.getUsedLifes().ToString();
		teamZombies.text = playerGroup.getKilledZombies().ToString();

		// Generating children
		foreach( PlayerBehaviour player in playerGroup.Players )
		{
			GameObject pInfo = Instantiate( playerInfoPrefab, playerInfoContainer.transform );
			pInfo.GetComponent<GameoverPlayerInfoController>().Player = player;
		}
		updateValuesHighScore();
	}
}
