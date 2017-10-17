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

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = collision.gameObject;
        controller.Target = player;
    }
}
