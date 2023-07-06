 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombSniff : MonoBehaviour
{
    public GameObject zombi;
    public string activatedScript;
    public string activatedFunction;
    public string deactivatedFunction;
    public string[] targetTags;
    public bool smellsYa;
    public bool smellsNa;
    public GameObject smelt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (smellsYa == true)
        {
            var smelli = zombi.GetComponent(activatedScript);
            smelli.SendMessage(activatedFunction);
            smellsYa = false;
        }
        if(smellsNa == true)
        {
            var smelli = zombi.GetComponent(activatedScript);
            smelli.SendMessage(deactivatedFunction);
            smellsNa = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i=0; i < targetTags.Length; i++)
        {
            if (other.gameObject.tag == targetTags[i])
            {
                smelt = other.gameObject;
                smellsYa = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < targetTags.Length; i++)
        {
            if (collision.gameObject.tag == targetTags[i])
            {
                smelt = collision.gameObject;
            }
        }
    }
}
