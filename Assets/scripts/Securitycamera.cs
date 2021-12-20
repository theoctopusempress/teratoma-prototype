using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Securitycamera : MonoBehaviour
{
    [SerializeField] public fieldOfVeiw field;
    public GameObject Player;
    public int sightDistance = 10;
    public int sightAngle = 45;
    public GameObject SoundWave;
    public GameObject masterController;
    public bool selfDestuct = false;
    public bool friendly = false;
    public bool lastFriendly;
    public fieldOfVeiw FOV;
    public GameObject fovLazer;
    public bool rotating = false;
    public int rotationAngle;
    public float roteSpeed = 10.0f;
    private bool turningLeft = false;
    Quaternion rotationAmount1;
    Quaternion postRotation1;
    Quaternion rotationAmount2;
    Quaternion postRotation2;
    public GameObject Explosion;
    private float exposionRadius = 5.0f;


    //SpawnController spawnController;
    // Start is called before the first frame update

    void Start()
    {
        masterController = GameObject.FindWithTag("masterController");
        Player = GameObject.FindWithTag("Player");
        fovLazer = Instantiate(fovLazer);
        var laze = fovLazer.GetComponent<fovLazer>();
        laze.witness = gameObject;

         rotationAmount1 = Quaternion.Euler(0, 0, rotationAngle);
         postRotation1 = transform.rotation * rotationAmount1;
        rotationAmount2 = Quaternion.Euler(0, 0, -rotationAngle);
        postRotation2 = transform.rotation * rotationAmount2;
    }

    // Update is called once per frame
    void Update()
    {
        if (friendly == false && Player != null)
        {
            //sightcone
            Vector2 targetDirection = Player.transform.position - transform.position;
            float angel = Vector2.Angle(targetDirection, transform.up);
            if (angel < sightAngle)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);
                if (hit.collider.tag == "Player" && hit.distance < sightDistance)
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
                field = Instantiate(FOV);
            }
            field.SetOrigin(transform.position);
            field.SetAimDirection(transform.rotation);
        }
        lastFriendly = friendly;
        if (rotating == true)
        {
            if (turningLeft == false)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, postRotation1, Time.deltaTime * roteSpeed);
                if (transform.rotation == postRotation1)
                {
                turningLeft = true;
                }
            }
        else if (turningLeft == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, postRotation2, Time.deltaTime * roteSpeed);
            if (transform.rotation == postRotation2)
            {
                turningLeft = false;
            }
            }
        }
    }
    void Destruction()
    {
        GameObject newExplosion = Instantiate(Explosion, this.gameObject.transform.position, transform.rotation) as GameObject;
        newExplosion.transform.localScale = new Vector2(exposionRadius, exposionRadius);
        selfDestuct = true;
    }
}
