using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour {
    [SerializeField]
    private ZombiSpawner[] spawners;
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private ZombiSpawner[] enabledSpawners;
    [SerializeField]
    private ZombiSpawner[] disabledSpawners;
    [SerializeField]
    private float activationRange_inf = 5;
    [SerializeField]
    private float activationRange_sup = 15;

    [SerializeField]
    private float timePerWave = 30;
    [SerializeField]
    private float coefSupPerWave = 20;
    [SerializeField]
    private float timeUntilNextWave = 3;
    [SerializeField]
    private int nbZombiesOnNextWave = 5;

	// Use this for initialization
	void Start () {
        spawners = FindObjectsOfType<ZombiSpawner>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        timeUntilNextWave -= Time.deltaTime;

        if (timeUntilNextWave < 0)
        {
            releaseWave();
        }
	}

    private void releaseWave()
    {
        findSpawnersOnRange();
        dispatchZombiesOnSpawners();
        launchWave();
        initNextWaveParams();
    }

    private void launchWave()
    {
        foreach (ZombiSpawner zombieSpawner in disabledSpawners)
        {
            zombieSpawner.enabled = false;
        }
        foreach (ZombiSpawner zombieSpawner in enabledSpawners)
        {
            zombieSpawner.enabled = true;
        }
    }

    private void dispatchZombiesOnSpawners()
    {
        int zombiesLeft = nbZombiesOnNextWave;
        foreach (ZombiSpawner zombieSpawner in enabledSpawners)
        {
            if (zombiesLeft < 1)
            {
                return;
            }
            zombieSpawner.ZombiesInQueue = (int) ((float) nbZombiesOnNextWave / (float) enabledSpawners.Length);
            if (zombieSpawner.ZombiesInQueue == 0)
            {
                zombieSpawner.ZombiesInQueue = 1;
            }
            zombiesLeft -= zombieSpawner.ZombiesInQueue;
        }
    }

    private void initNextWaveParams()
    {
        timeUntilNextWave = timePerWave;
        nbZombiesOnNextWave = (int) (nbZombiesOnNextWave * (1 + coefSupPerWave / 100));
    }

    private void findSpawnersOnRange()
    {
        if (players.Length == 0)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        //Create activation table
        bool[] isEnable = new bool[spawners.Length];
        for (int i = 0; i < spawners.Length; i++)
        {
            isEnable[i] = true;
        }

        //Disable spawners close to each player
        for (int i = 0; i < spawners.Length; i++)
        {
            ZombiSpawner spawner = spawners[i];
            foreach (GameObject player in players)
            {
                if (Vector3.Distance(spawner.transform.position, player.transform.position) < activationRange_inf)
                {
                    isEnable[i] = false;
                }
            }
        }

        //Disable spawners far from everyPlayers 
        for (int i = 0; i < spawners.Length; i++)
        {
            ZombiSpawner spawner = spawners[i];
            bool isFarFromEverybody = true;
            foreach (GameObject player in players)
            {
                if (Vector3.Distance(spawner.transform.position, player.transform.position) < activationRange_sup)
                {
                    isFarFromEverybody = false;
                }
            }
            if (isFarFromEverybody)
            {
                isEnable[i] = false;
            }
        }

        //Apply activation Table
        List<ZombiSpawner> enabledList = new List<ZombiSpawner>();
        List<ZombiSpawner> disabledList = new List<ZombiSpawner>();
        for (int i = 0; i < spawners.Length; i++)
        {
            if (isEnable[i])
            {
                enabledList.Add(spawners[i]);
            } else
            {
                disabledList.Add(spawners[i]);
            }
        }
        enabledSpawners = enabledList.ToArray();
        disabledSpawners = disabledList.ToArray();
    }

	public float getNextWaveTime()
	{
		return this.timeUntilNextWave;
	}
	public float getNextWaveAmountZombies()
	{
		return this.nbZombiesOnNextWave;
	}
}
