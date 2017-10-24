using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "victoryScene")
        {
            SoundManager.instance.victory();
        }
        if (scene.name == "gameOverScene")
        {
            SoundManager.instance.defeat();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
