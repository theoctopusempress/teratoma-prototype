using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1AI : MonoBehaviour
{
    private float shootTimer;
    public GameObject BulletPrefab;
    private GameObject Player;
    public Rigidbody2D rb;
    public bool isCharging;
    public float velocity;
    private float lastDist;
    public GameObject playerSword;
    Vitals vitals;
    public doorCode[] doors1;
    public doorCode[] doors2;
    public zombaddie[] zombs1;
    public zombaddie[] zombs2;
    public int interum;
    public GameObject powerCycler1;
    public GameObject powerCycler2;
    private float atk;
    public Collider2D triggerBox;
    private bool activated;
    public GameObject bossBar;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        vitals = GetComponent<Vitals>();
        doors1 = powerCycler1.GetComponentsInChildren<doorCode>();
        doors2 = powerCycler2.GetComponentsInChildren<doorCode>();
        zombs1 = powerCycler1.GetComponentsInChildren<zombaddie>();
        zombs2 = powerCycler2.GetComponentsInChildren<zombaddie>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer < Time.time && interum == 0 && activated)
        {
            //look at player
            Vector3 targetPos = Player.transform.position;
            targetPos.x = targetPos.x - transform.position.x;
            targetPos.y = targetPos.y - transform.position.y;
            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            atk = Random.value;
            if (atk < .7)
            {
                throwNeedle();
            }
            else
            {
                scalpleSlash();
            }
            shootTimer = Time.time + 1;

        }
        velocity = rb.velocity.magnitude;
        if (isCharging == true)
        {
            float dist = Vector3.Distance(transform.position, Player.transform.position);
            if (dist > lastDist)
            {
                isCharging = false;
                lastDist = Mathf.Infinity;
            }
            else
            {
                lastDist = dist;
            }
        }
        if(vitals.hp <= vitals.maxHp * 2 / 3 && vitals.lastHp > vitals.maxHp * 2 / 3)
        {
            interum = 1;
            //bossPhase2();
            vitals.invulnerable = true;
            var pwer = powerCycler1.GetComponent<powerCycler>();
            pwer.cyclePower();
        }
        if (vitals.hp <= vitals.maxHp * 1 / 3 && vitals.lastHp > vitals.maxHp * 1 / 3)
        {
            interum = 2;
            vitals.invulnerable = true;
            //bossPhase3();
            var pwer = powerCycler2.GetComponent<powerCycler>();
            pwer.cyclePower();
        }
        if (interum == 1)
        {

            if(zombs1[0] == null && zombs1[1] == null && zombs1[2] == null && zombs1[3] == null)// change to be all zombs up to zomb 3
            {
                interum = 0;
                vitals.invulnerable = false;
            }
        }
        if(interum == 2)
        {
            if (zombs2[0] == null && zombs2[1] == null && zombs2[2] == null && zombs2[3] == null && zombs2[4] == null && zombs2[5] == null)
            {
                interum = 0;
                vitals.invulnerable = false;
            }
        }
    }
        void throwNeedle()
        {
            for (int i = 0; i < 4; i++)
            {
                var rotation = transform.rotation;
                rotation *= Quaternion.Euler(0, 0, i * 10 - 15);
                GameObject.Instantiate(BulletPrefab, transform.position, rotation);
                BulletPrefab.GetComponent<baddieBulletNyooms>().source = gameObject;
            }
        }
    void scalpleSlash()
    {
        //look at player
        Vector3 targetPos = Player.transform.position;
        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        rb.AddForce(targetPos*2500);
        isCharging = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isCharging)
        {
            if (other.gameObject == Player)
            {
                var vitals = other.gameObject.GetComponent<Vitals>();
                vitals.hp -= 25;
            }
        }
    }
    public void helloWorld()
    {
        activated = true;
        bossBar.SetActive(true);

    }
    private void OnDestroy()
    {
        bossBar.SetActive(false);
    }
}
