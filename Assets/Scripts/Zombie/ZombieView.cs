using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieView : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void die()
    {
        //Debug.Log("TODO : Trigger die sprite animation");
    }

    public void moveFwd()
    {
        anim.SetBool("isMoving", true);
    }

    public void turnLeft()
    {        
    }

    public void turnRight()
    {        
    }

    public void hitPlayer(PlayerBehaviour player)
    {
        anim.SetTrigger("isAttackingPlayer");
    }

    public void dealDamage(float damages)
    {        
        //Debug.Log("Hit : " + this.name);
    }
}
