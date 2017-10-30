using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroupModel{
	[SerializeField]
	private int nLifes = 5;
	[SerializeField]
	private int aidPerLife = 100;
	// Team Name
	private string teamName = "";
	// list of players
	ArrayList players = new ArrayList();

	// Perfoms the action of aid a player
	public void heal( PlayerBehaviour injuredPlayer, PlayerBehaviour helperPlayer )
	{
		if( this.nLifes > 0 )
		{
			injuredPlayer.beHealed( this.aidPerLife );
			helperPlayer.heal();
			this.nLifes--;
		}
	}
	// Perfoms the action of aid itself
	public void healItself( PlayerBehaviour injuredPlayer )
	{
		if( this.nLifes > 0 )
		{
			injuredPlayer.healItself();
			this.nLifes--;
		}
	}
	// Add a lifePoint to the Group
	public void obtainLife()
	{
		this.nLifes++;
	}

	public void setTeamName( string teamName )
	{
		this.teamName = teamName;
	}
	public void addPlayer( PlayerBehaviour player )
	{
		this.players.Add( player );
	}

	public ArrayList Players {
		get {
			return players;
		}
	}

	public int getNlifes()
	{
		return this.nLifes;
	}
	public float getPartialScore()
	{
		float score = 0;
		foreach( PlayerBehaviour p in this.players )
		{
			score += p.Model.getPlayerPartialScore();
		}
		return score;
	}
	public float getFinalScore()
	{
		float score = 0;
		foreach( PlayerBehaviour p in this.players )
		{
			score += p.Model.getPlayerFinalScore();
		}
		return score;
	}
	public int getBulletsFired()
	{
		int result = 0;
		foreach( PlayerBehaviour p in this.players )
		{
			result += p.Model.STAT_bulletsFired;
		}
		return result;
	}
	public int getBulletsHit()
	{
		int result = 0;
		foreach( PlayerBehaviour p in this.players )
		{
			result += p.Model.STAT_bulletsHits;
		}
		return result;
	}
	public float getPrecision()
	{
		return (getBulletsFired()>0?getBulletsHit()/(float)getBulletsFired():0);
	}
	public int getUsedBatteries()
	{
		int result = 0;
		foreach( PlayerBehaviour p in this.players )
		{
			result += p.Model.STAT_batteryUsed;
		}
		return result;
	}
	public int getUsedLifes()
	{
		int result = 0;
		foreach( PlayerBehaviour p in this.players )
		{
			result += p.Model.STAT_selfHeal + p.Model.STAT_healedSomeone;
		}
		return result;
	}
	public int getKilledZombies()
	{
		int result = 0;
		foreach( PlayerBehaviour p in this.players )
		{
			result += p.Model.PlayerKilledZombies;
		}
		return result;
	}
	public string getTeamName()
	{
		return this.teamName;
	}
}
