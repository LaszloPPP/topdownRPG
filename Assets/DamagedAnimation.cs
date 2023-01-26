using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamagedAnimation : MonoBehaviour
{
    private GameObject weapon;
    private Animator anim;    
    public float damageAnimTriggerDistance = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("weapon_0");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, weapon.transform.position);
        if (dist <= damageAnimTriggerDistance)
        {
            Damaged();            
        }
    }

    private void Damaged()
    {
        anim.SetTrigger("Damaged");
    }
}
