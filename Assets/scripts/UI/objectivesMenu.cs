using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectivesMenu : MonoBehaviour
{
    public GameObject discriptor;
    public List<string> objectives = new List<string>();
    public List<int> denominator = new List<int>();
    public int numerator;
    public int currentObjective=0;
    public GameObject Player;
    public GameObject poweredElivator;
    public GameObject theBoss;
    string objectiveText;
    string lastObjectiveText;
    public GameObject objectiveUI;
    public GameObject objectiveBorder;
    public GameObject objectiveMenu;
    Text theText;
    // Start is called before the first frame update
    void Start()
    {
        objectiveUI = GameObject.FindWithTag("hackingUI");
        theText = objectiveUI.gameObject.GetComponent<Text>();
        //discriptor = GameObject.FindWithTag("discriptor");
        //discriptor.GetComponent<Text>().text = "";
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObjective == 0)
        {
            var multiTag = Player.GetComponent<CustomTag>();
            if ( multiTag != null && multiTag.HasTag("blueKey"))
            {
                numerator = 1;
            }
            else { numerator = 0; }
        } else if(currentObjective == 1)
        {
            var PlayContrl = Player.gameObject.GetComponent<PlayerController>();
            numerator = PlayContrl.scrap;
        } else if(currentObjective == 2)
        {
            var power = poweredElivator.gameObject.GetComponent<elivatorconsole>();
            numerator = power.currentPower;
        } else if (currentObjective == 3)
        {
            var bossHealth = theBoss.GetComponent<Vitals>();
            numerator = bossHealth.hp;
        }
        objectiveText = objectives[currentObjective] + ": " + numerator + "/" + denominator[currentObjective];
        if (discriptor.activeSelf == true)
        {
            discriptor.GetComponent<Text>().text = objectiveText;
        }
        if(lastObjectiveText != objectiveText)
        {
            StartCoroutine(TypeSentence(objectiveText, true));
        }
        lastObjectiveText = objectiveText;
        if (theText.text != "")
        {
            objectiveBorder.SetActive(true);
        } else { objectiveBorder.SetActive(false); }
    }
    private void OnDisable()
    {
        discriptor.GetComponent<Text>().text = "";
    }
    public IEnumerator TypeSentence(string sentence, bool full)
    {
        theText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            theText.text += letter;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        theText.text = "";
    }
}