using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Mover
{
    //experience
    public int xpValue = 1;

    //logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    //OC
    public bool canjump;
    public float jumpTriggerLength;
    //OC--
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    //OC
    private Animator anim;
    //OC--

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        //OC
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            return;
        }

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
                //OC
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                    
                    if (Vector3.Distance(playerTransform.position, transform.position) < 0.2f)
                    {
                        Slam();
                    }
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
        //check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }



            //the array is not cleaned up ever time. clean up
            hits[i] = null;
        }

    }

    protected override void Die()
    {
        if (gameObject.name == "Fireball")
        {
            gameObject.SetActive(false);
            GameManager.instance.GrantXp(xpValue);
            GameManager.instance.ShowText("+" + xpValue + "xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        }

        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + "xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);


    }
    //OC
    private void Jump()
    {
        anim.SetTrigger("Jump");
    }

    private void Slam()
    {
        anim.SetTrigger("Slam");
    }
    //OC--
}
