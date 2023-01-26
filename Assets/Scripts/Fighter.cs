using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;

    //enemy damage animation test
    /*
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }
   
    */
    //enemy damage animation test-------

    //all "Fighter" (tag) can be damaged, and Die()

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 40, 0.5f);
            //enemy damage animation test

            //Damaged();

            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Die();
            }
        }
    }

    protected virtual void Die()
    {

    }
    public void Damaged()
    {
        //anim.SetTrigger("Damaged");
    }
}
