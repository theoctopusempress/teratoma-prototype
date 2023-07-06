using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public bool alarm = false;
    private bool oldAlarm =false; 
    public Transform[] spawnPoint;
    public GameObject shieldGuy;
    public float alarmTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(alarm == true && oldAlarm == false)
        {
            //create a for loop that instantiates a sheild guy at each spawn point in the list
            for(int i=0; i < spawnPoint.Length; i++)
            {
                Instantiate(shieldGuy, spawnPoint[i].transform.position, transform.rotation);
            
            }
            alarmTimer = Time.time + 30;
        }
        oldAlarm = alarm;
        if(alarm == true && alarmTimer < Time.time)
        {
            alarm = false;
        }
    }
}
