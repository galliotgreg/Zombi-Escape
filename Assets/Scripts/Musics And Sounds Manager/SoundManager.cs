using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


    public AudioSource playerFxSource;
    public AudioSource playerWalkSource;
    public AudioSource playerDeathSource;

    public AudioSource musicSource;
    public AudioSource zombieSource;

    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.95f;

    public AudioClip[] playeurWalkClips;
    public AudioClip[] playeurHitClips;
    public AudioClip[] playeurDeathClips;

    public AudioClip[] handGunShotClips;
    public AudioClip[] handGunReloadClips;
    public AudioClip[] handGunShotFailClips;


    public AudioClip[] zombieWalkClips;
    public AudioClip[] zombieHitWalkClips;

    public AudioClip[] ambienceClips;
    public AudioClip[] mainMenuClips;
    public AudioClip[] victoryEndGameClips;
    public AudioClip[] gameOverEndGameClips;

    void Awake() {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    //public void PlaySingle(AudioClip clip)
    //{
    //    efxSource.clip = clip;
    //    efxSource.Play();
    //}

    public void playeurWalk()
    {
        RandomizeClips(playeurWalkClips, playerWalkSource);
    }
    public void playeurDeath()
    {
        RandomizeClips(playeurDeathClips, playerDeathSource);
    }

    public void handGunShot()
    {
        RandomizeClips(handGunShotClips, playerFxSource);
    }
    public void handGunReload()
    {
        RandomizeClips(handGunReloadClips, playerFxSource);
    }
    public void handGunShotFail()
    {
        RandomizeClips(handGunShotFailClips, playerFxSource);
    }


    public void zombieWalk()
    {
        RandomizeClips(zombieWalkClips, zombieSource);
    }
    public void zombieAttack()
    {
        RandomizeClips(zombieHitWalkClips, zombieSource);
    }

    public void mainMenu()
    {
        RandomizeClips(mainMenuClips,musicSource);
    }

    public void inGameAmbienceClip()
    {
        RandomizeClips(ambienceClips,musicSource);
    }

    public void RandomizeClips(AudioClip[] clips,  AudioSource aSource)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        aSource.pitch = randomPitch;
        aSource.clip = clips[randomIndex];

        aSource.PlayOneShot(clips[randomIndex]);
    }   
}
