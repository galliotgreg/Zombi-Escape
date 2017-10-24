using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour {
    [SerializeField]
    private float hitRate = 1;              //Nb bullets / seconds
    [SerializeField]
    private float lifePoints_max = 100;
	private float lifePoints_current = 100;
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float angularSpeed = 90;
    [SerializeField]
    private float hitDamage = 20;

    // Nbre de balle max de la reserve
    private int nbBullets_max = 32;
    // Nbre de balle dans la reserve
    private int nbBullets = 32;
    // Nbre de balle max dans l'arme
    private int nbBullets_in_gun_max = 8;
    // Nbre de balle dans l'arme
    private int nbBullets_in_gun = 8;



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
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
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

    public void moveFwd()
    {
        this.transform.position += this.transform.right * Time.deltaTime * this.moveSpeed;
    }

    public void moveBck()
    {
        this.transform.position -= this.transform.right * Time.deltaTime * this.moveSpeed;
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
        SoundManager.instance.playeurDeath();
        //GameObject.Destroy(this.gameObject);
    }

    public void dealDamage(float damages)
    {
		this.lifePoints_current -= damages;
        this.lifePoints_current = Math.Max(this.lifePoints_current, 0);
    }

    public void fire()
    {
        int zombieHitBoxLayerMask = 1 << LayerMask.NameToLayer("ZombieHitbox");
        int wallLayerMask = 1 << LayerMask.NameToLayer("Walls");
        int mask = zombieHitBoxLayerMask | wallLayerMask;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, mask);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("ZombieHitbox"))
        {
            Debug.Log("Hit");
            ZombieBehaviour zombie = hit.collider.GetComponentInParent<ZombieBehaviour>();
            zombie.handleDealDamage(this.HitDamage);
        }
        this.nbBullets_in_gun--;
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
		
	}
		
	public void beHealed( float aidAmount  )
	{
		// update life points
		this.lifePoints_current = Math.Min( this.lifePoints_current+aidAmount, this.lifePoints_max );
	}
	public void heal()
	{
		// nothing to be done
	}
	public bool canBeHealed()
	{
		return this.lifePoints_current <= 0;
	}
}
