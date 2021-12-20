using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elivatorconsole : MonoBehaviour
{
    GameObject player;
    public GameObject destination;
    public bool locked = false;
    public bool unPowered = false;
    public int powerNeeded;
    public int currentPower;
    public string theKey;
    public bool goingDark;
    public GameObject Player;
    public GameObject globalLight;
    GameObject gameManager;
    //public GameObject[] keyList;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("masterController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            if (locked)
            {
                var multiTag = other.gameObject.GetComponent<CustomTag>();
                if (multiTag.HasTag(theKey))
                {
                    locked = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && locked == false)
            {
                player.transform.position = destination.transform.position;
                lightsOff();
                var saving = gameManager.GetComponent<GameManager>();
                saving.SaveState();
            }
        }
    }
    void lightsOff()
    {
        if(goingDark == true)
        {
            Player.SetActive(true);

            globalLight.SetActive(false);
        } else
        {
            Player.SetActive(false);
            globalLight.SetActive(true);

        }
    }
}
