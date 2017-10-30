using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifAnimator : MonoBehaviour {
    public Sprite[] frames = new Sprite[16];
    public float framesPerSecond = 10;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        int index = (int) (Time.time * framesPerSecond) % frames.Length;
        spriteRenderer.sprite = frames[index];
    }
}
