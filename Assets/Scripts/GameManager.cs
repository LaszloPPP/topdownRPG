using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //references
    public Player player;

    //public weapons

    //logic
    public int money;
    public int experience;

    //save state
    public void SaveState()
    { 
    
    }

    public void LoadState()
    { 
    
    }

}
