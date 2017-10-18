using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour {
    [SerializeField]
    private float hitRate = 1;              //Nb bullets / seconds
    [SerializeField]
    private float lifePoints = 100;
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float angularSpeed = 90;
    [SerializeField]
    private float hitDamage = 20;

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
            return lifePoints;
        }

        set
        {
            lifePoints = value;
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
        this.transform.Rotate(0, 0, -Time.deltaTime * this.angularSpeed);
    }

    public void turnRight()
    {
        this.transform.Rotate(0, 0, Time.deltaTime * this.angularSpeed);
    }

    public void die()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void dealDamage(float damages)
    {
        this.lifePoints -= damages;
    }

    public void fire()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        if (hit.collider != null)
        int zombieHitBoxLayerMask = 1 << LayerMask.NameToLayer("ZombieHitbox");
        int wallLayerMask = 1 << LayerMask.NameToLayer("Walls");
        int mask = zombieHitBoxLayerMask | wallLayerMask;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, mask);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("ZombieHitbox"))
        {
            ZombieBehaviour zombie = hit.collider.GetComponent<ZombieBehaviour>();
            Debug.Log("Hit");
            ZombieBehaviour zombie = hit.collider.GetComponentInParent<ZombieBehaviour>();
            zombie.handleDealDamage(this.HitDamage);
        }
    }

    public void reload()
    {
        //add some ammos to the gun
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
