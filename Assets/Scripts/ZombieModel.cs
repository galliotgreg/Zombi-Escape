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
    [SerializeField]
    private float aimThreshold = 2;

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

    public float AimThreshold
    {
        get
        {
            return aimThreshold;
        }

        set
        {
            aimThreshold = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
