using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] patrolPoint;
    private int pointList = 0;
    EnemyAI enemyAI;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.pathDestination = patrolPoint[pointList].position;

    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoint.Length > 0 && enemyAI.alert == false)
        {
            enemyAI.pathDestination = patrolPoint[pointList].position;
            if (Vector2.Distance(transform.position, patrolPoint[pointList].position) < 1)
            {
               
                pointList++;
                if(pointList >= patrolPoint.Length)
                {
                    pointList = 0;
                }
            }

        }
    }
}
