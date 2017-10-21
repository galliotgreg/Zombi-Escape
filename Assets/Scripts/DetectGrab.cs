using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGrab : MonoBehaviour {
    private PlayerBehaviour inCollisionPlayer = null;

    public PlayerBehaviour InCollisionPlayer
    {
        get
        {
            return inCollisionPlayer;
        }

        set
        {
            inCollisionPlayer = value;
        }
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        this.InCollisionPlayer = collider.gameObject.GetComponent<PlayerBehaviour>();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        this.InCollisionPlayer = null;
    }
}
