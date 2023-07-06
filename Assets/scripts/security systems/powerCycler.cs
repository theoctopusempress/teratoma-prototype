using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerCycler : MonoBehaviour
{
    public GameObject elivator;
    private bool hasCycled;
    public doorCode[] doors;
    public zombaddie[] zombs;
    // Start is called before the first frame update
    void Start()
    {
        
        doors = GetComponentsInChildren<doorCode>();
        zombs = GetComponentsInChildren<zombaddie>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void cyclePower()
    {
        if (hasCycled == false)
        {
            if (elivator != null)
            {
                var eli = elivator.GetComponent<elivatorconsole>();

                eli.currentPower++;
                if (eli.currentPower >= eli.powerNeeded)
                {
                    eli.locked = false;
                }
            }
        
            hasCycled = true;

            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].locked = false;
            }
            for (int i= 0; i < zombs.Length; i++)
            {
                zombs[i].redie();
                zombs[i].lockedAway = false;
            }
        }
    }
}
