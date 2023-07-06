using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class myAnimator : MonoBehaviour
{
    public Texture2D animationKey;
    public Sprite[] animationframe;
    public int[] startpoints;
    public string[] startName;
    public SpriteRenderer spriteRenderer;
    public bool animating;
    int spriteNum;
    int startPoint;
    int oneFramePer =3;
    int onePerFrame;
    int lastStart;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (animationKey != null)
        {
            animationframe = Resources.LoadAll<Sprite>(animationKey.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animating)
        {
            if (spriteNum < startpoints[startPoint])
            {
                if (onePerFrame == oneFramePer)
                {
                    spriteRenderer.sprite = animationframe[spriteNum];
                    spriteNum++;
                    onePerFrame = 0;
                }
                else { onePerFrame++; }
            }
            else { animating = false; }
        }
        var animed = this.GetComponent<myAnimator>();
    }
    public void Animate(int start)
    {
        if (animating == false || lastStart != start)
        {
            animating = true;
            //this.gameObject.GetComponent<animator>().enabled = false;
            startPoint = start + 1;
            spriteNum = startpoints[start];
            lastStart = start;
        }
    }
    public void startFromName(string name)
    {
        Animate(System.Array.IndexOf(startName, name));
    }
}