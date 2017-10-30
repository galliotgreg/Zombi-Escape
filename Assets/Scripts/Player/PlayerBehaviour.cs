using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    private PlayerModel model = null;
    private PlayerView view = null;
    private PlayerFeetView feetView = null;
	private DetectGrab detectPlayer = null;

    private PlayerGroupModel playerGroup;	// Collective data
	// Property
	public PlayerGroupModel PlayerGroup {
		get {
			return playerGroup;
		}
		set {
			playerGroup = value;
		}
	}

    public PlayerModel Model
    {
        get
        {
            return model;
        }

        set
        {
            model = value;
        }
    }

    public PlayerView View
    {
        get
        {
            return view;
        }

        set
        {
            view = value;
        }
    }

    [SerializeField]
    private float hitCooldown = -1;

    void Awake()
    {
        this.model = this.gameObject.GetComponent<PlayerModel>();
        this.view = this.gameObject.GetComponent<PlayerView>();
        this.feetView = this.GetComponentInChildren<PlayerFeetView>();
		this.detectPlayer = this.gameObject.GetComponentInChildren<DetectGrab>();
    }

	void Start()
	{
		
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
        if (this.model.LifePoints > 0)
        {
            this.model.moveFwd();
            this.view.moveFwd();
            this.feetView.moveFwd();
        }

    }

    public void moveBck()
    {
        if (this.model.LifePoints > 0)
        {
            this.model.moveBck();
            this.view.moveBck();
            this.feetView.moveBck();
        }
    }

    public void turnLeft()
    {
        if (this.model.LifePoints > 0)
        {
            this.model.turnLeft();
            this.view.turnLeft();
            this.feetView.turnLeft();
        }
    }

    public void turnRight()
    {
        if (this.model.LifePoints > 0)
        {
            this.model.turnRight();
            this.view.turnRight();
            this.feetView.turnRight();
        }
    }

    public void SetRuning(bool isRuning)
    {
        this.model.IsRuning = isRuning;
    }

    public void straffLeft()
    {
        if (this.model.LifePoints > 0)
        {
            this.model.straffLeft();
            this.view.straffLeft();
            this.feetView.straffLeft();
        }
    }

    public void straffRight()
    {
        if (this.model.LifePoints > 0)
        {
            this.model.straffRight();
            this.view.straffRight();
            this.feetView.straffRight();
        }
    }

    public void handleFire()
    {
        if (this.model.LifePoints > 0 && !this.model.IsRuning)
        {
            if (this.model.NbBullets_in_gun > 0)
            {
                if (hitCooldown < 0)
                {
                    float scoreFromFire = this.model.fire();
                    this.view.fire();
                    hitCooldown = this.model.HitRate;
                }
            }
            else
            {
                this.view.fireFail();
            }
        }
    }

    public void toggleLight()
    {
        if (this.model.LifePoints > 0)
        {
            this.view.toggleLight();
            this.model.toggleLight();
        }
    }

    public void StopFire()
    {      
        this.view.StopFire();            
    }

    public void reloadGun()
    {
        if (this.model.LifePoints > 0)
        {
            this.model.reload();
            this.view.reload();
        }
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
        this.model.idle();
        this.view.idle();
        this.feetView.idle();
    }

	public void executePlayerGroupHeal()
	{
		if( this.detectPlayer.InCollisionPlayer != null && this.detectPlayer.InCollisionPlayer.model.canBeHealed() ){
			this.playerGroup.heal( this.detectPlayer.InCollisionPlayer, this );
		}
	}
	public void executePlayerGroupHealItself()
	{
		if (this.model.canBeHealedItself ()) {
			this.playerGroup.healItself (this);
		}
	}
	public void beHealed( float aidAmount )
	{
		float points = this.model.beHealed( aidAmount );
		this.view.beHealed( points );
	}
    public void heal()
    {
        if (this.model.LifePoints > 0) {
            this.model.heal();
            this.view.heal();
        }
	}
    public void healItself()
    {
        if (this.model.LifePoints > 0) {
            float points = this.model.healItself();
            this.view.beHealed(points);
        }
	}

	public void obtainItem( ItemBehaviour item )
	{
        if (this.model.LifePoints > 0)
        {
            switch (item.getItemType())
            {
                case ItemModel.ItemType.Heal:
                    this.playerGroup.obtainLife();
                    this.model.obtainItemHeal();
                    this.view.obtainItemHeal();
                    break;
                case ItemModel.ItemType.Bullets:
                    this.model.obtainItemBullets();
                    this.view.obtainItemBullets();
                    break;
                case ItemModel.ItemType.Battery:
                default:
                    this.model.obtainItemBattery();
                    this.view.obtainItemBattery();
                    break;
            }
        }
	}

	public void setPlayerName( string name )
	{
		if( name != "" ){
			this.model.PlayerName = name;
		}
		else{
			this.model.PlayerName = "Player "+(this.model.PlayerId+1).ToString();
		}
	}
	public void setPlayerId( int id )
	{
		this.model.PlayerId = id;
		this.view.updatePlayerId (id);
	}
}
