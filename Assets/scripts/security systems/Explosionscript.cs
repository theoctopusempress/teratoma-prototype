using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosionscript : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 100;
    public float explosionDiameter = 10;
    private float frameCount = 0;
    private float currentDiamter;
    public AudioSource explosion;
    void Start()
    {
        explosion.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount < 60)
        {
            currentDiamter = frameCount / explosionDiameter;
            transform.localScale = new Vector2(currentDiamter, currentDiamter);
            frameCount++;
        } else if (frameCount == 60)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        var vitals = other.gameObject.GetComponent<Vitals>();
        if (vitals != null)
        {
            vitals.hp -= damage;
        }

    }
}
