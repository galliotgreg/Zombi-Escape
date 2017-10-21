using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpawner : MonoBehaviour {
    [SerializeField]
    private float frequency = 0.2f;    // spawn frequecy given on zombie / sec
    [SerializeField]
    private GameObject zombiePrefab = null;

    [SerializeField]
    private int nbActiveZombies = 0;

    private List<GameObject> activeZombies = new List<GameObject>();

    private float spawnCooldown = -1;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        handleSpawn();
        updateNbZombies();
    }

    private void updateNbZombies()
    {
        nbActiveZombies = activeZombies.Count;
    }

    private void handleSpawn()
    {
        if (spawnCooldown < 0)
        {
            //Create Zombie
            GameObject zombie = GameObject.Instantiate(zombiePrefab);
            activeZombies.Add(zombie);

            //Set random orientation
            ZombieAIInput zombieIAInput = zombie.GetComponent<ZombieAIInput>();
            float r = UnityEngine.Random.value;
            float angle = 360 * r;
            zombieIAInput.RoamDirection = Quaternion.Euler(0, 0, angle) * Vector3.right;

            //Refreash coolDown
            spawnCooldown = 1 / frequency;
        }
        else
        {
            spawnCooldown -= Time.deltaTime;
        }
    }
}
