using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombaddie : MonoBehaviour
{
    private int lastHp;
    private bool undeaded = false;
    public GameObject player;
    //public Animator animator;
    //private bool biting = false;
    public float biteTime;
    public Rigidbody2D rb;
    private float deadTime;
    private float undeadTime;
    private bool isHere;
    private bool wasHere;
    public bool lockedAway;
    public Sprite corpse;
    public Sprite alive;
    public SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        undie();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var vitals = gameObject.GetComponent<Vitals>();
        var Ai = gameObject.GetComponent<EnemyAI>();
        if(vitals.hp <= vitals.maxHp /2 && lastHp > vitals.maxHp/2 && undeaded == false)
        {
            undeaded = true;
            undie();

        }
        lastHp = vitals.hp;
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if(dist < 2)
        {
            //animator.SetTrigger("knife_Trig");
            //biting = true;
        } else
        {
            //animator.SetTrigger("idle");
            //biting = false;
        }
        if(Vector3.Distance(transform.position,player.transform.position) < 5 && lockedAway == false)
        {
            redie();
            var badi = this.GetComponent<EnemyAI>();
            badi.AttackMode();
        }
        if(Vector2.Distance(transform.position,Ai.pathDestination) < .1)
        {
            isHere = true;
            if (isHere == true && wasHere == false) {
                deadTime = Time.time + 3;
            }
            if (deadTime <= Time.time)
            {
                undie();
            }
           if(isHere == true)
           {
                wasHere = true;
           }
        }
        else
        {
            isHere = false;
            wasHere = false;
        }
        if (gameObject.GetComponent<EnemyAI>().enabled == false && vitals.hp < vitals.lastHp)
        {
            vitals.hp = vitals.lastHp;
            if (undeadTime <= Time.time)
            {
                redie();
            }
        }

    }
    void undie()
    {
        rend.sprite = corpse;
        //animator.SetBool("playing dead", true);
        gameObject.GetComponent<EnemyAI>().enabled = false;
        undeadTime = Time.time + 1;
    }
    public void redie()
    {
        rend.sprite = alive;
        gameObject.GetComponent<EnemyAI>().enabled = true;
        //animator.SetBool("playing dead", false);
        deadTime = Time.time + 3;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (undeadTime <= Time.time)
        {
            redie();
        }
        if (other.gameObject == player && biteTime < Time.time)
        {
            var vitals = other.gameObject.GetComponent<Vitals>();
            vitals.hp -= 25;
            biteTime = Time.time + 1;
        }
    }
}