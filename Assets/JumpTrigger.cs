using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JumpTrigger : MonoBehaviour
{
    private GameObject objPlayer;
    private Animator anim;
    private float cooldown = 1.5f;
    private float lastJump;
    public float bossSwingDistance = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        objPlayer = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, objPlayer.transform.position);
        if (dist <= bossSwingDistance)
        {
            if (Time.time - lastJump > cooldown)
            {
                lastJump = Time.time;
                Jump();
            }
        }
    }

    private void Jump()
    {
        anim.SetTrigger("Jump");
    }
}
