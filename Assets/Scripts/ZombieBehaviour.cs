using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour {
    [SerializeField]
    private float hitRate = 1;
    [SerializeField]
    private float lifePoints = 100;
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float angularSpeed = 90;
    [SerializeField]
    private float hitDamage = 20;
    [SerializeField]
    private float aimThreshold = 2;

    private long hitTimer = 0;

    [SerializeField]
    private GameObject target = null;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
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
}
