using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VitalsUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public Text healthText;
    public GameObject masterController;
    public Slider sliderhp;
    public Slider sliderEnergy;
    public bool isPlayer;
    void Start()
    {
        if (isPlayer)
        {
            Player = GameObject.FindWithTag("Player");
        }
    }
    // Update is called once per frame
    void Update()
    {
        var vitals = Player.gameObject.GetComponent<Vitals>();
        var play = Player.GetComponent<PlayerController>();
        //healthText.text = "HP: " + vitals.hp + "\nammo: " + vitals.ammo + "\nEnergy: " + vitals.Energy;
        sliderhp.maxValue = vitals.maxHp;
        sliderhp.value = vitals.hp;
        if (isPlayer)
        {
            var spawn = masterController.GetComponent<SpawnController>();
            if (spawn.alarm == true)
            {
                var alarmTime = spawn.alarmTimer - Time.time;
                //healthText.text = "\nALERT: " + alarmTime;
            }
            sliderEnergy.maxValue = vitals.maxEnrgy;
            sliderEnergy.value = vitals.Energy;
        

        if (play.youOnlyDieOnce)
        {
            sliderhp.value = 0;
        }
        }
    }
}