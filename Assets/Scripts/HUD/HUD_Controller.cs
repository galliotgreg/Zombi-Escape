using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour {

	[SerializeField]
	private PlayerModel player = null;
	[SerializeField]
	private Text playerLife = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		playerLife.text = "Life: "+player.LifePoints.ToString();
	}
}
