using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectible
{
    public Sprite emptyChest;
    public int moneyAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+" + moneyAmount + "money!", 25, Color.yellow, transform.position, Vector3.up * 25, 0.7f);
        }
        //base.OnCollect(); //this equals: collected=true;
        
    }
}
