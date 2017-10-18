using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWall : MonoBehaviour {
    private ZombieAIInput zombiAIInput = null;

    // Use this for initialization
    void Start()
    {
        this.zombiAIInput = gameObject.GetComponentInParent<ZombieAIInput>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (zombiAIInput.AiState == ZombieAIInput.ZombieAIState.Roaming)
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 inDir = new Vector2(transform.right.x, transform.right.y);
            Vector2 outDir = inDir - 2 * Vector2.Dot(inDir, normal) * normal;
            float r = Random.Range(-1, 1);
            float maxAngle = 10;
            float angle = maxAngle * r;
            outDir = Quaternion.Euler(0, 0, angle) * outDir;
            zombiAIInput.RoamDirection =  outDir;
        }
    }
}
