using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> inventoryName = new List<string>();
    public List<int> inventoryQuant = new List<int>();
    public List<int> inventoryMax = new List<int>();
    public List<string> inventoryMasterName = new List<string>();
    public List<int> inventoryMasterMax = new List<int>();
    public List<int> inventoryMasterQuant = new List<int>();
    public List<string> inventoryItemdiscription = new List<string>();
    public int ammo;
    private int lastAmmo;
    public int maxAmmo;
    public GameObject Player;
    public bool loadNotRestart;
    public GameObject ObjectiveMenu;
    public int currentObject;
    // Start is called before the first frame update
    public static GameManager instance;
    public bool lightsOff;
    public int targetFrameRate = 60;
    private void Start()
    {
        inventoryName.Add("ammo");
        inventoryQuant.Add(14);
        inventoryMax.Add(14);
        inventoryItemdiscription.Add("These make your gun shoot despite looking like bullets, they are in fact small batteries with enough capacity for only a few shots each");
        Player = GameObject.FindWithTag("Player");
        //loads save game, it needs to be in start because part of the loading process involves restarting the scene 
        Time.timeScale = 1f;
        PlayerData data = SaveSystem.LoadPlayer("saveOrLoad");
        //Debug.Log(data.loadNotRestart);
        if (data.loadNotRestart) {
            loadPrefs();
        }
        //caps the framerate at 60
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
    private void Update()
    {
        maxAmmo = inventoryMax[inventoryName.IndexOf("ammo")];
        if(ammo != lastAmmo)
        {
            inventoryQuant[inventoryName.IndexOf("ammo")] = ammo;
        }
        lastAmmo = ammo;
        ammo = inventoryQuant[inventoryName.IndexOf("ammo")];
        var objective = ObjectiveMenu.GetComponent<objectivesMenu>();
        objective.currentObjective = currentObject;
    }
    private void Awake()
    {
        instance = this;

    }
    //Save state
    public void SaveState()
    {
        var playercontroller = Player.GetComponent<PlayerController>();
        SaveSystem.SavePlayer("autoSave",playercontroller,this);
        Debug.Log("your game is saved");
    }
    public void LoadState()
    {
        //reloads the scene, is called when we want to laod so that the onstart actually calls the prefs
        //SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Saves.save");
        var playercontroller = Player.GetComponent<PlayerController>();
        SaveSystem.SavePlayer("saveOrLoad", playercontroller, this);
        PlayerData data = SaveSystem.LoadPlayer("saveOrLoad");
        data.loadNotRestart = loadNotRestart;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void loadPrefs()
    {
        //actually loads the differences betweent the checkpoint and the start of the level
        PlayerData data = SaveSystem.LoadPlayer("autoSave");
        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        //int hp = data.hp;
        Player.transform.position = position;
        ammo = data.ammo;
        lightsOff = data.lightsOff;
        var playVital = Player.GetComponent<Vitals>();
        playVital.hp = data.hp;
        playVital.Energy = data.energy;
        currentObject = data.CurrentObjective;
    }
}
