using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroupModel{
	[SerializeField]
	private int nLifes = 5;
	[SerializeField]
	private int aidPerLife = 100;
	private float score = 0;
	private float scorePerTime = 0;
	private string teamName = "";

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

	public void addScore(float scoreToAdd){
		this.score += scoreToAdd;
	}
	public void reduceScore(float scoreToReduce){
		this.score = Mathf.Max( this.score-scoreToReduce, 0 );
	}
	public void addScorePerTime(float scorePerTimeToAdd){
		this.scorePerTime += scorePerTimeToAdd;
	}
	public void setTeamName( string teamName )
	{
		this.teamName = teamName;
	}

	public int getNlifes()
	{
		return this.nLifes;
	}
	public float getScore()
	{
		return this.score;
	}
	public float getScorePerTime()
	{
		return this.scorePerTime;
	}
	public float getTotalScore()
	{
		return this.score+this.scorePerTime;
	}
	public string getTeamName()
	{
		return this.teamName;
	}
}
