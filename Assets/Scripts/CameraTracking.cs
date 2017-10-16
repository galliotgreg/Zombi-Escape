using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    [SerializeField]
    private GameObject trackedPlayer = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(trackedPlayer.transform.position.x, trackedPlayer.transform.position.y, this.transform.position.z);
	}
}
