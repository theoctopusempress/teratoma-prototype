using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class portraitFace : MonoBehaviour
{
    public GameObject Player;
    public Image m_Image;
    public Sprite[] spritelist;
    private float spriteNum;
    public GameObject dialoger;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var talker = dialoger.GetComponent<dialoger>();
        //int whoseTalk;
        //if (talker.whoseTalk[talker.whichSentence] != null)
        //{
          //  whoseTalk = talker.whoseTalk[talker.whichSentence];
        //}
       // else { whoseTalk = 0; }
        if (talker.isTalking == true && talker.whoseTalk[talker.whichSentence] == 1)
        {
            animator.SetBool("isTalking", true);
        } else
        {
            animator.SetBool("isTalking", false);
        }
        var vitals = Player.gameObject.GetComponent<Vitals>();
        spriteNum = Mathf.Floor(((float)vitals.hp / vitals.maxHp * 4));
        if(spriteNum < 0)
        {
            spriteNum = 0;
        }
        m_Image.sprite = spritelist[(int)spriteNum];
        animator.SetInteger("healthiness", (int)spriteNum);
    }
}