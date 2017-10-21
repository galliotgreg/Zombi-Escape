using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    private PlayerModel model = null;
    private PlayerView view = null;
    private PlayerFeetView feetView = null;

    [SerializeField]
    private float hitCooldown = -1;

    // Use this for initialization
    void Start()
    {
        this.model = this.gameObject.GetComponent<PlayerModel>();
        this.view = this.gameObject.GetComponent<PlayerView>();
        this.feetView = this.GetComponentInChildren<PlayerFeetView>();
    }

    // Update is called once per frame
    void Update()
    {
        handleDeath();
        updateCoolDown();
    }

    private void updateCoolDown()
    {
        if (hitCooldown > 0)
        {
            hitCooldown -= Time.deltaTime;
        }
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
        this.feetView.moveFwd();
    }

    public void moveBck()
    {
        this.model.moveBck();
        this.view.moveBck();
        this.feetView.moveBck();
    }

    public void turnLeft()
    {
        this.model.turnLeft();
        this.view.turnLeft();
        this.feetView.turnLeft();
    }

    public void turnRight()
    {
        this.model.turnRight();
        this.view.turnRight();
        this.feetView.turnRight();
    }

    public void handleFire()
    {
        if (hitCooldown < 0)  
        {
            this.model.fire();
            this.view.fire();

            hitCooldown = this.model.HitRate;
        }
    }

    public void StopFire()
    {      
        this.view.StopFire();            
    }

    public void reloadGun()
    {
        this.model.reload();
        this.view.reload();
    }

    public void StopReloadGun()
    {        
        this.view.StopReload();
    }

    public void handleDealDamage(float damages)
    {
        this.model.dealDamage(damages);
        this.view.dealDamage(damages);
    }

    public void idle()
    {        
        this.view.idle();
        this.feetView.idle();
    }
}
