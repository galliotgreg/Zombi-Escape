using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour {
    public int STAT_bulletsFired = 0;
    public int STAT_bulletsHits = 0;
    public int STAT_selfHeal = 0;
    public int STAT_beHealed = 0;
    public int STAT_healedSomeone = 0;
    public int STAT_nbDead = 0;
    public int STAT_batteryUsed = 1;
    public float STAT_batteryTime = 0;

    [SerializeField]
	private int playerId = -1;              // Player's ID
	[SerializeField]
	private string playerName = "Player";   // Player's Name
	[SerializeField]
	private int playerKilledZombies = 0;    // Amounnt of Zombies killed by the player

    [SerializeField]
    private float hitRate = 1;              //Nb bullets / seconds
    [SerializeField]
    private float lifePoints_max = 100;
    [SerializeField]
    private float lifePoints_current = 100;
    [SerializeField]
    private float lightBattery_max = 100;
    [SerializeField]
    private float lightBattery_current = 100;
    [SerializeField]
    private float walkSpeed = 1;
    [SerializeField]
    private float runSpeed = 2;
    [SerializeField]
    private float angularSpeed = 90;
    [SerializeField]
    private float hitDamage = 20;
    [SerializeField]
    private bool isRuning = false;
    private bool lightOn = true;	// Indicates the state of the light

    private bool isMovingX = false;
    private bool isMovingY = false;

    // Nbre de balle max de la reserve
    private int nbBullets_max = 32;
    // Nbre de balle dans la reserve
    private int nbBullets = 32;
    // Nbre de balle max dans l'arme
    private int nbBullets_in_gun_max = 8;
    // Nbre de balle dans l'arme
    private int nbBullets_in_gun = 8;

	public int PlayerId {
		get {
			return playerId;
		}
		set {
			playerId = value;
		}
	}
    public bool IsRuning
    {
        get
        {
            return isRuning;
        }

        set
        {
            isRuning = value;
        }
    }

    public string PlayerName {
		get {
			return playerName;
		}
		set {
			playerName = value;
		}
	}
	public int PlayerKilledZombies {
		get {
			return playerKilledZombies;
		}
		set {
			playerKilledZombies = value;
		}
	}

    public int NbBullets_in_gun
    {
        get
        {
            return nbBullets_in_gun;
        }

        set
        {
            nbBullets_in_gun = value;
        }
    }

    public int NbBullets_in_gun_max
    {
        get
        {
            return nbBullets_in_gun_max;
        }

        set
        {
            nbBullets_in_gun_max = value;
        }
    }

    public int NbBullets
    {
        get
        {
            return nbBullets;
        }

        set
        {
            nbBullets = value;
        }
    }
    public int NbBullets_max
    {
        get
        {
            return nbBullets_max;
        }

        set
        {
            nbBullets_max = value;
        }
    }


    public float HitRate
    {
        get
        {
            return hitRate;
        }

        set
        {
            hitRate = value;
        }
    }

    public float LifePoints
    {
        get
        {
			return lifePoints_current;
        }

        set
        {
			lifePoints_current = value;
        }
    }

	public float LifePoints_Max
	{
		get
		{
			return lifePoints_max;
		}

		set
		{
			lifePoints_max = value;
		}
	}

    public float MoveSpeed
    {
        get
        {
            return walkSpeed;
        }

        set
        {
            walkSpeed = value;
        }
    }

    public float AngularSpeed
    {
        get
        {
            return angularSpeed;
        }

        set
        {
            angularSpeed = value;
        }
    }

    public float HitDamage
    {
        get
        {
            return hitDamage;
        }

        set
        {
            hitDamage = value;
        }
    }

	public bool LightOn {
		get {
			return lightOn;
		}
	}

    public void toggleLight()
    {
        LightFade lightFade = this.GetComponentInChildren<LightFade>();
        GameObject flashLight = lightFade.gameObject;
        Light lightSpot = flashLight.GetComponent<Light>();
        if (lightSpot.enabled)
        {
            lightSpot.enabled = false;
            lightFade.enabled = false;
			this.lightOn = false;
        } else
        {
            lightSpot.enabled = true;
            lightFade.enabled = true;
			this.lightOn = true;
        }
    }

    public float LightBattery_max
    {
        get
        {
            return lightBattery_max;
        }

        set
        {
            lightBattery_max = value;
        }
    }

    public float LightBattery_current
    {
        get
        {
            return lightBattery_current;
        }

        set
        {
            lightBattery_current = value;
        }
    }


    public void idle()
    {
        isMovingX = false;
        isMovingY = false;
    }

    public void moveFwd()
    {
        isMovingY = true;
        float speed = walkSpeed;
        if (isRuning)
        {
            speed = runSpeed;
        }
        if (isMovingX && isMovingY)
        {
            speed *= Mathf.Sqrt(2) / 2;
        }
        this.transform.position += this.transform.right * Time.deltaTime * speed;
    }

    public void moveBck()
    {
        isMovingY = true;
        float speed = walkSpeed;
        if (isRuning)
        {
            speed = runSpeed;
        }
        if (isMovingX && isMovingY)
        {
            speed *= Mathf.Sqrt(2) / 2;
        }
        this.transform.position -= this.transform.right * Time.deltaTime * speed;
    }

    public void straffRight()
    {
        isMovingX = true;
        float speed = walkSpeed;
        if (isRuning)
        {
            speed = runSpeed;
        }
        if (isMovingX && isMovingY)
        {
            speed *= Mathf.Sqrt(2) / 2;
        }
        this.transform.position -= this.transform.up * Time.deltaTime * speed;
    }
    public void straffLeft()
    {
        isMovingX = true;
        float speed = walkSpeed;
        if (isRuning)
        {
            speed = runSpeed;
        }
        if (isMovingX && isMovingY)
        {
            speed *= Mathf.Sqrt(2) / 2;
        }
        this.transform.position += this.transform.up * Time.deltaTime * speed;
    }

    public void turnLeft()
    {
        this.transform.Rotate(0, 0, Time.deltaTime * this.angularSpeed);
    }

    public void turnRight()
    {
        this.transform.Rotate(0, 0, -Time.deltaTime * this.angularSpeed);
    }

    public void die()
    {
        STAT_nbDead++;
        SoundManager.instance.playeurDeath();
        //GameObject.Destroy(this.gameObject);
    }

    public void dealDamage(float damages)
    {
		this.lifePoints_current -= damages;
        this.lifePoints_current = Math.Max(this.lifePoints_current, 0);
    }

	// returns the score of the fire
	public float fire()
    {
        STAT_bulletsFired++;

        float scoreFromFire = 0;
        int zombieHitBoxLayerMask = 1 << LayerMask.NameToLayer("ZombieHitbox");
        int wallLayerMask = 1 << LayerMask.NameToLayer("Walls");
        int mask = zombieHitBoxLayerMask | wallLayerMask;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, mask);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("ZombieHitbox"))
        {
            Debug.Log("Hit");
            ZombieBehaviour zombie = hit.collider.GetComponentInParent<ZombieBehaviour>();
			ZombieModel.DamageResult damageResult = zombie.handleDealDamage (this.HitDamage);
            STAT_bulletsHits++;

            if ( damageResult.killed ){
				this.playerKilledZombies++;
			}
			scoreFromFire = damageResult.score;
        }
        this.nbBullets_in_gun--;
		return scoreFromFire;
    }

    public void reload()
    {
        int nbBullet_a_ajouter = nbBullets_in_gun_max - nbBullets_in_gun;

        //le chargeur n'est pas plein 
        if (nbBullets_in_gun < nbBullets_in_gun_max)
            // il reste assez de balle pour complètement remplir le chargeur
        { if(nbBullet_a_ajouter <= nbBullets)
            {
                nbBullets_in_gun += nbBullet_a_ajouter;
                nbBullets -= nbBullet_a_ajouter;
            }                        
            else
            {
                nbBullets_in_gun += nbBullets;
                nbBullets -= nbBullets;
            }
        }
        //Debug.Log(NbBullets_in_gun.ToString() + "/" + nbBullets.ToString() );
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LightFade lightFade = this.GetComponentInChildren<LightFade>();
        if (lightFade.enabled && LightBattery_current > 0)
        {
            STAT_batteryTime += Time.deltaTime;
        }
    }

	// Healing : return the actual number of points add to the player
	public float beHealed( float aidAmount  )
	{
        STAT_beHealed++;

        float oldPoints = this.lifePoints_current;
		// update life points
		this.lifePoints_current = Math.Min( this.lifePoints_current+aidAmount, this.lifePoints_max );
		return lifePoints_current - oldPoints;
	}
	public void heal()
	{
		// nothing to be done
	}
	// HealingIlself : return the actual number of points add to the player
	public float healItself()
	{
        STAT_selfHeal++;

        float oldPoints = this.lifePoints_current;
		this.lifePoints_current = Math.Min( this.lifePoints_current+(this.lifePoints_max/2f), this.lifePoints_max );
		return lifePoints_current - oldPoints;
	}
	public bool canBeHealed()
	{
		return this.lifePoints_current <= 0;
	}
	public bool canBeHealedItself()
	{
		return this.lifePoints_current > 0 && this.lifePoints_current <= this.lifePoints_max/2f;
	}

	// Items
	// Heal
	public void obtainItemHeal(){} // The effect is applicable over the Group
	// Bullets
	public void obtainItemBullets()
	{
		// Recharge the bullets
		this.NbBullets = NbBullets_max;
	}
	// Battery
	public void obtainItemBattery()
	{
        STAT_batteryUsed++;
        // Recharge the battery
        this.lightBattery_current = this.lightBattery_max;
	}
}
