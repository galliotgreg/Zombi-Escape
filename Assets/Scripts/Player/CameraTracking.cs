using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    [SerializeField]
    private GameObject trackedPlayer = null;

    public GameObject TrackedPlayer
    {
        get
        {
            return trackedPlayer;
        }

        set
        {
            trackedPlayer = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TrackedPlayer != null)
        {
            this.transform.position = new Vector3(TrackedPlayer.transform.position.x, TrackedPlayer.transform.position.y, this.transform.position.z);
        }
	}

	public void setTrackedPlayer( GameObject player ){
		this.trackedPlayer = player;
	}
}
