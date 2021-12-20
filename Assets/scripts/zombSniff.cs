 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombSniff : MonoBehaviour
{
    public GameObject zombi;
    public string activatedScript;
    public string activatedFunction;
    public string[] targetTags;
    private bool smellsYa;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = zombi.transform.position;
        if (smellsYa)
        {
            var smelli = zombi.GetComponent(activatedScript);
            smelli.SendMessage(activatedFunction);
            smellsYa = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i=0; i < targetTags.Length; i++)
        {
            if (other.gameObject.tag == targetTags[i])
            {
                smellsYa = true;
            }
        }

    }
}
