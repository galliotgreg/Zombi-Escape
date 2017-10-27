using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCameraManager : MonoBehaviour {

	[SerializeField]
	private GameObject playerCamera_prefab;	// PlayerCamera Prefab to be instantiated for each player

	private ArrayList playerCameras;		// cameras that follow players
	private ArrayList players;				// players to be displayed

	void Awake(){
		this.playerCameras = new ArrayList();
	}

	// Use this for initialization
	void Start () {
		// TODO Check camera prefab HERE
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setPlayers( ArrayList newPlayers ){
		// Check size
		if( newPlayers.Count < 0 || newPlayers.Count > 4 ){
			Debug.LogError("setPlayers : newPlayers must contain more than 0 and less than 5 elements");
			return;
		}
		// Check type of elements
		for( int i=0; i<newPlayers.Count; i++ ){
			if( newPlayers[i].GetType() != typeof(GameObject) ){
				Debug.LogError("setPlayers : newPlayers must contain GameObjects");
				return;
			}
		}
		this.players = newPlayers;

		this.splitScreen();
	}

	private void splitScreen(){
		if( this.playerCamera_prefab != null ){

			// Generate Cameras
			this.playerCameras.Clear();
			for( int i=0; i<this.players.Count; i++ ){
				// Create Camera
				GameObject camera = GameObject.Instantiate(playerCamera_prefab, this.gameObject.transform);
				this.playerCameras.Add( camera );
				// Associate the players to the cameras
				CameraTracking trackingCamera = camera.GetComponent<CameraTracking>();
				if( trackingCamera != null ){
					trackingCamera.setTrackedPlayer( (GameObject)this.players[i] );
				}else{
					Debug.LogError("Camera prefab does not contain trackingCamera component");
				}
			}

			// Spliting Screen : to split vertically, switch x_split and y_split, i/2 and i%2
			float generalHUDheight = 0.07f;
			float totalY = 1f - generalHUDheight;
			float x_split = (this.players.Count>1)?0.5f:1f;					// the screen splits in X only if there are more than 1 players
			float y_split = (this.players.Count>2)?totalY/2f:totalY;		// the screen splits in Y only if there are more than 2 players
			float border = 0.002f;
			float borderRadar = 0.01f;
			for( int i=0; i<this.playerCameras.Count; i++ ){
				float x = (i % 2) * x_split + border;
				float y = ((this.players.Count > 2 ? 3 - i : i) / 2) * y_split + border;
				float w = x_split - 2 * border;
				float h = y_split - 2 * border;

				Camera c = ((GameObject)this.playerCameras[i]).GetComponent<Camera>();
				c.rect = new Rect (x, y, w, h);
				// Ajusting Radar
				RectTransform radar = c.GetComponentInChildren<UnityEngine.UI.Mask>().rectTransform;
				radar.anchorMin = new Vector2 ( border + borderRadar, border + borderRadar);
				// size = initial*w; size = factor*h => factor = initial*w/h (w<h, since we search for the smallest)
				float initialFactor = 0.3f;
				if( w<h )
				{
					float factor = initialFactor*w/h;
					radar.anchorMax = new Vector2 ( border + borderRadar + initialFactor, border + borderRadar + factor );
				}
				else
				{
					float factor = initialFactor*h/w;
					radar.anchorMax = new Vector2 ( border + borderRadar + factor, border + borderRadar + initialFactor );
				}
			}

		}else{
			Debug.LogError("PLayerCamera prefab not set");
		}
	}
}
