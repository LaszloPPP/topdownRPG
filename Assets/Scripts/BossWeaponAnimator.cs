using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossWeaponAnimator : MonoBehaviour
{
    private GameObject objPlayer;
    private Animator anim;
    private float cooldown = 1.5f;
    private float lastSwing;
    public float bossSwingDistance=0.25f;
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
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                BossSwing();
            }
        }
    }

    private void BossSwing()
    {
        anim.SetTrigger("Swing");
    }
}
