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
        }
        //base.OnCollect(); //this equals: collected=true;
        
    }
}
