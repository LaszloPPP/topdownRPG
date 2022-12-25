using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //damage structure
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    //upgrades
    public int weaponLevel;
    private SpriteRenderer spriteRenderer;

    //weapon swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start(); //or:         boxCollider = GetComponent<BoxCollider2D>(); from the Collidable.cs Start
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
            {
                return;
            }

            //create new damage object, and sent it to the "Fighter" (tag) we hit

            Damage dmg = new Damage()
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
            
        }
        
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
}
