using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    private PlayerModel model = null;
    private PlayerView view = null;

    private float hitCooldown = -1;

    // Use this for initialization
    void Start()
    {
        this.model = this.gameObject.GetComponent<PlayerModel>();
        this.view = this.gameObject.GetComponent<PlayerView>();
    }

    // Update is called once per frame
    void Update()
    {
        handleDeath();
    }

    private void handleDeath()
    {
        if (this.model.LifePoints <= 0)
        {
            this.view.die();
            this.model.die();
        }
    }

    public void moveFwd()
    {
        this.model.moveFwd();
        this.view.moveFwd();
    }

    public void moveBck()
    {
        this.model.moveBck();
        this.view.moveBck();
    }

    public void turnLeft()
    {
        this.model.turnLeft();
        this.view.turnLeft();
    }

    public void turnRight()
    {
        this.model.turnRight();
        this.view.turnRight();
    }

    public void handleFire()
    {
        if (hitCooldown < 0)  
        {
            this.model.fire();
            this.view.fire();

            hitCooldown = this.model.HitRate;
        } else
        {
            hitCooldown -= Time.deltaTime;
        }
    }

    public void handleDealDamage(float damages)
    {
        this.model.dealDamage(damages);
        this.view.dealDamage(damages);
    }
}
