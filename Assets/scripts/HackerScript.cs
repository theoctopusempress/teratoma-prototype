using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HackerScript : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject hackingUI;
    //private float exposionRadius = 5.0f;
    private bool nearHack;
    private GameObject Player;
    //private Transform[] hackableObjs;
    public Hackable[] hackable;
    public List<string> hackName = new List<string>();
    public List<int> hackCost = new List<int>();
    public List<string> hackID = new List<string>();
    //private string currentKey;
    public string hackUiText;
    // Start is called before the first frame update
    void Start()
    {
        hackingUI = GameObject.FindWithTag("hackingUI");
        //hackingUI.SetActive(false);
        Text theText = hackingUI.gameObject.GetComponent<Text>();
        theText.text = "";
        Player = GameObject.Find("Player").gameObject;
        //hackableObjs = GetComponentsInChildren<Transform>();
        hackable = GetComponentsInChildren<Hackable>();
        for (int i = 0; i < hackable.Length; i++)
        {

            //var hackable = hackableObjs[i].gameObject.GetComponent<Hackable>();
            if (hackID.Contains(hackable[i].hackID) == false)
            {

                hackID.Add(hackable[i].hackID);
                for (int j=0; j < hackable[i].hackName.Count; j++)
                {

                    if (hackName.Contains(hackable[i].hackName[j])== false)
                    {

                        hackName.Add(hackable[i].hackName[j]);
                        hackCost.Add(hackable[i].hackCost[j]);
                    }
                }
            }
        }
        for (int j = 0; j < hackName.Count; j++)
        {
            hackUiText += hackName[j] + ": " + hackCost[j] + "\n";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (nearHack)
        {
            var vitals = Player.gameObject.GetComponent<Vitals>();
            //hackingUI.SetActive(true);
            Text theText = hackingUI.gameObject.GetComponent<Text>();
            //theText.text = "1.Destruction: 25 energy 2.possess sight: 50 energy";
            theText.text = hackUiText;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
               // if (vitals.Energy >= 25)
                {
                 //   vitals.Energy -= 25;

                   // for (var i = 0; i < transform.childCount; i++)
                    {
                     //  var securitySys = this.gameObject.transform.GetChild(i).GetComponent<Securitycamera>();
                        //GameObject newExplosion = Instantiate(Explosion, this.gameObject.transform.GetChild(i).transform.position, transform.rotation) as GameObject;
                        //newExplosion.transform.localScale = new Vector2(exposionRadius, exposionRadius);
                        //securitySys.selfDestuct = true;

                    }
                }
                if (vitals.Energy >= hackCost[0])
                {
                    vitals.Energy -= hackCost[0];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[0]);
                        hackIt.SendMessage(hackName[0]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && vitals.Energy >= 50)
            {
                if (vitals.Energy >= hackCost[1])
                {
                    vitals.Energy -= hackCost[1];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[1]);
                        hackIt.SendMessage(hackName[1]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if(vitals.Energy >= hackCost[2])
                {
                    vitals.Energy -= hackCost[2];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[2]);
                        hackIt.SendMessage(hackName[2]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (vitals.Energy >= hackCost[3])
                {
                    vitals.Energy -= hackCost[3];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[3]);
                        hackIt.SendMessage(hackName[3]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (vitals.Energy >= hackCost[4])
                {
                    vitals.Energy -= hackCost[4];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[4]);
                        hackIt.SendMessage(hackName[4]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                if (vitals.Energy >= hackCost[5])
                {
                    vitals.Energy -= hackCost[5];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[5]);
                        hackIt.SendMessage(hackName[5]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                if (vitals.Energy >= hackCost[6])
                {
                    vitals.Energy -= hackCost[6];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[6]);
                        hackIt.SendMessage(hackName[6]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (vitals.Energy >= hackCost[7])
                {
                    vitals.Energy -= hackCost[7];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[7]);
                        hackIt.SendMessage(hackName[7]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                if (vitals.Energy >= hackCost[8])
                {
                    vitals.Energy -= hackCost[8];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[8]);
                        hackIt.SendMessage(hackName[8]);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                if (vitals.Energy >= hackCost[9])
                {
                    vitals.Energy -= hackCost[9];
                    //add for loop for each child object containing hackable.hackID[2] and run its fucntion with the same name
                    for (int i = 0; i < hackable.Length; i++)
                    {
                        var hackIt = hackable[i].GetComponent(hackID[9]);
                        hackIt.SendMessage(hackName[9]);
                    }
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        nearHack = true;

    }
    void OnTriggerExit2D(Collider2D other)
    {
        Text theText = hackingUI.gameObject.GetComponent<Text>();
        nearHack = false;
        theText.text = "";
    }
}