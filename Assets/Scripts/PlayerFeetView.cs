using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetView : MonoBehaviour {

    private Animator anim;    

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //Manage Motion
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("isWalking", true);
            Debug.Log("Up");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("isWalking", true);
            Debug.Log("Down");
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("isWalking", false);
        }
    }
}
