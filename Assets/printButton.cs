using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class printButton : MonoBehaviour
{
    public GameObject Player;
    public Text buttonText;
    public string theTextSays;
    public int cost;
    public int listNum;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        buttonText.text = theTextSays;
    }

    // Update is called once per frame
    public void buttPush()
    {
        var PlayContrl = Player.gameObject.GetComponent<PlayerController>();
        var multiTag = Player.gameObject.GetComponent<CustomTag>();

        if (PlayContrl.scrap >= cost && multiTag.HasTag(theTextSays) == false)
        {
            PlayContrl.scrap -= cost;
            multiTag.tags.Add(theTextSays);
        }
    }
    public void InMouseOver()
    {
        parent = this.transform.parent.gameObject;
        var parnt = parent.GetComponent<printingMenu>();
        parnt.currentButt = listNum;
    }
}
