using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class dialoger : MonoBehaviour
{
    public string theWords;
    public Text healthText;
    //public Queue<string> sentences;
    public string[] sentences;
    public bool isTalking = false;
    private int whichSentence = 0;
    public GameObject backing;
    // Start is called before the first frame update
    void Start()
    {
        //sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(healthText.text);
        if(isTalking == true)
        {
            backing.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                whichSentence++;
            }
            if (whichSentence < sentences.Length)
            {
                //healthText.text = sentences[whichSentence];
                //StopAllCoroutines();
                StartCoroutine(TypeSentence(sentences[whichSentence]));
            }
            else
            {
                whichSentence = 0;
                isTalking = false;

            }
            
        } else
        {
            if (healthText.text == "")
            {
                backing.SetActive(false);
            }
            healthText.text = "";
        }
    }
    IEnumerator TypeSentence (string sentence)
    {
        healthText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            healthText.text += letter;
            yield return null;
        }
    }
}