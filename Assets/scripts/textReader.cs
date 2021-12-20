using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textReader : MonoBehaviour
{
    public GameObject player;
    public string theWords;
    public GameObject TextDoc;
    private GameObject othered;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        TextDoc = GameObject.FindWithTag("DocUI");
    }

    // Update is called once per frame
    void Update()
    {
        if(othered == player)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Text theText = TextDoc.gameObject.GetComponent<Text>();
                theText.text = theWords;
            }
        }
    }
    //if the player is colliding with the game object and presses e, the text will appear on screen
    void OnTriggerStay2D(Collider2D other)
    {
        othered = other.gameObject;
    }
    //deletes text when you move
    void OnTriggerExit2D(Collider2D other)
    {
        Text theText = TextDoc.gameObject.GetComponent<Text>();

        theText.text = "";
        othered = null;
    }
}