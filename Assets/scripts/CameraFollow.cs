using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject Player;
	 private Vector3 offset = new Vector3(0, 0, -10);
    private Vector3 onset = new Vector3(-5, -5, 0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + offset;
        if(Input.GetKey("left shift"))
        {
            transform.position += (Vector3)Camera.main.ScreenToViewportPoint(Input.mousePosition) *10 +onset;
        }
    }
}
