using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {

    private ParticleSystem ps;

    // Use this for initialization
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Manage Fire
        if (Input.GetKeyDown(KeyCode.R))
        {
            ps.gameObject.SetActive(true);

            var main = ps.main;
            main.startDelay = 5.0f;
            main.startLifetime = 2.0f;

        }        
    }
}
