using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {

    private Animator anim;    

    private void Awake()
    {
        anim = GetComponent<Animator>();             
    }

    public void fire()
    {
        anim.SetBool("isShooting", true);
        //Enable the spaming for space button, no need to wait the end of the shotting animation
        anim.Play("Shoot", -1, 0f);        
    }

    public void StopFire()
    {
        anim.SetBool("isShooting", false);
    }

    public void reload()
    {
        anim.SetBool("isReloading", true);
    }

    public void StopReload()
    {
        anim.SetBool("isReloading", false);
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

    public void die()
    {
        Debug.Log("TODO : Trigger die sprite animation");
        //Debug.Log("TODO : Trigger die sprite animation");
    }

    public void dealDamage(float damages)
    {
        Debug.Log("TODO : Trigger Blood Particle Effect");
        //Debug.Log("TODO : Trigger Blood Particle Effect");
    }
}
