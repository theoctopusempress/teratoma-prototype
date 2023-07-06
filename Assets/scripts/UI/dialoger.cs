using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class dialoger : MonoBehaviour
{
    public string theWords;
    public Text healthText;
    public Text logText;
    //public Queue<string> sentences;
    public string[] sentences;
    public int[] whoseTalk;
    public int[] whoseThere;
    public bool isTalking = false;
    public int whichSentence = 0;
    public GameObject backing;
    public GameObject caller;
    public Image callFace;
    public Sprite[] callList;
    public string textMem;
    public GameObject logMenu;
    public Text inventoryDiscription;
    // Start is called before the first frame update
    void Start()
    {
        //sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthText.text != "")
        {
            backing.SetActive(true);
        }
        if (isTalking == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                whichSentence++;
                if (logText != null)
                {
                    logText.text += healthText.text + "\n";
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                whichSentence = sentences.Length;
            }
            if (whichSentence < sentences.Length)
            {
                //healthText.text = sentences[whichSentence];
                //StopAllCoroutines();
                StartCoroutine(TypeSentence(sentences[whichSentence]));
                if (whoseThere[whichSentence] >0 && whichSentence <=whoseThere.Length)
                {
                    caller.SetActive(true);
                    callFace.sprite = callList[0];
                    //callFace.sprite = callList[whoseThere[whichSentence]-1];
                }
                else { caller.SetActive(false); }
            }
            else
            {
                whichSentence = 0;
                isTalking = false;
                caller.SetActive(false);
            }
            
        } else
        {
            if (healthText.text == "")
            {
                backing.SetActive(false);
            }
            healthText.text = "";
        }
        if(logMenu != null)
        {
            inventoryDiscription.text = textMem;
        }
    }
    public IEnumerator TypeSentence (string sentence)
    {
        textMem = healthText.text + "\n ";
        healthText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            healthText.text += letter;
            yield return null;
        }
    }
}