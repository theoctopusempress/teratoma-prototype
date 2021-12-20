using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField] public fieldOfVeiw field;
	public float speed = 20.0f;
	private float horizontalInput;
    private float verticalInput;
	public GameObject BulletPrefab;
	public GameObject mossswordPrefab;
	public GameObject SoundWave;
	public float PlayerDirection;
	public Rigidbody2D playerRigidbody;
	private float slashTime = 0.0f;
	private bool Grabbing = false;
	public GameObject aimPointer;
	private GameObject pointing;
	public Slider progressBar;
	private int progress = 0;
	public int scrap = 0;
	public GameObject gameManager;
	private GameObject othered;
	private bool nearCorpse = false;
	private bool nearScrap = false;
	private int lastHp;
	public bool isStunned;
	public bool lastStunned;
	private float stunTimer;
	private Quaternion lastRotate;
	public Animator Animator;
	public Rigidbody2D rb;
	// Start is called before the first frame update
	void Start()
    {
        
    }
	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
     }
    // Update is called once per frame
    void Update()
    {
		var GM = gameManager.GetComponent<GameManager>();
		//for the field of view script
		var vitals = gameObject.GetComponent<Vitals>();
		if (isStunned == false) { 
        field.SetOrigin(transform.position);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

		
		float xForce = speed * horizontalInput * Time.deltaTime;
		float yForce = speed * verticalInput * Time.deltaTime;
		if(Grabbing == true)
        {
			xForce = xForce /2;
			yForce = yForce / 2;
		}
		Vector2 force = new Vector2 (xForce, yForce);

		playerRigidbody.AddForce(force);

		//transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime, 0);	


		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		lastRotate = transform.rotation;
		//Destroy(GameObject.FindWithTag("Noise"));
		if (Input.GetButtonDown("Fire2"))
        {
			Animator.SetTrigger("startaim");
			pointing = Instantiate(aimPointer);
			var thePoint = pointing.GetComponent<PlayerLazer>();
			thePoint.witness = gameObject;
		} else if (Input.GetButton("Fire2"))
            {

            }
			
			if (Input.GetButtonUp("Fire2")) {
				if (GM.ammo > 0)
				{
					Instantiate(BulletPrefab, transform.position, transform.rotation);
					BulletPrefab.GetComponent<BulletNyooms>().source = gameObject;
					Instantiate(SoundWave, gameObject.transform.position, transform.rotation);
					GM.ammo--;
				}
				Animator.SetTrigger("shoot");
				Destroy(pointing);
		}
		if (Input.GetButtonDown("Fire1") && slashTime < Time.time)
		{
			Animator.SetTrigger("slash");
			Instantiate(mossswordPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
			slashTime = Time.time + 0.25f;
		}
        if (Input.GetKeyDown(KeyCode.R) && vitals.Energy >0)
        {
			vitals.Energy -= 25;
			GM.ammo += 4;
			if(GM.ammo > GM.maxAmmo)
            {
				GM.ammo = GM.maxAmmo;
            }
        }
		if (Input.GetKeyDown(KeyCode.Q) && vitals.Energy > 0)
		{
			vitals.Energy -= 25;
			vitals.hp += 25;
		}

	if(nearCorpse == true)
        {
			if (Input.GetKeyDown(KeyCode.G))
			{

				var multiTag = othered.gameObject.GetComponent<CustomTag>();
				if (multiTag.HasTag("Grabbed"))
				{
					multiTag.tags.Remove("Grabbed");
					Grabbing = false;
				}
				else if (Grabbing == false)
				{
					multiTag.tags.Add("Grabbed");
					Grabbing = true;
				}

			}
			if (Input.GetKey(KeyCode.E))
			{
				var crops = othered.GetComponent<beingGrabbed>();
				crops.corpseHp -= 1;
				vitals.Energy += 1;
				progressBar.maxValue = crops.corpseMaxHp;
				progressBar.value = crops.corpseHp;
				if (crops.corpseHp == 0)
				{
					Destroy(othered.gameObject);

					Grabbing = false;
				}
			}
		}
	if(nearScrap)
        {
			if (Input.GetKeyDown(KeyCode.E))
            {
				Animator.SetTrigger("interac");
			}
			if (Input.GetKey(KeyCode.E))
			{
				progress++;
				progressBar.maxValue = 100;
				progressBar.value = progress;
				if (progress == 100)
				{
					Destroy(othered.gameObject);
					scrap += 10;
					//progressBar.value = 0;
					progress = 0;
				}
			}
		}
		}
		if (vitals.hp <= 0 && vitals.lastHp >0) //what happens when you die needs reworking
		{
			Animator.SetTrigger("ded");
			//Time.timeScale = 0f;
			isStunned = true;
			stunTimer = Time.time + Mathf.Infinity;

			vitals.invulnerable = true;
		}
		else if (vitals.hp < lastHp && isStunned == false)
        {
			Animator.SetTrigger("dmg");
			isStunned = true;
			stunTimer = Time.time + 0.5f;
        }
		if(stunTimer <= Time.time)
        {
			isStunned = false;
        }
		if (isStunned == true && lastStunned == true)
		{
			vitals.hp = lastHp;
			transform.rotation = lastRotate;
		}
		lastStunned = isStunned;
		lastHp = vitals.hp;

		if(rb.velocity.magnitude > 0.5)
        {
			Animator.SetBool("is walking", true);

		} else if (rb.velocity.magnitude <= 0.5)
        {
			Animator.SetBool("is walking", false);
		}

	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Corpse")
		{
			nearCorpse = true;
			othered = other.gameObject;
		}
		if (other.gameObject == null)
		{
			Grabbing = false;
			nearCorpse = false;
		}
		if(other.gameObject.tag == "scrapHeap")
        {
			nearScrap = true;
			othered = other.gameObject;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "scrapHeap" || other.gameObject.tag == "Corpse")
		{
			progressBar.value = 0;
		}
		if (other.gameObject.tag == "Corpse")
        {
			nearCorpse = false;
		}
		if (other.gameObject.tag == "scrapHeap")
		{
			nearScrap = false;
		}
	}
}
