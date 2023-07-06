using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class fogOfWar : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == Player)
        {
            GetComponent<Light2D>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            GetComponent<Light2D>().enabled = false;
        }
    }
}
