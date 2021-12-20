using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour
{
    public int hp;
    public int ammo;
    public int Energy;
    public GameObject blueKeyCard;
    public GameObject corpse;
    public int maxHp;
    public int maxEnrgy;
    public int lastHp;
    private float hurtTimer;
    public int maxAmmo;
    public bool invulnerable;
    // Start is called before the first frame update
    void Start()
    {
        maxEnrgy = Energy;
        maxHp = hp;
        maxAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0 && gameObject.tag != "Player")
        {
            GameObject corpseBb = Instantiate(corpse, transform.position, transform.rotation);
            var multiTag = gameObject.GetComponent<CustomTag>();
            var crops = corpseBb.GetComponent<beingGrabbed>();
            //crops.corpseHp = maxHp;
            if (multiTag != null && multiTag.HasTag("blueKey"))
            {
                Instantiate(blueKeyCard, transform.position, transform.rotation);
            }
                Destroy(gameObject);            
        }
        if(hp > maxHp)
        {
            hp = maxHp;
        }
        if(Energy > maxEnrgy)
        {
            Energy = maxEnrgy;
        }
        var colour = GetComponent<Renderer>();
        if (hp < lastHp)
        {
            
            colour.material.color = Color.red;
            hurtTimer = Time.time;
        }
        else if (hurtTimer < Time.time -1/2)
        {
            colour.material.color = Color.white;
        }
        if(invulnerable && hp < lastHp)
        {
            hp = lastHp;
        }
        lastHp = hp;

    }
    void OnDestroy()
    {

    }
}
