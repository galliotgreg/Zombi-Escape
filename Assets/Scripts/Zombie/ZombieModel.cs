using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieModel : MonoBehaviour {
    [SerializeField]
    private float hitRateSec = 2;
    [SerializeField]
    private float lifePoints = 100;
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private float angularSpeed = 90;
    [SerializeField]
    private float hitDamage = 20;

    public float HitRateSec
    {
        get
        {
            return hitRateSec;
        }

        set
        {
            hitRateSec = value;
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

    public void die()
    {
        GameObject.Destroy(this.gameObject);                
    }

    public void turnLeft()
    {
        this.transform.Rotate(0, 0, -Time.deltaTime * angularSpeed);
    }

    public void turnRight()
    {
        this.transform.Rotate(0, 0, Time.deltaTime * angularSpeed);
    }

    public void moveFwd()
    {
        this.transform.position += this.transform.right * Time.deltaTime * moveSpeed;
    }

    // Use this for initialization
    void Start () {
		
	}

    public void hitPlayer(PlayerBehaviour player)
    {
        player.handleDealDamage(hitDamage);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void dealDamage(float damages)
    {
        this.lifePoints -= damages;
    }
}
