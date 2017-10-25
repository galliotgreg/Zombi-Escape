using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetectionBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setColor( Color color ){
		this.GetComponent<SpriteRenderer> ().color = color;
	}
}
