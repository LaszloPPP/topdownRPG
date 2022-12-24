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

    //floating text
    public FloatingTextManager floatingTextManager;

    //logic
    public int money;
    public int experience;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
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
        s += "0"; //will be the weaponlevel

        PlayerPrefs.SetString("SaveSate",s);
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
        experience = int.Parse(data[2]);
        //change weaponlevel
    }

}
