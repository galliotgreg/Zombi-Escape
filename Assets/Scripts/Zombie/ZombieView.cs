using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieView : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer zombieRenderer;
    public ParticleSystem zombieBlood;
    public GameObject zombieDeath;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void die()
    {
        //Explosion du zombie
        SoundManager.instance.zombieExplosion();
        Instantiate(zombieDeath, transform.position, transform.rotation);
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
        // Affiche les particules de sang        
        zombieBlood.gameObject.SetActive(true);

        StartCoroutine(waitAndDisable(zombieBlood.gameObject, 0.6f));
        //Debug.Log("Hit : " + this.name);
    }

    IEnumerator waitAndDisable(GameObject gObject, float time)
    {
        yield return new WaitForSeconds(time);
        gObject.SetActive(false);
    }

    public void walkClip()
    {
        SoundManager.instance.zombieWalk();
    }
    public void attackClip()
    {
        SoundManager.instance.zombieAttack();
    }
}
