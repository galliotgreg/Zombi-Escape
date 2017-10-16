using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour {
    private ZombieModel model = null;

    private float hitTimer = -1;
	private PlayerBehaviour inCollisionPlayer = null;

    [SerializeField]
    private GameObject target = null;

	public void setTarget(GameObject newTarget)
	{
		this.target = newTarget;
	}

    // Use this for initialization
    void Start () {
        this.model = this.gameObject.GetComponent<ZombieModel>();
    }

	// Update is called once per frame
	void Update()
	{
		handleLifeState();
		handleMotion();
		handleHitTimer();
	}

	private void handleLifeState()
	{
		if( this.model.LifePoints <= 0 )
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	private void handleMotion()
	{
		if (target != null)
		{
			//Manage look at
			Vector3 trgDir = target.transform.position - this.transform.position;
			float angle = Vector3.Angle(trgDir, this.transform.right);
			Vector3 cross = Vector3.Cross(trgDir, this.transform.right);
			if (cross.z < 0) { angle = -angle; }
			if (angle > 0 && angle < 180 - model.AimThreshold)
			{
				this.transform.Rotate(0, 0, -Time.deltaTime * model.AngularSpeed);
			}
			if (angle < 0 && angle > -180 + model.AimThreshold)
			{
				this.transform.Rotate(0, 0, Time.deltaTime * model.AngularSpeed);
			}

			//manage move
			if (Vector2.Dot(trgDir, this.transform.right) > 0)
			{
				this.transform.position += this.transform.right * Time.deltaTime * model.MoveSpeed;
			}
		}
	}

	private void handleHitTimer()
	{
		if (this.hitTimer >= 0) {
			this.hitTimer -= Time.deltaTime;
		} else if ( this.inCollisionPlayer != null ) {
			this.hitPlayer ( this.inCollisionPlayer );
		}
	}

	private void hitPlayer(PlayerBehaviour player)
	{
		if( this.hitTimer < 0 && player != null )
		{
			player.handleDealDamage ( this.model.HitDamage );
			this.hitTimer = this.model.HitRateSec;
		}
	}

	public void dealDamage(float damages)
	{
        Debug.Log("Hit : " + this.name);
        this.model.LifePoints -= damages;
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		this.inCollisionPlayer = collision.gameObject.GetComponent<PlayerBehaviour> ();
	}

	void OnCollisionExit2D( Collision2D collision )
	{
		this.inCollisionPlayer = null;
	}
}
