using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAIInput : MonoBehaviour {
    private ZombieBehaviour controller = null;

	// Use this for initialization
	void Start () {
        this.controller = this.gameObject.GetComponent<ZombieBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.controller.Target != null)
        {
            //Manage look at
            Vector3 trgDir = this.controller.Target.transform.position - this.transform.position;
            float angle = Vector3.Angle(trgDir, this.transform.right);
            Vector3 cross = Vector3.Cross(trgDir, this.transform.right);
            if (cross.z < 0) {
                angle = -angle;
            }
            if (angle > 0 && angle < 180 - this.controller.AimThreshold)
            {
                this.controller.turnLeft();
            }
            if (angle < 0 && angle > -180 + this.controller.AimThreshold)
            {
                this.controller.turnRight();
            }

            //manage move
            if (Vector2.Dot(trgDir, this.transform.right) > 0)
            {
                this.controller.moveFwd();
            }
        }
    }
}
