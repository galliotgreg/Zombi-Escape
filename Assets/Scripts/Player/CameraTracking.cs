using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    [SerializeField]
    private GameObject trackedPlayer = null;
    [SerializeField]
    private float maxRotspeed = 90;
    [SerializeField]
    private float timeToFocus = 0.5f;
    [SerializeField]
    private float timeSinceTurn = 0f;
    [SerializeField]
    private bool isTurning;

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
            this.transform.position += this.transform.up * 1.5f;
            float trgAngle = TrackedPlayer.transform.eulerAngles.z - (this.transform.eulerAngles.z + 90);
            if (trgAngle > 180)
            {
                trgAngle -= 360;
            }
            if (trgAngle < -180)
            {
                trgAngle += 360;
            }

            if (Mathf.Abs(trgAngle) > 0.1) {
                if (!isTurning)
                {
                    timeSinceTurn = 0;
                } else
                {
                    timeSinceTurn += Time.deltaTime;
                }
                isTurning = true;

                //float maxAngle = Time.deltaTime * maxRotspeed;
                //Debug.Log(trgAngle + " " + maxAngle);
                //float angle = trgAngle;
                //if (Mathf.Abs(trgAngle) > maxAngle)
                //{
                //    Debug.Log(trgAngle + " " + maxAngle);
                //    float sign = Mathf.Sign(trgAngle);
                //    angle = sign * maxAngle;
                //}

                float t = timeSinceTurn / timeToFocus;
                t = Mathf.Min(t, 1);
                this.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z + Mathf.Lerp(0, trgAngle, t));
            } else
            {
                isTurning = false;
            }
        }
	}

	public void setTrackedPlayer( GameObject player ){
		this.trackedPlayer = player;

		HUD_Controller hud = this.GetComponentInChildren<HUD_Controller>();
		hud.setPlayer( player );
	}
}
