using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNyooms : MonoBehaviour
{
	public GameObject Player;
	public GameObject Baddie;
	public GameObject wall;
    // Start is called before the first frame update
	public float speed = .05f;
	public int damage = 25;
	public AudioSource laserShot;
	public int friendOrFoe;
	void Start()
    {
		Player = GameObject.FindWithTag("Player");
		Baddie = GameObject.FindWithTag("Enemy");
		wall = GameObject.FindWithTag("wall");
		laserShot.Play ();
		var animed = this.GetComponent<myAnimator>();
		animed.Animate(friendOrFoe);
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed);
		if(Vector2.Distance(transform.position, Player.transform.position)>100){
			Destroy(gameObject);
		}
		var animed = this.GetComponent<myAnimator>();

		if (animed.animating == false)
		{
			animed.Animate(friendOrFoe);
		}
	}
	void OnTriggerEnter2D(Collider2D other) {		
			if((other.gameObject.tag == "Enemy" && friendOrFoe == 0)||(other.gameObject.tag== "Player" && friendOrFoe == 1)){
				var vitals = other.gameObject.GetComponent<Vitals>();
				vitals.hp -= damage;
			}
			if( other.gameObject.tag == "wall") {	
				Destroy(gameObject);
			}		
	}
}