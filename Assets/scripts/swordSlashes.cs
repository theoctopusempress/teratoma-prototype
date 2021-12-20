using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSlashes : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Baddie;
    private float startRotate;
    private float slashTimer = 0.0f;
    private Quaternion endRotate;
    public int damage = 25;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        //startRotate = transform.rotation.z - 45;
        //transform.rotation = Quaternion.Euler(0, 0, startRotate);
        transform.position = transform.position + transform.up * 2;
         endRotate = transform.rotation * Quaternion.Euler(0, 0, -90);
        //Baddie = GameObject.Baddie;
        slashTimer = Time.time + 0.25f;
        Baddie = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, endRotate, Time.deltaTime * 1);
        transform.RotateAround(Player.transform.position, Vector3.forward, -360 * Time.deltaTime);
    if(slashTimer < Time.time)
        {
            Destroy(gameObject);
        }
        transform.position = Player.transform.position + transform.up / 2;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var vitals = other.gameObject.GetComponent<Vitals>();
            vitals.hp -= damage;
        }
        boss1AI script = other.GetComponent<boss1AI>();
        if (script != null)
        {
            script.rb.velocity = Vector3.zero;
            Debug.Log("parry this casual");
        }
    }
}
