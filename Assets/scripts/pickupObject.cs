using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupObject : MonoBehaviour
{
    public string powerUP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
       var multiTag = collision.gameObject.GetComponent<CustomTag>();
        if (multiTag != null && multiTag.HasTag("player"))
        {
            Destroy(gameObject);
            multiTag.tags.Add(powerUP);
        }
    }

}
