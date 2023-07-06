using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	//[SerializeField] public fieldOfVeiw field;
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
	//public Animator Animator;
	public Rigidbody2D rb;
	public bool youOnlyDieOnce;
	public CircleCollider2D circle;
	GameObject mossSword;
	swordSlashes mssSwrd;
	public Sprite[] spritelist;
	public SpriteRenderer spriteRenderer;
	public bool crouching;
	bool aiming;
	public AudioSource reload;
	public AudioSource bodyDrag;
	public AudioSource glitch1;
	// Start is called before the first frame update
	void Start()
    {
	}
	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
     }
    // Update is called once per framen
    void Update()
    {
		var GM = gameManager.GetComponent<GameManager>();
		var vitals = gameObject.GetComponent<Vitals>();
		if (isStunned == false) { 
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
		float xForce = speed * horizontalInput * Time.deltaTime;
		float yForce = speed * verticalInput * Time.deltaTime;
			var anime = this.GetComponent<myAnimator>();

			if (Grabbing||crouching)
        {
			xForce /=2;
			yForce /=2;
		}
		Vector2 force = new Vector2 (xForce, yForce);
			force = Vector2.ClampMagnitude(force, speed);
			rb.velocity += force;
			if(horizontalInput == 0 && verticalInput == 0)
            {
				rb.velocity -= rb.velocity;
            }

		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		lastRotate = transform.rotation;
            //Destroy(GameObject.FindWithTag("Noise"));
            if (Input.GetKeyDown(KeyCode.C) && !Grabbing)
            {
				crouching = !crouching;
			}
		if (Input.GetButtonDown("Fire2"))
        {
			//var anime = this.GetComponent<myAnimator>();
			anime.startFromName("aim");
			pointing = Instantiate(aimPointer);
			var thePoint = pointing.GetComponent<PlayerLazer>();
			thePoint.witness = gameObject;
			aiming = true;
		}
			if (Input.GetButtonUp("Fire2")) {
				if (GM.ammo > 0)
				{
					GameObject bullet =Instantiate(BulletPrefab, transform.position, transform.rotation);
					var bull = bullet.GetComponent<BulletNyooms>();
					bull.friendOrFoe = 0;
					Instantiate(SoundWave, gameObject.transform.position, transform.rotation);
					GM.ammo--;
				} else if(GM.ammo <= 0)
                {
					glitch1.Play();
                }
				//var anime = this.GetComponent<myAnimator>();
				anime.startFromName("fire");
				aiming = false;
		}
			//var anime = this.GetComponent<myAnimator>();
			if (aiming && !anime.animating)
            {
				anime.startFromName("aimHold");
            }
            if (!aiming)
            {
				Destroy(pointing);
			}
			if (Input.GetButtonDown("Fire1") && slashTime < Time.time)
		{
			mossSword = Instantiate(mossswordPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
			slashTime = Time.time + 0.25f;
			mssSwrd = mossSword.GetComponent<swordSlashes>();
			mssSwrd.blocking = false;
		}
		if(mssSwrd != null)
            {
				float e = (float)mssSwrd.spriteNum / mssSwrd.spritelist.Length * spritelist.Length;
				if ((int)e < spritelist.Length)
				{
					spriteRenderer.sprite = spritelist[(int)e];
				}
            }
			if (Input.GetKeyDown(KeyCode.B))
			{
				mossSword = Instantiate(mossswordPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
				mssSwrd = mossSword.GetComponent<swordSlashes>();
				mssSwrd.blocking = true;
			}
            if (!Input.GetKey(KeyCode.B) && mssSwrd != null &&mssSwrd.blocking== true)
            {
				Destroy(mossSword);
            }
			if (Input.GetKeyDown(KeyCode.R) && vitals.Energy >0 && GM.ammo < GM.maxAmmo)
        {
			vitals.Energy -= 25;
			GM.ammo += 4;
			if(GM.ammo > GM.maxAmmo)
            {
				GM.ammo = GM.maxAmmo;
            }
				reload.Play();
		}
		else if (Input.GetKeyDown(KeyCode.R) && vitals.Energy <= 0)
		{
			glitch1.Play();
		}

			if (Input.GetKeyDown(KeyCode.Q) && vitals.Energy > 0 && vitals.hp < vitals.maxHp)
		{
			vitals.Energy -= 25;
			vitals.hp += 25;
		}
		else if (Input.GetKeyDown(KeyCode.Q) && vitals.Energy <= 0)
		{
			glitch1.Play();
		}

			if (nearCorpse == true)
        {
			if (Input.GetKeyDown(KeyCode.G))
			{
				var multiTag = othered.GetComponent<CustomTag>();
				if (multiTag.HasTag("Grabbed"))
				{
					multiTag.tags.Remove("Grabbed");
					Grabbing = false;
				}
				else if (Grabbing == false && !crouching)
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
					Destroy(othered);

					Grabbing = false;
				}
			}
		}
	if(nearScrap)
        {
			if (Input.GetKeyDown(KeyCode.E))
            {
					//var anime = this.GetComponent<myAnimator>();
					anime.startFromName("interact");
			}
			if (Input.GetKey(KeyCode.E))
			{
				progress++;
				progressBar.maxValue = 100;
				progressBar.value = progress;
				if (progress == 100)
				{
					Destroy(othered);
					scrap += 10;
					//progressBar.value = 0;
					progress = 0;
				}
			}
		}
		}
		if (vitals.hp <= 0 && youOnlyDieOnce == false) //what happens when you die needs reworking
		{
			//Time.timeScale = 0f;
			isStunned = true;
			stunTimer = Time.time + Mathf.Infinity;
			//vitals.invulnerable = true;
			youOnlyDieOnce = true;
			circle.enabled = false;
			var anime = this.GetComponent<myAnimator>();
			anime.startFromName("die");
		}
		else if (vitals.hp < lastHp && isStunned == false)
        {
			var anime = this.GetComponent<myAnimator>();
			anime.startFromName("damage");
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
			var anime = this.GetComponent<myAnimator>();
			if (!anime.animating)
			{
				if (!crouching)
				{
					anime.startFromName("walk");
				} else { anime.startFromName("crouchWalk1"); }
			}
			if (Grabbing && !bodyDrag.isPlaying)
            {
				bodyDrag.Play();
            }
            else if (!Grabbing)
            {
				bodyDrag.Pause();
            }
		} else if (bodyDrag.isPlaying)
        {
			bodyDrag.Stop();
        }
		var animed = this.GetComponent<myAnimator>();
		if (!animed.animating && !youOnlyDieOnce)
		{
			if (!crouching)
			{
				animed.startFromName("idle");
			} else
            {
				animed.startFromName("crouch");
			}
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Corpse"))
		{
			nearCorpse = true;
			othered = other.gameObject;
		}
		if (other.gameObject == null)
		{
			Grabbing = false;
			nearCorpse = false;
		}
		if(other.gameObject.CompareTag("scrapHeap"))
        {
			nearScrap = true;
			othered = other.gameObject;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("scrapHeap") || other.gameObject.CompareTag("Corpse"))
		{
			progressBar.value = 0;
		}
		if (other.gameObject.CompareTag("Corpse"))
        {
			nearCorpse = false;
		}
		if (other.gameObject.CompareTag("scrapHeap"))
		{
			nearScrap = false;
		}
	}
}