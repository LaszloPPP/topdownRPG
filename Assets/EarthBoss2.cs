using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBoss2 : MonoBehaviour
{
    private GameObject objPlayer;
    private Animator anim;
    void Start()
    {
        objPlayer = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, objPlayer.transform.position);

        Debug.Log(dist);
        Debug.Log(GameManager.instance.player.transform.position);
        if (dist <= 0.4f)
        {
            anim.SetTrigger("Jump");

        }
    }
}
