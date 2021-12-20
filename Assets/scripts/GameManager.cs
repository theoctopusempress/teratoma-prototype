using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> inventoryName = new List<string>();
    public List<int> inventoryQuant = new List<int>();
    public List<int> inventoryMax = new List<int>();
    public int ammo;
    private int lastAmmo;
    public int maxAmmo;
    public GameObject Player;

    // Start is called before the first frame update
    public static GameManager instance;
    private void Start()
    {
        inventoryName.Add("ammo");
        inventoryQuant.Add(14);
        inventoryMax.Add(14);
        Player = GameObject.FindWithTag("Player");
        //loads save game, it needs to be in start because part of the loading process involves restarting the scene 
        loadPrefs();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void loadPrefs()
    {
        //actually loads the differences betweent the checkpoint and the start of the level
        PlayerData data = SaveSystem.LoadPlayer();
        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        //int hp = data.hp;
        Player.transform.position = position;
        ammo = data.ammo;
        var playVital = Player.GetComponent<Vitals>();
        playVital.hp = data.hp;
        playVital.Energy = data.energy;
    }
}
