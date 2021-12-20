using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beingGrabbed : MonoBehaviour
{
    public GameObject Player;
    public SpriteRenderer spriteRenderer;
    public int corpseHp;
    public int corpseMaxHp;
    public Sprite[] spritelist;
    private float spriteNum;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        corpseMaxHp = corpseHp;
    }

    // Update is called once per frame
    void Update()
    {
        var multiTag = gameObject.GetComponent<CustomTag>();
        if (multiTag.HasTag("Grabbed"))
        {
            transform.position = Player.transform.position;
        }
        spriteNum = Mathf.Floor(((float)corpseHp / corpseMaxHp * 2));
        spriteRenderer.sprite = spritelist[(int)spriteNum];
    }
}
