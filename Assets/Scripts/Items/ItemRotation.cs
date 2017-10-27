using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour {
    [SerializeField]
    private float rotSpeed;

	// Use this for initialization
	void Start () {
        transform.Rotate(0, 0, Random.value * 360);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, transform.rotation.z + rotSpeed * Time.deltaTime);
	}
}
