using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class printingMenu : MonoBehaviour
{
    public GameObject Player;
    public Text scrapCount;
    List<string> printName = new List<string>();
    public List<string> printDescription = new List<string>();
    List<int> printCost = new List<int>();
    List<Button> ButtNumb = new List<Button>();
    List<int> printQuant = new List<int>();
    List<int> printMax = new List<int>();
    public Button printerButton;
    public Button inventButton;
    private Button theButt;
    private GameObject myButt;
    public int currentButt;
    public int currentBoott;
    public List<string> inventoryName = new List<string>();
    List<int> inventoryQuant = new List<int>();
    List<int> inventoryMax = new List<int>();
    List<int> inventQuant = new List<int>();
    public GameObject GameManager;
    List<Button> inventButtNumb = new List<Button>();
    public GameObject discriptor;
    // Start is called before the first frame update

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        printName.Add("redKey");
        printDescription.Add("a ket that gives full securty acess to the elevator");
        printCost.Add(30);
        printMax.Add(1);
        printQuant.Add(1);
        printName.Add("ammo");
        printDescription.Add("These make your gun shoot despite looking like bullets, they are in fact small batteries with enough capacity for only a few shots each");
        printCost.Add(5);
        printMax.Add(14);
        printQuant.Add(4);
        //inventoryName.Add("ammo");
        //inventoryQuant.Add(14);
        //inventoryMax.Add(14);
        setPrintMenu();
        setInventMenu();
    }

    // Update is called once per frame
    void Update()
    {
        var PlayContrl = Player.gameObject.GetComponent<PlayerController>();
        scrapCount.text = "Scrap: " + PlayContrl.scrap;
        //setInventMenu();
        var GM = GameManager.GetComponent<GameManager>();
        for (int i = 0; i < GM.inventoryName.Count; i++)
        {
            theButt = inventButtNumb[i];
            
            var Buttt = theButt.gameObject.GetComponent<printButton>();

            Buttt.theTextSays = GM.inventoryName[i] + ": " + GM.inventoryQuant[i] + "/ " + GM.inventoryMax[i];
        }

    }
    void setPrintMenu()
    {
        for(int i = 0; i < printName.Count; i++)
        {
            theButt = Instantiate(printerButton);
            theButt.transform.SetParent(gameObject.transform, false);
            RectTransform ButtPosition = theButt.GetComponent<RectTransform>();
            //positions the button
            ButtPosition.localPosition = Vector3.down * 50 * i + Vector3.left * 340 + Vector3.up * 70;
            var Buttt = theButt.gameObject.GetComponent<printButton>();
            Buttt.theTextSays = printName[i] + ": " + printCost[i];
            // Buttt.cost = printCost[i];
            ButtNumb.Add(theButt);
            theButt.onClick.AddListener(buttPush);
            Buttt.listNum = i;
        }
    }
    void setInventMenu()
    {
        var GM = GameManager.GetComponent<GameManager>();
        for (int i = 0; i < GM.inventoryName.Count; i++)
        {
            if (inventButtNumb.Count < i+1)
            {
                theButt = Instantiate(inventButton);
                theButt.transform.SetParent(gameObject.transform, false);
                RectTransform ButtPosition = theButt.GetComponent<RectTransform>();
                //positions the button
                ButtPosition.localPosition = Vector3.down * 50 * i + Vector3.right * 340 + Vector3.up * 70;
                inventButtNumb.Add(theButt);
            }
                theButt = inventButtNumb[i];
            var Buttt = theButt.gameObject.GetComponent<printButton>();
            Buttt.theTextSays = GM.inventoryName[i] + ": " + GM.inventoryQuant[i] + "/ " + GM.inventoryMax[i];
            ButtNumb.Add(theButt);
            theButt.onClick.AddListener(boottPush);
            Buttt.listNum = i;
        }
    }
    public void buttPush()
    {
        var GM = GameManager.GetComponent<GameManager>();

        var PlayContrl = Player.gameObject.GetComponent<PlayerController>();
        var multiTag = Player.gameObject.GetComponent<CustomTag>();
        if (PlayContrl.scrap >= printCost[currentButt] && multiTag.HasTag(printName[currentButt]) == false)
        {
            PlayContrl.scrap -= printCost[currentButt];
            if(GM.inventoryName.Contains(printName[currentButt]) == false)
            {
                GM.inventoryName.Add(printName[currentButt]);
                GM.inventoryMax.Add(printMax[currentButt]);
                GM.inventoryQuant.Add(0);
            }
            currentBoott = GM.inventoryName.IndexOf(printName[currentButt]);
            GM.inventoryQuant[currentBoott] += printQuant[currentButt];
            if(GM.inventoryQuant[currentBoott] > GM.inventoryMax[currentBoott])
            {
                GM.inventoryQuant[currentBoott] = GM.inventoryMax[currentBoott];
            }
            setInventMenu();
        }
    }
    public void boottPush()
    {

    }
}