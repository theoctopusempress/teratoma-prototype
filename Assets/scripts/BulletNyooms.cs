using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNyooms : MonoBehaviour
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
		Player = GameObject.FindWithTag("Player");
		Baddie = GameObject.FindWithTag("Enemy");
		wall = GameObject.FindWithTag("wall");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed);
		if(Vector2.Distance(transform.position, Player.transform.position)>100){
			Destroy(gameObject);
		}
    }
	void OnTriggerEnter2D(Collider2D other) {
		
		if( other.gameObject != source) {
			if(other.gameObject.tag == "Enemy" ){
				var vitals = other.gameObject.GetComponent<Vitals>();
				vitals.hp -= damage;
			}
		if( other.gameObject.tag == "wall") {	
			Destroy(gameObject);
		}
		}
		//Destroy(other.gameObject);
		
	
	}
}