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

        //Manage Motion
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.controller.moveFwd();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.controller.moveBck();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.controller.turnLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.controller.turnRight();
        }
    }
}
