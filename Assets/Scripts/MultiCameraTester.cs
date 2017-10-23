using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCameraTester : MonoBehaviour {

	// TODO THIS CLASS IS USED EXCLUSIVELY TO TEST THE MULTICAMERA SCRIPT. PLEASE REMOVE IT AFTER TESTING PHASE
	[SerializeField]
	private GameObject[] players;
	[SerializeField]
	private MultiCamera_Controller multiCamera;

	// Use this for initialization
	void Start () {
		ArrayList p = new ArrayList();
		foreach( GameObject g in players ){
			p.Add(g);
		}
		multiCamera.setPlayers( p );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
