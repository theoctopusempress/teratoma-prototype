using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorCode : MonoBehaviour
    
{
    private bool open = false;
    private float sightTimer = 0.0f;
    public bool locked = false;
    public string doorKey;
    private bool lockAgain = false;
    public Collider2D col;
    private float hieght;
    private Vector3 startPostition;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        hieght = Mathf.Sqrt(Mathf.Pow(col.bounds.size.y,2) + Mathf.Pow(col.bounds.size.x,2));
        startPostition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (open == true) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,  -gameObject.transform.up);
            var multiTag= hit.collider.GetComponent<CustomTag>();
            if (multiTag != null && multiTag.HasTag("wall") && sightTimer < Time.time)
            {
                //transform.Translate(Vector2.down * hieght);
                StartCoroutine(closeSeasame(hieght));
                open = true;
                open = false;
            }

        }
    } 
    public void OnCollisionEnter2D(Collision2D collision)
    {
      var multiTag = collision.gameObject.GetComponent<CustomTag>();
        if(locked == true && multiTag != null && multiTag.HasTag(doorKey))
        {
            locked = false;
            lockAgain = true;
        }
        if (multiTag != null && multiTag.HasTag("person") && open == false && locked == false)
      {
            StartCoroutine(openSeasame(hieght));
            //transform.Translate(Vector2.up *hieght);
            open = true;
            sightTimer = Time.time + 1;

      }
        if(lockAgain == true)
        {
            locked = true;
        }
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
