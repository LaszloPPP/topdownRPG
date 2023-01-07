using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        //transform.position = GameObject.Find("SP").transform.position;

        DontDestroyOnLoad(gameObject);

    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
        /*
        //own code--------------
        Debug.Log("Spawnpoit location according to Player" + GameObject.Find("SpawnPoint").transform.position);
        //own code--------------
        */

    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitPoint++;
        hitPoint = maxHitPoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
}
