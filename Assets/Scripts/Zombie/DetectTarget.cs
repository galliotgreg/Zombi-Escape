using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTarget : MonoBehaviour {
    private ZombieBehaviour controller = null;
    private int[] agroTable = { -1, -1, -1, -1 };
    private float agroCooldown = 10;
    private float timeToFlushAgro = 10;

    // Use this for initialization
    void Start()
    {
        this.controller = gameObject.GetComponentInParent<ZombieBehaviour>();
    }

    void Update()
    {
        UpdateCooldown();
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        int nbVal = 0;
        int valMax = -1;
        for (int i = 0; i < agroTable.Length; i++)
        {
            if (agroTable[i] > valMax)
            {
                valMax = agroTable[i];
                nbVal = 1;
            } else if (agroTable[i] == valMax)
            {
                nbVal++;
            }
        }

        if (valMax > -1)
        {
            int[] indices = new int[nbVal];
            int ind = 0;
            for(int i = 0; i < agroTable.Length; i++)
            {
                if (agroTable[i] == valMax)
                {
                    indices[ind++] = i;
                }
            }

            int indFinal = 0;
            if (indices.Length > 1)
            {
                int r = Random.Range(0, indices.Length);
                indFinal = indices[r];
                agroTable[indFinal]++;
            }
            controller.Target = (GameObject)GameState.instance.Players[indFinal];
        }
    }

    private void UpdateCooldown()
    {
        if (agroCooldown > 0)
        {
            agroCooldown -= Time.deltaTime;
        } else
        {
            agroCooldown = timeToFlushAgro;
            FlushAgroTable();
        }
    }

    private void FlushAgroTable()
    {
        controller.Target = null;
        for (int i = 0; i < agroTable.Length; i++)
        {
            agroTable[i] = -1;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject player = collider.gameObject;
        PlayerModel playerModel = player.gameObject.GetComponent<PlayerModel>();
        agroTable[playerModel.PlayerId]++;
    }
}
