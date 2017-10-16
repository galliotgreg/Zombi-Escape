using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
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

    private float cooldown = -1;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        handleDeath();
        handleMotion();
        handleFire();
    }

    private void handleDeath()
    {
        if (this.lifePoints <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void handleFire()
    {
        if (cooldown < 0)  
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fire();
                cooldown = hitRate;
            }
        } else
        {
            cooldown -= Time.deltaTime;
        }
    }

    private void fire()
    {
        Debug.Log("Fire");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        if (hit.collider != null)
        {
            Debug.Log("Hit : " + hit.collider.name);
            ZombieBehaviour zombie = hit.collider.GetComponent<ZombieBehaviour>();
            zombie.dealDamage(hitDamage);
        }
    }

    private void handleMotion()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += this.transform.right * Time.deltaTime * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position -= this.transform.right * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(0, 0, -Time.deltaTime * angularSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(0, 0, Time.deltaTime * angularSpeed);
        }
    }

    public void dealDamage(float damages)
    {
        this.lifePoints -= damages;
    }
}
