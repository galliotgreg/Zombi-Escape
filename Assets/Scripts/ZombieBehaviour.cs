using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour {
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

    private float hitTimer = -1;
	private PlayerBehaviour inCollisionPlayer = null;

    [SerializeField]
    private GameObject target = null;

    // Use this for initialization
    void Start () {
		
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
		if( this.lifePoints <= 0 )
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
			if (angle > 0 && angle < 180 - aimThreshold)
			{
				this.transform.Rotate(0, 0, Time.deltaTime * angularSpeed);
			}
			if (angle < 0 && angle > -180 + aimThreshold)
			{
				this.transform.Rotate(0, 0, -Time.deltaTime * angularSpeed);
			}

			//manage move
			if (Vector2.Dot(trgDir, this.transform.right) > 0)
			{
				this.transform.position += this.transform.right * Time.deltaTime * moveSpeed;
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
			player.dealDamage ( this.hitDamage );
			this.hitTimer = this.hitRateSec;
		}
	}

	public void dealDamage(float damages)
	{
		this.lifePoints -= damages;
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
