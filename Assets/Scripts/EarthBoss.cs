using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBoss : Enemy
{

    private GameObject objPlayer;
    private Animator anim;
    private Vector3 startingPosition;



    protected override void Start()
    {
        objPlayer = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        startingPosition = transform.position;
    }


    void Update()
    {
        //float dist = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        float dist = Vector3.Distance(startingPosition, objPlayer.transform.position);
        
        Debug.Log(dist);
        Debug.Log(GameManager.instance.player.transform.position);
        if (dist <= 0.2f)
        {
            anim.SetTrigger("Jump");

        }


    }

}
