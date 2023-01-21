using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject objPlayer;
    private float cooldown = 1.5f;
    private float lastTeleport;
    public float teleportTriggerDistance = 0.25f;
    [SerializeField] Bounds teleportBounds;
    // Start is called before the first frame update
    void Start()
    {
        objPlayer = GameObject.Find("Player");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(teleportBounds.center, teleportBounds.size);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, objPlayer.transform.position);
        if (dist <= teleportTriggerDistance)
        {
            if (Time.time - lastTeleport > cooldown)
            {
                lastTeleport = Time.time;
                TeleportRandom();
            }
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
}
