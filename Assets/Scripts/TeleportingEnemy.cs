using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingEnemy : Mover
{
    //experience
    public int xpValue = 1;

    //logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;

    //teleport stuff
    private float cooldown = 1.5f;
    private float lastTeleport;
    public float teleportTriggerDistance = 0.25f;
    [SerializeField] Bounds teleportBounds;

    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    private Vector3 lastTeleportPosition;

    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];



    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        //startingPosition = transform.position;
        lastTeleportPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(teleportBounds.center, teleportBounds.size);
    }

    private void FixedUpdate()
    {
        //is the player in range?        
        if (Vector3.Distance(playerTransform.position, lastTeleportPosition/*startingPosition*/) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, lastTeleportPosition/*startingPosition*/) < triggerLength)
            {
                chasing = true;
            }
            if (chasing)
            {

                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                    float dist = Vector3.Distance(transform.position, playerTransform.position);
                    if (dist <= teleportTriggerDistance)
                    {
                        if (Time.time - lastTeleport > cooldown)
                        {
                            lastTeleport = Time.time;
                            TeleportRandom();
                            lastTeleportPosition = transform.position;
                        }
                    }
                }

            }
            else
            {
                UpdateMotor(lastTeleportPosition/*startingPosition*/ - transform.position);
            }

        }
        else
        {
            UpdateMotor(lastTeleportPosition/*startingPosition*/ - transform.position);
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

    private void TeleportRandom()
    {
        Vector3 min = teleportBounds.min;
        Vector3 max = teleportBounds.max;
        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);
        float randomZ = Random.Range(min.z, max.z);

        //Vector3 randomPoint = new Vector3(randomX, randomY, randomZ); change Z to 0 for 2D
        Vector3 randomPoint = new Vector3(randomX, randomY, 0);
        transform.position = randomPoint;
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
}
