using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour {
    private ZombieBehaviour controller = null;

    // Use this for initialization
    void Start()
    {
        this.controller = gameObject.GetComponentInParent<ZombieBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject player = collider.gameObject;
        controller.Target = player;
    }
}
