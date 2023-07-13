using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject hackingUI;
    public bool onElivator;
    public GameObject ObjectiveMenu;
    public UnityEngine.Rendering.Universal.Light2D lit;
    public AudioSource elivatorSound;
    //public GameObject[] keyList;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("masterController");
        hackingUI = GameObject.FindWithTag("hackingUI");
        Text theText = hackingUI.gameObject.GetComponent<Text>();
        lightsOff();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onElivator)
        {
            player.transform.position = destination.transform.position;
            var saving = gameManager.GetComponent<GameManager>();
            saving.lightsOff = goingDark;
            lightsOff();
            saving.currentObject++;
            saving.SaveState();
        }
        if (locked)
        {
            lit.color = Color.black;
        } else if (!locked)
        {
            lit.color = Color.white;
        }
        if (locked)
        {
            var GM = gameManager.GetComponent<GameManager>();
            if (GM.inventoryName.Contains(theKey))
            {
                locked = false;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            if (!elivatorSound.isPlaying)
            {
                elivatorSound.Play();
            }
            if ( locked == false)
            {
                Text theText = hackingUI.gameObject.GetComponent<Text>();
                theText.text = "Press E to ride elivator";
                onElivator = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            Text theText = hackingUI.gameObject.GetComponent<Text>();
            theText.text = "";
            onElivator = false;
            if (locked)
            {
                //StartCoroutine(lightFlicker());
            }
            elivatorSound.Stop();
        }
    }
    void lightsOff()
    {
        var saving = gameManager.GetComponent<GameManager>();
        if (saving.lightsOff == true)
        {
            Player.SetActive(true);

            globalLight.SetActive(false);
        } else
        {
            Player.SetActive(false);
            globalLight.SetActive(true);

        }
    }
    public IEnumerator lightFlicker()
    {
        lit.color = Color.white;
        while (lit.color.b > 0 && lit.color.r > 0&&lit.color.g > 0)
        {
            lit.color -= new Color(0.01f,0.01f,0.01f,0.01f);
            yield return null;
        }
    }
}
