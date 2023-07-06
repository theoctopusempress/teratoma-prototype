using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMenu : MonoBehaviour
{
    public List<GameObject> tabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openThisTab(int whichTab)
    {
        for (int i= 0; i < tabs.Count; i++)
        {
            tabs[i].SetActive(false);
        }
        tabs[whichTab].SetActive(true);
    }
}
