using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour {
    private ZombieModel model = null;
    private ZombieView view = null;

    private float hitTimer = -1;
	private PlayerBehaviour inCollisionPlayer = null;

    [SerializeField]
    private GameObject target = null;
    [SerializeField]
    private float aimThreshold = 2;

    public GameObject Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
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
        this.model = this.gameObject.GetComponent<ZombieModel>();
        this.view = this.gameObject.GetComponent<ZombieView>();
    }

	// Update is called once per frame
	void Update()
	{
		handleLifeState();
		//handleMotion();
		handleHitTimer();
	}

	private void handleLifeState()
	{
		if( this.model.LifePoints <= 0 )
		{
            this.view.die();
            this.model.die();
		}
	}

    public void turnLeft()
    {
        this.view.turnLeft();
        this.model.turnLeft();
    }

    public void turnRight()
    {
        this.view.turnRight();
        this.model.turnRight();
    }

    public void moveFwd()
    {
        this.view.moveFwd();
        this.model.moveFwd();
    }

	private void handleHitTimer()
	{
		if (this.hitTimer >= 0) {
			this.hitTimer -= Time.deltaTime;
		} else if ( this.inCollisionPlayer != null ) {
			this.handleHitPlayer ( this.inCollisionPlayer );
		}
	}

	private void handleHitPlayer(PlayerBehaviour player)
	{
		if( this.hitTimer < 0 && player != null )
		{
            //TODO : see how to give player in another way
            this.view.hitPlayer(player);
            this.model.hitPlayer(player);

			this.hitTimer = this.model.HitRateSec;
		}
	}

	public void handleDealDamage(float damages)
	{
        this.view.dealDamage(damages);
        this.model.dealDamage(damages);
	}

    private void OnTriggerEnter2D( Collision2D collision )
	{
		this.inCollisionPlayer = collision.gameObject.GetComponent<PlayerBehaviour> ();
	}

	void OnTriggerExit2D( Collision2D collision )
	{
		this.inCollisionPlayer = null;
	}
}
