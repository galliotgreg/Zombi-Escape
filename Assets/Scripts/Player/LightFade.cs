using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour {
    private PlayerModel playerModel;
    private Light flashLight;

    [SerializeField]
    private float intensityMax = 15;
    [SerializeField]
    private float intensityMin = 0;
    [SerializeField]
    private float intensityLose = 1;


    // Use this for initialization
    void Start () {
        playerModel = GetComponentInParent<PlayerModel>();
        flashLight = GetComponentInParent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        playerModel.LightBattery_current -= Time.deltaTime * intensityLose;
        flashLight.intensity = Mathf.Lerp(intensityMin, intensityMax, playerModel.LightBattery_current / playerModel.LightBattery_max);
    }
}
