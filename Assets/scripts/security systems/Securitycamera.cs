using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Securitycamera : MonoBehaviour
{
    public GameObject Player;
    public int sightDistance = 10;
    public int sightAngle = 45;
    public GameObject SoundWave;
    public GameObject masterController;
    public bool selfDestuct = false;
    bool friendly = false;
    bool lastFriendly;
    public GameObject fovLazer;
    public bool rotating = false;
    public int rotationAngle;
    public float roteSpeed = 10.0f;
    bool turningLeft = false;
    Quaternion rotationAmount1;
    Quaternion postRotation1;
    Quaternion rotationAmount2;
    Quaternion postRotation2;
    public Quaternion currentRotation;
    public GameObject Explosion;
    private float exposionRadius = 5.0f;
    public Sprite[] spriteList;
    public SpriteRenderer spriteRenderer;
    //SpawnController spawnController;
    // Start is called before the first frame update

    void Start()
    {
        masterController = GameObject.FindWithTag("masterController");
        Player = GameObject.FindWithTag("Player");
        fovLazer = Instantiate(fovLazer);
        var laze = fovLazer.GetComponent<fovLazer>();
        laze.witness = gameObject;
        currentRotation = transform.rotation;
        currentRotation.z = currentRotation.z / 2;
        rotationAmount1 = Quaternion.Euler(0, 0, rotationAngle);
         postRotation1 = Quaternion.identity * rotationAmount1;
        //postRotation1.z = postRotation1.z / 2;
        rotationAmount2 = Quaternion.Euler(0, 0, -rotationAngle);
        postRotation2 = Quaternion.identity * rotationAmount2;
        //postRotation2.z = postRotation2.z / 2;
        //currentRotation = Quaternion.identity;

    }

    // Update is called once per frame
    void Update()
    {
        if (friendly == false && Player != null)
        {
            //sightcone
            Vector2 targetDirection = Player.transform.position - transform.position;
            float angel = Vector2.Angle(targetDirection, currentRotation * transform.up);
            if (angel < sightAngle)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);
                int tempSightDistance;
                var play = Player.GetComponent<PlayerController>();
                if (play.crouching)
                {
                    tempSightDistance = sightDistance / 2;
                }
                else { tempSightDistance = sightDistance; }
                if (hit.collider.tag == "Player" && hit.distance < tempSightDistance)
                {
                    Instantiate(SoundWave, hit.transform.position, transform.rotation);
                    var spawn = masterController.GetComponent<SpawnController>();
                    if (spawn.alarm == false)
                    {
                        spawn.alarm = true;
                    }
                }
            }
        }
    if(selfDestuct == true)
        {
            Destroy(gameObject);
        }
    if(friendly == true)
        {
            if(lastFriendly == false){
              //  field = Instantiate(FOV);
            }
            //field.SetOrigin(transform.position);
            //field.SetAimDirection(transform.rotation);
        }
        lastFriendly = friendly;
        if (rotating == true)
        {
            if (turningLeft == false)
            {
                //transform.rotation = Quaternion.Slerp(transform.rotation, postRotation1, Time.deltaTime * roteSpeed);
                currentRotation = Quaternion.Slerp(currentRotation, postRotation2, Time.deltaTime * roteSpeed);
                if(Mathf.Abs(currentRotation.eulerAngles.z-postRotation2.eulerAngles.z) < 1)
                //if (currentRotation == postRotation2)
                //if(Mathf.DeltaAngle(currentRotation.eulerAngles.z, transform.rotation.eulerAngles.z) >=45)
                {
                turningLeft = true;
                }
            }
        else if (turningLeft == true)
        {
                //transform.rotation = Quaternion.Slerp(transform.rotation, postRotation2, Time.deltaTime * roteSpeed);
                currentRotation = Quaternion.Slerp(currentRotation, postRotation1, Time.deltaTime * roteSpeed);
                if (Mathf.Abs(currentRotation.eulerAngles.z - postRotation1.eulerAngles.z) < 1)
                //if (currentRotation == postRotation1)
            //if (Mathf.DeltaAngle(currentRotation.eulerAngles.z, transform.rotation.eulerAngles.z) >= 45)
                {
                    turningLeft = false;
            }
            }
            float d = currentRotation.eulerAngles.z;
            if (d > 180)
            {
               d = d - 360;
            }
            int e = (int)Mathf.Round((d + 45) / 10)-1;
            if (e > 6)
            {
                e = 6;
            }
            if (e < 0) { e = 0; }
            spriteRenderer.sprite = spriteList[e];
        }
    }
    void Destruction()
    {
        GameObject newExplosion = Instantiate(Explosion, this.gameObject.transform.position, transform.rotation) as GameObject;
        newExplosion.transform.localScale = new Vector2(exposionRadius, exposionRadius);
        selfDestuct = true;
    }
}