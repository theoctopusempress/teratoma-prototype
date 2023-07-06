using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] patrolPoint;
    private int pointList = 0;
    EnemyAI enemyAI;
    boss1AI boss1AI;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        if( enemyAI != null) {
            enemyAI.pathDestination = patrolPoint[pointList].position;
        } else
        {
            boss1AI = GetComponent<boss1AI>();
            if(boss1AI != null)
            {
                boss1AI.pathDestination = patrolPoint[pointList].position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoint.Length > 0) {
            if (boss1AI != null)
            {
                boss1AI.pathDestination = patrolPoint[pointList].position;
                patrol();
            }
            else if (enemyAI.startpath == true)
            {
                enemyAI.pathDestination = patrolPoint[pointList].position;
                patrol();
            }
        }
    }
    void patrol()
    {
        if (Vector2.Distance(transform.position, patrolPoint[pointList].position) < 1)
        {
            pointList++;
            if (pointList >= patrolPoint.Length)
            {
                pointList = 0;
            }
        }

    }
}
