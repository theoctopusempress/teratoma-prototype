using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int level;
    public float[] position;
    public int hp;
    public int ammo;
    public int energy;
    public PlayerData (PlayerController player, GameManager gameManager)
    {
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        ammo = gameManager.ammo;
        var playVital = gameManager.Player.GetComponent<Vitals>();
        hp = playVital.hp;
        energy = playVital.Energy;
    }
}
