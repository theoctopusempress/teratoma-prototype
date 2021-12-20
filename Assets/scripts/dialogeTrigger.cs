using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogeTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] sentences;
    private bool steppy = false;
    private bool lastSteppy = false;
    public GameObject Player;
    public GameObject dialoger;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        dialoger = GameObject.FindWithTag("dialoger");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == Player)
        {
            steppy = true;
            if (steppy == true && lastSteppy == false)
            {
                talkNow();
                lastSteppy = true;
            }
        }
    }
    void talkNow()
    {
        var dialoging = dialoger.gameObject.GetComponent<dialoger>();
        dialoging.sentences = sentences;
        dialoging.isTalking = true;
    }
}
