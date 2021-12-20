using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baddieBulletNyooms : MonoBehaviour
{
	public GameObject Player;
	public GameObject Baddie;
	public GameObject source;
	public  GameObject wall;
    // Start is called before the first frame update
	public float speed = .05f;
	public int damage = 25;
    void Start()
    {
		Baddie = GameObject.FindWithTag("Player");
		Player = GameObject.FindWithTag("Enemy");
		wall = GameObject.FindWithTag("wall");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed);
		if (Player != null)
		{
			if (Vector2.Distance(transform.position, Player.transform.position) > 100)
			{
				//Destroy(gameObject);
			}
		}
    }
	void OnTriggerEnter2D(Collider2D other) {
		if( other.gameObject != source) {
			if(other.gameObject.tag == "Player" ){
				//Destroy(other.gameObject);
				var vitals = other.gameObject.GetComponent<Vitals>();
				vitals.hp -= damage;
			}
		//Destroy(gameObject);
		}
		if( other.gameObject.tag == "wall") {	
			Destroy(gameObject);
		}
		//Destroy(other.gameObject);
		
	
	}
}