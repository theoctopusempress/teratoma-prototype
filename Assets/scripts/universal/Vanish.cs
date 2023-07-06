using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanish : MonoBehaviour
{
    private int frame = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frame == 1)
        {
            Destroy(gameObject);
        }
        frame++;
    }
}
