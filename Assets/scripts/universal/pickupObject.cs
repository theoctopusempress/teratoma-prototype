using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupObject : MonoBehaviour
{
    public string powerUP;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("masterController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
       var multiTag = collision.gameObject.GetComponent<CustomTag>();
       var GM = gameManager.GetComponent<GameManager>();

        if (multiTag != null && multiTag.HasTag("player"))
        {
            Destroy(gameObject);
            //multiTag.tags.Add(powerUP);
            if (GM.inventoryName.Contains(powerUP) == false)
            {
                GM.inventoryName.Add(powerUP);
                GM.inventoryMax.Add(GM.inventoryMasterMax[GM.inventoryMasterName.IndexOf(powerUP)]);
                GM.inventoryQuant.Add(GM.inventoryMasterQuant[GM.inventoryMasterName.IndexOf(powerUP)]);
            }
            //GM.inventoryQuant += powerUPQuant;
        }
    }
}