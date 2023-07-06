using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSlashes : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Baddie;
    private float startRotate;
    private float slashTimer = 0.0f;
    private float blockTimer = 0.0f;
    private Quaternion endRotate;
    public int damage = 25;
    public Sprite[] spritelist;
    public SpriteRenderer spriteRenderer;
    public int spriteNum=0;
    public float[] BoxSizeX;
    public float[] BoxSizeY;
    public float[] BoxOffsetX;
    public float[] BoxOffsetY;
    List<GameObject> onlyOnce = new List<GameObject>();
    public bool blocking;
    //int animationframecounter=0;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        //startRotate = transform.rotation.z - 45;
        transform.rotation = Player.transform.rotation;
        //transform.rotation = Quaternion.Euler(0, 0, startRotate);
        transform.position = transform.position + transform.up * 2;
         endRotate = transform.rotation * Quaternion.Euler(0, 0, -45);
        //Baddie = GameObject.Baddie;
        slashTimer = Time.time + 03.75f;
        blockTimer = Time.time + 1.0f;
        Baddie = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, endRotate, Time.deltaTime * 1);
        //transform.RotateAround(Player.transform.position, Vector3.forward, -360 * Time.deltaTime);
        transform.position = Player.transform.position + transform.up / 2;
        transform.rotation = Player.transform.rotation;
        if (!blocking)
        {
            if (slashTimer < Time.time)
            {
                Destroy(gameObject);
            }
            if (spriteNum < spritelist.Length)
            {
                //if (animationframecounter == 3)
                {
                    spriteRenderer.sprite = spritelist[spriteNum];
                    this.GetComponent<BoxCollider2D>().size = new Vector2(BoxSizeX[spriteNum], BoxSizeY[spriteNum]);
                    this.GetComponent<BoxCollider2D>().offset = new Vector2(BoxOffsetX[spriteNum], BoxOffsetY[spriteNum]);
                    spriteNum++;
                    //animationframecounter = 0;
                }// else { animationframecounter++; }
            }
            else { Destroy(gameObject); }
        } else if (blocking)
        {
            spriteRenderer.sprite = spritelist[5];
            this.GetComponent<BoxCollider2D>().size = new Vector2(BoxSizeX[5], BoxSizeY[5]);
            this.GetComponent<BoxCollider2D>().offset = new Vector2(BoxOffsetX[5], BoxOffsetY[5]);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && onlyOnce.Contains(other.gameObject) == false && !blocking)
        {
            Vector3 targetDirection = other.transform.position - Player.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(Player.transform.position, targetDirection);
            if (hit.transform.gameObject == other.gameObject)
            {
                var vitals = other.gameObject.GetComponent<Vitals>();
                Vector2 angelDirection = other.transform.position - Player.transform.position;
                float angel = Vector2.Angle(angelDirection, -other.transform.up);
                var play = Player.GetComponent<PlayerController>();
                if (160< angel && angel < 200 && play.crouching)
                {
                    vitals.hp -= vitals.hp;
                }
                else {
                    var ai = other.gameObject.GetComponent<EnemyAI>();
                    vitals.hp -= damage;
                    if (ai != null)
                    {
                        ai.shootTimer = Time.time + 1;
                        ai.AttackMode();
                    }
                }
                onlyOnce.Add(other.gameObject);
            }
        }
        if(blocking && other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            if(blockTimer < Time.time)
            {
                var bullet = other.gameObject.GetComponent<baddieBulletNyooms>();
                if(bullet != null)
                {
                    var vital = Player.GetComponent<Vitals>();
                    vital.hp -= bullet.damage / 10;
                }
            }
        }
        boss1AI script = other.GetComponent<boss1AI>();
        if (script != null)
        {
            script.rb.velocity = Vector3.zero;
        }
    }
}