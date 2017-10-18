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

            ZombieAIInput zombieIAInput = zombie.GetComponent<ZombieAIInput>();
            float r = Random.value;
            float angle = 360 * r;
            zombieIAInput.RoamDirection = Quaternion.Euler(0, 0, angle) * Vector3.right;
            Debug.Log(angle);
            spawnCooldown = frequency;
        } else
        {
            spawnCooldown -= Time.deltaTime;
        }
    }
}
