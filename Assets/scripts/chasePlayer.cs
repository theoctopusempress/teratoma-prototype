using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasePlayer : MonoBehaviour
{
    public GameObject Player;
    public float speed = 20;
    Rigidbody2D rb;
    public Vector2 startingPos;
    public HingeJoint2D wallTether;
    public HingeJoint2D Wrist;
    public GameObject wristWatcher;
    private float armLength;
    private bool striking;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
        armLength = Vector2.Distance(startingPos, wallTether.connectedAnchor);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(wristWatcher.transform.position);
        Vector2 wristTarget = Player.transform.position - wristWatcher.transform.position;
        //Debug.Log(wristTarget);
        float angle = Mathf.Atan2(wristTarget.y, wristTarget.x) * Mathf.Rad2Deg - 90;
        Debug.Log(angle);
        if (angle > 0)
        {
            Debug.Log("hi");
            JointMotor2D wristMoter = Wrist.motor;
            wristMoter.motorSpeed = 1000;

        }
        if (angle < 0)
        {
            Debug.Log("bye");
            JointMotor2D wristMoter = Wrist.motor;
            wristMoter.motorSpeed = -1000;

        }
        if (Vector2.Distance(Player.transform.position, wallTether.connectedAnchor) < armLength && Vector2.Distance(transform.position, wallTether.connectedAnchor) <= armLength / 3)
        {
            //striking = true;
            Vector2 targetDirection = Player.transform.position - transform.position;
            Vector2 force = targetDirection * 250000 * Time.deltaTime;
            rb.velocity = Vector3.zero;
            rb.AddForce(force);
        }
        if (Vector2.Distance(transform.position, wallTether.connectedAnchor) > armLength / 3 && striking == false)
        {
            Vector2 targetDirection = wallTether.connectedAnchor - new Vector2(transform.position.x, transform.position.y);
            Vector2 force = targetDirection * speed * Time.deltaTime;
            rb.AddForce(force);

        }
       
        if(striking == true && Vector2.Distance(transform.position, wallTether.connectedAnchor) == armLength)
        {
            striking = false;
        }
    }
}
