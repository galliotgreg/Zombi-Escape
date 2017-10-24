using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlash : MonoBehaviour {
    private float flashCooldown = 0.05f;
    private float elapsed = 0;
    private Light flash;

	// Use this for initialization
	void Start () {
        flash = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if (elapsed > flashCooldown)
        {
            flash.enabled = false;
        } else
        {
            elapsed += Time.deltaTime;
        }
	}

    public void Flash()
    {
        flash.enabled = true;
        elapsed = 0;
    }
}
