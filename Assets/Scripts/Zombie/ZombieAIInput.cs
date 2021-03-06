﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAIInput : MonoBehaviour {
    private ZombieBehaviour controller = null;

    public enum ZombieAIState {Roaming, Hunting}

    private ZombieAIState aiState = ZombieAIState.Roaming;

    private Vector3 roamDirection = new Vector3(1, 0, 0);

    private bool isTurning = false;

    public Vector3 RoamDirection
    {
        get
        {
            return roamDirection;
        }

        set
        {
            roamDirection = value;
        }
    }

    public ZombieAIState AiState
    {
        get
        {
            return aiState;
        }

        set
        {
            aiState = value;
        }
    }

    public bool IsTurning
    {
        get
        {
            return isTurning;
        }

        set
        {
            isTurning = value;
        }
    }

    // Use this for initialization
    void Start () {
        this.controller = this.gameObject.GetComponent<ZombieBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        updateAIState();
        if (AiState == ZombieAIState.Hunting)
        {
            huntBehaviour();
        } else
        {
            roamBehaviour();
        }
    }

    private void roamBehaviour()
    {

        //Compute signed angle betweed roam dir & zombi dir
        float angle = Vector3.Angle(roamDirection, this.transform.right);
        Vector3 cross = Vector3.Cross(roamDirection, this.transform.right);
        if (cross.z < 0)
        {
            angle = -angle;
        }

        //Manage motion
        if (angle > this.controller.AimThreshold && angle < 180 - this.controller.AimThreshold)
        {
            this.controller.turnLeft();
            IsTurning = true;
        }
        else if (angle < -this.controller.AimThreshold && angle > -180 + this.controller.AimThreshold)
        {
            this.controller.turnRight();
            IsTurning = true;
        }
        else if (Vector2.Dot(roamDirection, this.transform.right) < 0)
        {
            this.controller.turnRight();
            IsTurning = true;
        }
        else
        {
            this.controller.moveFwd();
            IsTurning = false;
        }
    }

    private void huntBehaviour()
    {
        //Manage look at
        Vector3 trgDir = this.controller.Target.transform.position - this.transform.position;
        float angle = Vector3.Angle(trgDir, this.transform.right);
        Vector3 cross = Vector3.Cross(trgDir, this.transform.right);
        if (cross.z < 0)
        {
            angle = -angle;
        }
        if (angle > this.controller.AimThreshold && angle < 180 - this.controller.AimThreshold)
        {
            this.controller.turnLeft();
        }
        else if (angle < -this.controller.AimThreshold && angle > -180 + this.controller.AimThreshold)
        {
            this.controller.turnRight();
        } else if (Vector2.Dot(trgDir, this.transform.right) < 0)
        {
            this.controller.turnRight();
        }

        //manage move
        if (Vector2.Dot(trgDir, this.transform.right) > 0)
        {
            this.controller.moveFwd();
        }
    }

    private void updateAIState()
    {
        if (this.controller.Target == null)
        {
            AiState = ZombieAIState.Roaming;
        } else
        {
            AiState = ZombieAIState.Hunting;
        }
    }
}
