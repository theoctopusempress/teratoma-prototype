using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
	public LayerMask Behindmask;
	public GameObject SoundWave;
	public GameObject BulletPrefab;
	private Animator baddieAnim;
	public LayerMask bullet;
	public bool alert = false;
	private bool goHome = false;
	private float shootTimer  = 0.0f;
	private float sightTimer = 0.0f;
	//public GameObject Player;
	// The target marker.
	public Transform Player;
	public int enemyType;
    // Angular speed in radians per sec.
    public float roteSpeed = 10.0f;
	public int sightDistance = 10;
	public int sightAngle = 45;
	public Rigidbody2D baddieRigidbody;
	public Vector3 startPosition;
	private Quaternion startRotate;
	//public Transform target;
	public float speed = 200f;
	public float nextWaypointDistance = 3f;
	Path path;
	int currentWaypoint = 0;
	//bool reachedEndOfPath = false;
	Seeker seeker;
	Rigidbody2D rb;
	public Vector2 direction;
	private Vector2 transDirection;
	private Vector2 force;
	public Vector3 pathDestination;
	private Vector2 soundSpot;
	private Vector3 lastKnownPosition;
	public GameObject masterController;
	public GameObject[] corpses;
	public GameObject fovLazer;
	private float lastRot_z;
	private float secLastRot_z;
	private bool sightcastHit;

	// Start is called before the first frame update
	void Start()
    {
       baddieAnim = GetComponent<Animator>();
	   Player = GameObject.Find("Player").transform;
	   startPosition = transform.position;
		startRotate = transform.rotation;
		pathDestination = startPosition;
		//stuff for pathing
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		//target = gameObject.Find("Player");
		masterController = GameObject.FindWithTag("masterController");
		InvokeRepeating("UpdatePath", 0f, 0.5f);
		//make the visible vision cones
		if (fovLazer != null)
		{
			fovLazer = Instantiate(fovLazer);
			var laze = fovLazer.GetComponent<fovLazer>();
			laze.witness = gameObject;
		}
	}
	//draws the path for movement
	void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}
	void UpdatePath()
	{
		if (seeker.IsDone())
		{
			seeker.StartPath(rb.position, pathDestination, OnPathComplete);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		//sightcone
		Vector2 targetDirection = Player.position - transform.position;
		float angel = Vector2.Angle(targetDirection, transform.up);
		if (angel < sightAngle) {
			for (int i = 0; i < 5; i++)
			{
				var rotation = transform.rotation;
				rotation *= Quaternion.Euler(0, 0, i * 2 - 5);
				RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);
				if (hit.collider.tag == "Player" && hit.distance < sightDistance)
                {
					sightcastHit = true;
                }
			}
			//RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);
			//, sightDistance, ~Behindmask);
			if (sightcastHit){
				
				if(alert == false)
                {
					Instantiate(SoundWave, gameObject.transform.position, transform.rotation);
				}
				alert = true;
				sightDistance = 20;
				sightTimer = Time.time + 5;
				pathDestination = Player.position;
				lastKnownPosition = Player.position;
				var spawn = masterController.GetComponent<SpawnController>();
				spawn.alarmTimer = Time.time + 30;
			}
			sightcastHit = false;
			if (alert == true)
			{
				if (sightTimer == Time.time + 5)
				{
					AttackMode();
				}
				else if (sightTimer < Time.time + 5)
				{
					pathDestination = lastKnownPosition;
				}
			}
		}
		if (sightTimer < Time.time && alert == true)
		{
			goHome = true;
			alert = false;
			sightDistance = 10;
			pathDestination = startPosition;
		}
		if (enemyType != 1)
		{
			//finds corpses so we can see them
			corpses = GameObject.FindGameObjectsWithTag("Corpse");
			foreach (GameObject corpse in corpses)
			{
				targetDirection = corpse.transform.position - transform.position;
				angel = Vector2.Angle(targetDirection, transform.up);
				if (angel < sightAngle)
				{
					RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);
					var multiTag = hit.transform.gameObject.GetComponent<CustomTag>();
					if (hit.collider.tag == "Corpse" && alert == false && multiTag.HasTag("undiscoveredBody") && hit.distance < sightDistance)
					{

						Instantiate(SoundWave, hit.transform.position, transform.rotation);
					}
				}
			}
		}
		if(goHome == true)
        {
			//returnToPost();
        }
		//more pathing bs
		if (path == null)
		{
			return;
		}
		direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		transDirection = (Vector2)path.vectorPath[currentWaypoint];
		//makes the npc look towards the point next waypoint

		force = direction * speed * Time.deltaTime;
		//transform.rotation = Quaternion.LookRotation(direction);
		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

		if (distance > 0.1)
		{
			rb.AddForce(force);
		}
		if (distance < nextWaypointDistance && currentWaypoint+1 < path.vectorPath.Count)
        {
				currentWaypoint++;
		}
		//makes the npc face the direction they are moving
		//if (direction.x != 0 && direction.y != 0 )
			//&& Vector3.Distance(transform.position, direction) < .1)
		{
			float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			
			if (lastKnownPosition != Player.position && Mathf.Abs(rot_z - lastRot_z) < 3 && Mathf.Abs(rot_z - secLastRot_z) < 3)
			{
				//Debug.Log(direction);
				transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			}
			secLastRot_z = lastRot_z;
			lastRot_z = rot_z;

		}
		//after investigating a noise, they will return to their post (add a means for them to actually see the point with raycast and have them look back and fourth to check for player)
		if (Vector3.Distance(transform.position, soundSpot) < 1)
		{
			pathDestination = startPosition;
		}
		if (Vector3.Distance(transform.position,startPosition) <.1 && pathDestination == startPosition)
		{
			transform.rotation = startRotate;
		}
	}
	//lisens for player noises
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Noise" && alert == false) {
		pathDestination = other.gameObject.transform.position;
		soundSpot = other.gameObject.transform.position;
			
			//alert = true;
		}
		if(other.GetComponent<CustomTag>() != null) {
			var multiTag = other.GetComponent<CustomTag>();

			if (multiTag.HasTag("undiscoveredBody"))
			{
				multiTag.tags.Remove("undiscoveredBody");
			}
		}
	}	
	//IEnumerator AttackMode() {
	void AttackMode () {
		//UpdatePath();
		
			if (shootTimer < Time.time && enemyType != 1) {
			baddieAnim.SetTrigger("knife_Trig");
			//shoot a bullet
			GameObject.Instantiate(BulletPrefab, transform.position, transform.rotation);
				BulletPrefab.GetComponent<baddieBulletNyooms>().source = gameObject;	
				shootTimer = Time.time +1;
				
			}

		Vector3 targetDirection = Player.position - transform.position;
		float dist = Vector2.Distance(Player.position, transform.position);
		if (dist < 8 && enemyType != 1)
		{
			rb.AddForce(-force * 2);
		}

		// Determine which direction to rotate towards
		// The step size is equal to speed times frame time.

		float singleStep = roteSpeed * Time.deltaTime;
		float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg -90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * roteSpeed);
		//transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

	}

	void returnToPost()
	{
		Vector3 targetDirection = startPosition - transform.position;
		float singleStep = roteSpeed * Time.deltaTime;
		float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10000);
		//transform.Translate(Vector2.up * Time.deltaTime * 5);
		
		if (Vector3.Distance(transform.position,startPosition) < .1 && transform.position != startPosition)
        {
			transform.position = startPosition;
			transform.rotation = startRotate;
			rb = GetComponent<Rigidbody2D>();
			rb.velocity = Vector3.zero;
			goHome = false;
        }
		if (alert == true)
        {
			goHome = false;
        }
	}
}