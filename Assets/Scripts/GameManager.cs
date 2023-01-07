using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return; //code creates a game manager object in every scene we enter. if we return to the Main scene where there is already a game manager it will
            //be duplicated. this IF check if it!s already there and destroyz one
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);        
    }

    

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //references
    public Player player;
    

    //public weapons
    public Weapon weapon;

    //floating text
    public FloatingTextManager floatingTextManager;

    //logic
    public int money;
    public int experience;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if (money >= weaponPrices[weapon.weaponLevel])
        {
            money -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    //experience system
    public int GetCurrentLevel()
    {
        int currentLevel = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[currentLevel];
            currentLevel++;

            if (currentLevel == xpTable.Count) //check if max level
            {
                return currentLevel;
            }
        }

        return currentLevel;
    }

    public int GetXpToLevel(int level) //total xp to reach a certain "level"
    {
        int r = 0;
        int xp = 0;
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
    }

    //save state
    /* what do we need to save?
     * INT preferredSkin
     * INT money
     * INT experience
     * INT weaponLevel
     */

    public void SaveState()
    {
        string s = "";
        s += "0" + "|"; //will be the preferredSkin
        s += money.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveSate", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //change player skin
        money = int.Parse(data[1]);

        //xp + level check
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
        }

        //weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        //spawnpoint
        //player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        player.transform.position = GameObject.Find("SP").transform.position;



    }

}
