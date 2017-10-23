﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyBoardInput : MonoBehaviour {
    private PlayerBehaviour controller = null;

    [SerializeField]
    private int playerId = 0;

    [SerializeField]
    private InputManager.KeyMapping keyMapping = InputManager.KeyMapping.KeyBoard;

    [SerializeField]
    private float axisThreshold = 0.01f;

    public int PlayerId
    {
        get
        {
            return playerId;
        }

        set
        {
            playerId = value;
        }
    }

    public InputManager.KeyMapping KeyMapping
    {
        get
        {
            return keyMapping;
        }

        set
        {
            keyMapping = value;
        }
    }

    // Use this for initialization
    void Start () {
        this.controller = this.gameObject.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //Manage Fire
        if (InputManager.instance.GetKeyDown(PlayerId, KeyMapping, InputManager.ActionControl.Fire))
        {            
            this.controller.handleFire();
        }
        if (InputManager.instance.GetKeyUp(PlayerId, KeyMapping, InputManager.ActionControl.Fire))
        {         
            this.controller.StopFire();
        }


        //Manage Reload
        if (InputManager.instance.GetKeyDown(PlayerId, KeyMapping, InputManager.ActionControl.Reload))
        {
            this.controller.reloadGun();
        }
        if (InputManager.instance.GetKeyUp(PlayerId, KeyMapping, InputManager.ActionControl.Reload))
        {
            this.controller.StopReloadGun();
        }

        //Manage Motion 
        bool isMovingFwd = false;
        bool isMovingBck = false;
        bool isTurning = false;
        if (InputManager.instance.GetAxis(PlayerId, KeyMapping, InputManager.ActionControl.MoveFwd) > axisThreshold)
        {         
            this.controller.moveFwd();
            isMovingFwd = true;
        }
        else if (InputManager.instance.GetAxis(PlayerId, KeyMapping, InputManager.ActionControl.MoveBck) < -axisThreshold)
        {         
            this.controller.moveBck();
            isMovingBck = true;
        }

        if (InputManager.instance.GetAxis(PlayerId, KeyMapping, InputManager.ActionControl.TurnLeft) > axisThreshold)
        {
            if (!isMovingBck)
            {
                this.controller.turnLeft();
            }
            else
            {
                this.controller.turnRight();
            }
            isTurning = true;
        }
        else if (InputManager.instance.GetAxis(PlayerId, KeyMapping, InputManager.ActionControl.TurnRight) < -axisThreshold)
        {
            if (!isMovingBck)
            {
                this.controller.turnRight();
            }
            else
            {
                this.controller.turnLeft();
            }
            isTurning = true;
        }
        if(!(isTurning || isMovingFwd || isMovingBck))
        {           
            this.controller.idle();
        }
    }
}