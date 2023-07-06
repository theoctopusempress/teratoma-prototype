using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public bool loadNotRestart;
    public int level;
    public float[] position;
    public int hp;
    public int ammo;
    public int energy;
    public int CurrentObjective;
    public bool lightsOff;
    public PlayerData (PlayerController player, GameManager gameManager)
    {
        loadNotRestart = gameManager.loadNotRestart;
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        ammo = gameManager.ammo;
        lightsOff = gameManager.lightsOff;
        var playVital = gameManager.Player.GetComponent<Vitals>();
        hp = playVital.hp;
        energy = playVital.Energy;
        CurrentObjective = gameManager.currentObject;
    }
}
