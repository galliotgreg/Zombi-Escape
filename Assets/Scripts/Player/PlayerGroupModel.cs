using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroupModel{
	[SerializeField]
	private int nLifes = 5;
	[SerializeField]
	private int aidPerLife = 50;
	private float score = 0;

	// Perfoms the action of aid a player
	public void heal( PlayerBehaviour injuredlayer, PlayerBehaviour helperPlayer )
	{
		if( this.nLifes > 0 )
		{
			injuredlayer.beHealed( this.aidPerLife );
			helperPlayer.heal();
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

	public int getNlifes()
	{
		return this.nLifes;
	}
	public float getScore()
	{
		return this.score;
	}
}
