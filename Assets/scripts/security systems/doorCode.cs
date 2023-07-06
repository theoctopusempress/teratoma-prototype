using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorCode : MonoBehaviour
    
{
    private bool open = false;
    private float sightTimer = 0.0f;
    public bool locked = false;
    public string doorKey;
    //private bool lockAgain = false;
    public Collider2D col;
    private float hieght;
    private Vector3 startPostition;
    public Vector3[] beamPoint;
    public int beamCount = 0;
    public Sprite unlockedDoor;
    public Sprite lockedDoor;
    private SpriteRenderer spriteRenderer;
    private GameObject player;
    GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        hieght = Mathf.Sqrt(Mathf.Pow(col.bounds.size.y,2) + Mathf.Pow(col.bounds.size.x,2));
        startPostition = transform.position;
        //uses a child object as the position from which to send ray cast so it dosent hit the wall it moved into
        beamPoint[0] = transform.GetChild(1).position;
        beamPoint[1] = transform.GetChild(2).position;
        beamPoint[2] = transform.GetChild(3).position;
        gameManager = GameObject.FindWithTag("masterController");
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (locked)
        {
            spriteRenderer.sprite = lockedDoor;
        } else if(locked== false)
        {
            spriteRenderer.sprite = unlockedDoor;
        }
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (open == true)
        {
            for (int i=0; i < 2; i++) { 
                RaycastHit2D hit = Physics2D.Raycast(beamPoint[i], -gameObject.transform.up);
                var multiTag = hit.collider.GetComponent<CustomTag>();
                if (multiTag != null && multiTag.HasTag("wall") && sightTimer < Time.time)
                {
                     beamCount++;    
                }
            }
            if (beamCount >= 2)
                {
                    //Debug.Log("hiii");
                    //transform.Translate(Vector2.down * hieght);
                    StartCoroutine(closeSeasame(hieght));
                    open = true;
                    open = false;
                }                           
        }
        beamCount = 0;
        if (locked)
        {
            var GM = gameManager.GetComponent<GameManager>();
            if (GM.inventoryName.Contains(doorKey))
            {
                locked = false;
            }

        }
    }
        public void OnCollisionEnter2D(Collision2D collision)
    {
      var multiTag = collision.gameObject.GetComponent<CustomTag>();
        if (locked == true && multiTag != null && !multiTag.HasTag("player") && multiTag.HasTag(doorKey))
        {
           locked = false;
            //lockAgain = true;
            if(collision.gameObject == player){
                spriteRenderer.sprite = unlockedDoor;
            }
        }
        if (multiTag != null && multiTag.HasTag("person") && open == false && locked == false)
      {
            StartCoroutine(openSeasame(hieght));
            open = true;
            sightTimer = Time.time + 1;

      }
        //if(lockAgain == true)
        //{
          //  locked = true;
        //}
    }
    IEnumerator openSeasame(float hieght)
    {
        //if (Vector3.Distance(transform.position, startPostition) < hieght)
        for(int i=0; i <10; i++)
        {
            transform.Translate(Vector2.up * hieght / 10);
            yield return null;

        }
    }
    IEnumerator closeSeasame(float hieght)
    {
        //if (Vector3.Distance(transform.position, startPostition) < hieght)
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(Vector2.down * hieght / 10);
            yield return null;

        }
    }
}
