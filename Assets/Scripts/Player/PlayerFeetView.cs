using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetView : MonoBehaviour {

    private Animator anim;    

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void walkingClip()
    {
        SoundManager.instance.playeurWalk();
    }

    public void moveFwd()
    {
        anim.SetBool("isWalking", true);
    }

    public void moveBck()
    {
        anim.SetBool("isWalking", true);
    }

    public void turnLeft()
    {
        anim.SetBool("isWalking", true);
    }

    public void turnRight()
    {
        anim.SetBool("isWalking", true);
    }

    public void idle()
    {        
        anim.SetBool("isWalking", false);
    }

    public void straffLeft()
    {
        anim.SetBool("isWalking", true);
    }

    public void straffRight()
    {
        anim.SetBool("isWalking", true);
    }
}
