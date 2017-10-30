using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTexture : MonoBehaviour {

	[SerializeField]
	RenderTexture texturePrefab = null;
	[SerializeField]
	UnityEngine.UI.RawImage textureTarget = null;

	RenderTexture texture = null;

	public RenderTexture Texture {
		get {
			return texture;
		}
	}

	// Use this for initialization
	void Start () {
		if (this.texturePrefab != null) {
			// Instantiate Texture
			this.texture = (RenderTexture) GameObject.Instantiate (texturePrefab, this.transform); 
			// Set texture to camera
			this.GetComponent<Camera>().targetTexture = this.texture;
			// Set the texture to the drawing place
			this.textureTarget.texture = this.texture;
		} else {
			Debug.LogError ( "CameraTexture : texturePrefab not set" );
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
