using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void die()
    {
        Debug.Log("TODO : Trigger die sprite animation");
    }

    public void moveFwd()
    {
        Debug.Log("TODO : Trigger move forward sprite animation");
    }

    public void turnLeft()
    {
        Debug.Log("TODO : Trigger turn left sprite animation");
    }

    public void turnRight()
    {
        Debug.Log("TODO : Trigger turn right sprite animation");
    }

    public void hitPlayer(PlayerBehaviour player)
    {
        Debug.Log("TODO : Trigger hit player sprite animation");
    }

    public void dealDamage(float damages)
    {
        Debug.Log("Hit : " + this.name);
    }
}
