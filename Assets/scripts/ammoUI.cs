using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoUI : MonoBehaviour
{
    public GameObject Player;
    public Image m_Image;
    public Sprite[] spritelist;
    public GameObject inventory;
    public GameObject gameManager;
    private int ammoCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var GM = gameManager.GetComponent<GameManager>();
        //var vitals = Player.gameObject.GetComponent<Vitals>();
        ammoCount = GM.ammo - 1;
        if(ammoCount < 0)
        {
            ammoCount = 0;
        }
        m_Image.sprite = spritelist[ammoCount];

    }
}
