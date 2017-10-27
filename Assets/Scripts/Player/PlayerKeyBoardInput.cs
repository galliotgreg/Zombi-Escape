using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyBoardInput : MonoBehaviour {
    private PlayerBehaviour controller = null;

    [SerializeField]
    private int playerId = 0;

    [SerializeField]
    private InputManager.KeyMapping keyMapping = InputManager.KeyMapping.KeyBoard;

    [SerializeField]
    private float axisThreshold = 0.1f;

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

        //Manage Light
        if (InputManager.instance.GetKeyUp(PlayerId, KeyMapping, InputManager.ActionControl.ToggleLight))
        {
            this.controller.toggleLight();
        }

        //Manage Run
        if (InputManager.instance.GetKeyDown(PlayerId, KeyMapping, InputManager.ActionControl.Run))
        {
            this.controller.SetRuning(true);Debug.Log("Run");
        } else if (InputManager.instance.GetKeyUp(PlayerId, KeyMapping, InputManager.ActionControl.Run))
        {
            this.controller.SetRuning(false); Debug.Log("Walk");
        }

        //Manage Motion 
        bool isMovingFwd = false;
        bool isMovingBck = false;
        bool isMovingRight = false;
        bool isMovingLeft = false;
        bool isTurning = false;

        //FWD/BCK
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

        //STRAFFING
        if (InputManager.instance.GetAxis(PlayerId, KeyMapping, InputManager.ActionControl.StraffRight) > axisThreshold)
        {
            this.controller.straffRight();
            isMovingRight = true;
        }
        else if (InputManager.instance.GetAxis(PlayerId, KeyMapping, InputManager.ActionControl.StraffLeft) < -axisThreshold)
        {
            this.controller.straffLeft();
            isMovingLeft = true;
        }

        //TURNING
        if (InputManager.instance.GetAxis(PlayerId, KeyMapping, InputManager.ActionControl.TurnLeft) > axisThreshold)
        {
            this.controller.turnLeft();
            isTurning = true;
        }
        else if (InputManager.instance.GetAxis(PlayerId, KeyMapping, InputManager.ActionControl.TurnRight) < -axisThreshold)
        {
            this.controller.turnRight();
            isTurning = true;
        }
        if(!(isTurning || isMovingFwd || isMovingBck || isMovingRight || isMovingLeft))
        {           
            this.controller.idle();
        }
			
		// Manage Healing
		if (InputManager.instance.GetKeyDown(PlayerId, KeyMapping, InputManager.ActionControl.Heal))
		{
			// Check if the player is close to an injured player
			this.controller.executePlayerGroupHeal();
		}
    }
}
