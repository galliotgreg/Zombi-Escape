using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyBoardInput : MonoBehaviour {
    private PlayerBehaviour controller = null;

    // Use this for initialization
    void Start () {
        this.controller = this.gameObject.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //Manage Fire
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            this.controller.handleFire();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {         
            this.controller.StopFire();
        }


        //Manage Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.controller.reloadGun();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            this.controller.StopReloadGun();
        }

        //Manage Motion 
        bool isMoving = false;
        if (Input.GetKey(KeyCode.Z))
        {         
            this.controller.moveFwd();
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {         
            this.controller.moveBck();
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.Q))
        {         
            this.controller.turnLeft();
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {            
            this.controller.turnRight();
            isMoving = true;
        }
        if(!isMoving)
        {           
            this.controller.idle();
        }
    }
}
