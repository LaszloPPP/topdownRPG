using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//
using UnityEngine.UI;


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
            Destroy(hud.gameObject);
            Destroy(menu.gameObject);
            return; //code creates a game manager object in every scene we enter. if we return to the Main scene where there is already a game manager it will
            //be duplicated. this IF check if it!s already there and destroyz one
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        //
        hitpointText.text = player.hitPoint.ToString();

        //
        if (experience == 0)
        {
            xpBarExternal.localScale = new Vector3(0, 1, 1);
        }
        //

        //SceneManager.sceneLoaded += OnSceneLoaded;
        //DontDestroyOnLoad(gameObject); - put in dontdestroy.cs
        //DontDestroyOnLoad(hitpointBar.gameObject); - put in dontdestroy.cs
    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //references
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;    
    public Text hitpointText;

    //external xp bar
    public RectTransform xpBarExternal;
    public Text xpTextExternal;

    //external gold
    public Text gold;
    public Text goldOutline;


    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;


    //logic
    public int money;
    public int experience;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //
    public void Update()
    {
        gold.text = "Gold: " + money;
        goldOutline.text = "Gold: " + money;
    }
    //

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

    //xpBarExternal

    public void OnXpChange()
    {
        int currLevel = GetCurrentLevel();

        if (currLevel == xpTable.Count) //check if at max xp level
        {
            xpTextExternal.text = experience.ToString() + " total experience points";
            xpBarExternal.localScale = Vector3.one;
        }
        
        else
        {
            
            int prevLevelXp = GetXpToLevel(currLevel - 1);
            int currLevelXp = GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBarExternal.localScale = new Vector3(completionRatio, 1, 1);
            xpTextExternal.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }

    //healthbar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
        //
        hitpointText.text = player.hitPoint.ToString();
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
        //
        OnXpChange();
        //
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitPointChange();
    }

    //on scene loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("Spawnpoint").transform.position;
    }

    //death menu and respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
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

        SceneManager.sceneLoaded -= LoadState;

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

    }

}
