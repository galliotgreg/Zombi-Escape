using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpawner : MonoBehaviour {
    [SerializeField]
    private float frequency = 0.2f;    // spawn frequecy given on zombie / sec
    [SerializeField]
    private GameObject zombiePrefab = null;

    private float spawnCooldown = -1;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (spawnCooldown < 0)
        {
            GameObject zombie = GameObject.Instantiate(zombiePrefab);
            ZombieBehaviour zombieBehaviour = zombie.GetComponent<ZombieBehaviour>();
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            zombieBehaviour.Target = player;

            spawnCooldown = frequency;
        } else
        {
            spawnCooldown -= Time.deltaTime;
        }
    }
}
