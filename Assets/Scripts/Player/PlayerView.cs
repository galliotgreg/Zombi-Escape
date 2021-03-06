﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {

    private Animator anim;    
    public ParticleSystem gunFire;
    public ParticleSystem playerBlood;
    public ParticleSystem playerHealed;

    private GunFlash gunFlash;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gunFlash = GetComponentInChildren<GunFlash>();
    }

    private void Start()
    {
       SoundManager.instance.inGameAmbienceClip();        
    }

    public void fire()
    {
        anim.SetBool("isShooting", true);
        Debug.Log("is Shooting True");
        //Enable the spaming for space button, no need to wait the end of the shotting animation
        anim.Play("Shoot", -1, 0f);

        // Affiche les particules
        gunFire.gameObject.SetActive(true);
        // Joue la piste sonore

        //audioSource.PlayOneShot(gunShot);
        SoundManager.instance.handGunShot();

        // Affiche le flash
        gunFlash.Flash();
    }

    public void fireFail()
    {
        SoundManager.instance.handGunShotFail();
    }

    public void StopFire()
    {
        anim.SetBool("isShooting", false);
        Debug.Log("isShooting False");
        // Désactive les particules
        gunFire.gameObject.SetActive(false);

    }

    public void reload()
    {    
        //Todo : Eviter le spam
        //audioSource.PlayOneShot(reloadSound);
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

    public void straffLeft()
    {
        anim.SetBool("isWalking", true);
    }

    public void straffRight()
    {
        anim.SetBool("isWalking", true);
    }

    public void die()
    {
        anim.SetBool("isDead", true);
        SoundManager.instance.playeurDeath();
        //Debug.Log("TODO : Trigger die sprite animation");
    }

    public void dealDamage(float damages)
    {

        Debug.Log("Player blood particle");
        // Affiche les particules de sang        
        playerBlood.gameObject.SetActive(true);

        if (playerBlood.gameObject.active) {
            Debug.Log("Player blood particle is active");
        }
        StartCoroutine(waitAndDisable(playerBlood.gameObject));

        //Debug.Log("Hit : " + this.name);
    }

    IEnumerator waitAndDisable(GameObject gObject)
    {
        yield return new WaitForSeconds(0.6f);
        gObject.SetActive(false);
        if (!gObject.active)
        {
            Debug.Log("System particle "+gObject.name +" is not active");
        }
    }

    public void playHandGunReload()
    {
        SoundManager.instance.handGunReload();
    }

	public void beHealed( float aidAmount )
	{
        // Call animation
        anim.SetTrigger("isHealed");
        anim.SetBool("isDead", false);
        playerHealed.Play();

        SoundManager.instance.healing();
	}
	public void heal()
	{
		// Call animation
	}

	// Items
	// Heal
	public void obtainItemHeal(){
        SoundManager.instance.seringuePickup();
    }
	// Bullets
	public void obtainItemBullets(){
        SoundManager.instance.chargerPickup();
    }
	// Batery
	public void obtainItemBattery(){
        SoundManager.instance.batteryPickup();
    }

    //Switch on / off the light
    public void toggleLight()
    {
        SoundManager.instance.switchLight();
    }

	public void updatePlayerId( int playerId )
	{
		// Put a color distinguish the players
		Color playerColor = new Color ( (playerId==1||playerId==3?1:0.6f), (playerId==2||playerId==3?1:0.6f), (playerId==0?1:0.6f));
		GetComponent<SpriteRenderer> ().material.color = playerColor;
		GetComponentInChildren<PlayerFeetView>().gameObject.GetComponent<SpriteRenderer> ().material.color = playerColor;
		GetComponentInChildren<RadarDetectionBehaviour> ().setColor (playerColor);
	}
}
