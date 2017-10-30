using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDriver : MonoBehaviour {
    GameObject blueArrow;
    GameObject redArrow;
    GameObject greenArrow;
    GameObject yellowArrow;

    PlayerModel thisPlayer;
    PlayerModel bluePlayer;
    PlayerModel redPlayer;
    PlayerModel greenPlayer;
    PlayerModel yellowPlayer;

    [SerializeField]
    private float distanceToPlayer = 0.5f;
    [SerializeField]
    private float minDistance = 5f;

    // Use this for initialization
    void Start () {
        //Retreive Arrows
        redArrow = transform.Find("Red").gameObject;
        greenArrow = transform.Find("Green").gameObject;
        blueArrow = transform.Find("Blue").gameObject;
        yellowArrow = transform.Find("Yellow").gameObject;

        //Retreive Players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        thisPlayer = GetComponentInParent<PlayerModel>();
        foreach (GameObject player in players)
        {
            PlayerModel model = player.GetComponent<PlayerModel>();
            switch(model.PlayerId)
            {
                case 0:
                    bluePlayer = model;
                    break;
                case 1:
                    redPlayer = model;
                    break;
                case 2:
                    greenPlayer = model;
                    break;
                case 3:
                    yellowPlayer = model;
                    break;
            }

            //Disable Unused Arrows
            if (bluePlayer != null && thisPlayer.PlayerId != bluePlayer.PlayerId)
            {
                blueArrow.SetActive(true);
            }
            if (redPlayer != null && thisPlayer.PlayerId != redPlayer.PlayerId)
            {
                redArrow.SetActive(true);
            }
            if (greenPlayer != null && thisPlayer.PlayerId != greenPlayer.PlayerId)
            {
                greenArrow.SetActive(true);
            }
            if (greenPlayer != null && thisPlayer.PlayerId != yellowPlayer.PlayerId)
            {
                yellowArrow.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        UpdateArrowPosition(blueArrow, bluePlayer);
        UpdateArrowPosition(redArrow, redPlayer);
        UpdateArrowPosition(greenArrow, greenPlayer);
        UpdateArrowPosition(yellowArrow, yellowPlayer);
    }

    private void UpdateArrowPosition(GameObject arrow, PlayerModel playerModel)
    {
        // Consider only active arrows
        if (playerModel == null)
        {
            return;
        }

        float distance = Vector3.Distance(thisPlayer.transform.position, playerModel.transform.position);
        if (distance > minDistance)
        {
            arrow.SetActive(true);
            //Compute Position
            Vector3 direction = playerModel.transform.position - thisPlayer.transform.position;
            direction.Normalize();
            Vector3 position = direction * distanceToPlayer;
            arrow.transform.position = thisPlayer.transform.position + position;

            //Compute orientation
            arrow.transform.LookAt(playerModel.transform);
            arrow.transform.Rotate(0, 90, 90);
        } else
        {
            arrow.SetActive(false);
        }
    }
}
