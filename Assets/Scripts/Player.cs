using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    //own code--------------
    public static Player instance;
    //own code--------------

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //own code--------------


        if (Player.instance != null)
        {
            Destroy(gameObject);
            return; //code creates a player object in every scene we enter. if we return to the Main scene where there is already a game manager it will
            //be duplicated. this IF check if it!s already there and destroyz one
        }

        instance = this;
        transform.position = GameObject.Find("SpawnPoint").transform.position;

        //own code--------------
        DontDestroyOnLoad(gameObject);

    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
        //own code--------------
        Debug.Log("Spawnpoit location according to Player" + GameObject.Find("SpawnPoint").transform.position);
        //own code--------------

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
