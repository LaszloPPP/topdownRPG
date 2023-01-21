using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : Mover
{
    //logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;

    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        //is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                chasing = true;
            }
            if (chasing)
            {

                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }

            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }

        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }
    }



}

