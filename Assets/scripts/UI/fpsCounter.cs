using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fpsCounter : MonoBehaviour
{
    public Text fpsText;
    int frameCount = 0;
    float frameTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if(frameTimer < Time.time)
        {
            frameTimer = Time.time + 1;
            fpsText.text = frameCount.ToString();
            frameCount = 0;
        }
    }
}
