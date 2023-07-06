using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shielddefence : MonoBehaviour
{
    public Vitals vitals;
    private int lastHp;
    public GameObject Spawncontroller;
    public GameObject masterController;
    EnemyAI enemyAI;
    private bool notYet;
    // Start is called before the first frame update
    void Start()
    {
        masterController = GameObject.FindWithTag("masterController");
        enemyAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {

        lastHp = vitals.hp;
       // if (alarm is off)
        {
            //return to your room and despawn
        }
        var spawn = masterController.GetComponent<SpawnController>();
        if(spawn.alarm == false)
        {
            enemyAI.pathDestination = enemyAI.startPosition;
            if (Vector3.Distance(transform.position, enemyAI.startPosition) < .1)
            {
                Destroy(gameObject);
            }
        }
        notYet = true;
    }
    void OnTriggerEnter2D(Collider2D other) {
        Vector2 targetDirection = other.transform.position - transform.position;
        float angel = Vector2.Angle(targetDirection, transform.up);
        if (angel < 90.0f && notYet)
        {
            vitals.hp = lastHp;
        }
    }
}