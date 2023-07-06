using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
	//this script governs the behavior of the standard enemy ai unit in the game
	public GameObject SoundWave;
	public GameObject BulletPrefab;
	//private Animator baddieAnim;
	myAnimator myAnimator;
	public LayerMask bullet;
	public bool alert = false;
	public bool startpath = true;
	public float shootTimer  = 0.0f;
	private float sightTimer = 0.0f;
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
	public float speed = 200f;
	public float nextWaypointDistance = 1f;
	Path path;
	int currentWaypoint = 0;
	Seeker seeker;
	Rigidbody2D rb;
	public Vector2 direction;
	private Vector2 force;
	public Vector3 pathDestination;
	Vector3 lastPathDesination;
	private Vector2 soundSpot;
	private Vector3 lastKnownPosition;
	public bool sheWasRightThere;
	public GameObject masterController;
	public GameObject[] corpses;
	public List<GameObject> smellables = new List<GameObject>();
	public GameObject fovLazer;
	private float lastRot_z;
	private float secLastRot_z;
	private bool sightcastHit;
	//public Sprite[] damageAnimation;
	public SpriteRenderer spriteRenderer;
	public LayerMask IgnoreThisSee;
	public LayerMask IgnoreThisWalk;
	public GameObject smeller;
	//int animationframe =0;
	//bool damageAnim;
	// Start is called before the first frame update
	void Start()
    {
		//baddieAnim = GetComponent<Animator>();
		myAnimator = GetComponent<myAnimator>();
		Player = GameObject.Find("Player").transform;
		startPosition = transform.position;
		startRotate = transform.rotation;
		pathDestination = startPosition;
		//stuff for pathing
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
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
				RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection ,1000f,~IgnoreThisSee);
				var play = Player.GetComponent<PlayerController>();
				float tempSightDistance;
                if (play.crouching)
                {
					tempSightDistance = sightDistance /2;
                } else
                {
					tempSightDistance = sightDistance;
                }
				if (hit.collider.tag == "Player" && hit.distance < tempSightDistance)
                {
					sightcastHit = true;
                }
			}
			if (sightcastHit){
				
				if(alert == false)
                {
					//alerts other ai to position when it sees the player
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
			//while it can see the player it will attack but if sight is lost for five seconds, it will go to the last position it saw the player
			if (alert == true)
			{
				startpath = false;
				if (sightTimer == Time.time + 5)
				{
					AttackMode();
				}
				else if (sightTimer < Time.time + 5)
				{
					pathDestination = lastKnownPosition;
                    sheWasRightThere = true;
                }
            }
		}
		if(Vector2.Distance(transform.position, pathDestination) < 1 && sheWasRightThere )//&&pathDestination != lastPathDesination)
        {
			sheWasRightThere = false;
		}
		//after checking for the player, it will return to initial location
		if (sightTimer < Time.time && alert == true && sheWasRightThere == false)
		{
			alert = false;
			sightDistance = 10;
			pathDestination = startPosition;
			startpath = true;
		}
		//will react to dead bodies by investigating their position and alerting others, "undead" enemy types will ignore this
		if (enemyType != 1)
		{
			//finds corpses so we can see them
			corpses = GameObject.FindGameObjectsWithTag("Corpse");
			smellables.RemoveAll(s => s == null);
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

		//more pathing code
		if (path == null)
		{
			return;
		}
		direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		//makes the npc look towards the point next waypoint

		force = direction * speed * Time.deltaTime;
		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

		if (distance > 0.1)
		{
			rb.AddForce(force);
		}
		if (distance < nextWaypointDistance && currentWaypoint+1 < path.vectorPath.Count)
        {
			targetDirection = path.vectorPath[currentWaypoint] - transform.position;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, 1000f, ~IgnoreThisWalk);
			if (hit.distance > Vector2.Distance(rb.position, path.vectorPath[currentWaypoint+1]))
			{
				currentWaypoint++;
			}
		}
		//makes the npc face the direction they are moving
			float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			
			if (lastKnownPosition != Player.position && Mathf.Abs(rot_z - lastRot_z) < 3 && Mathf.Abs(rot_z - secLastRot_z) < 3)
			{
				transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			}
			secLastRot_z = lastRot_z;
			lastRot_z = rot_z;
		
		//after investigating a noise, they will return to their post (add a means for them to actually see the point with raycast and have them look back and fourth to check for player)
		if (Vector3.Distance(transform.position, soundSpot) < 1)
		{
			pathDestination = startPosition;
			startpath = true;
		}
		if (Vector3.Distance(transform.position,startPosition) <.1 && pathDestination == startPosition)
		{
			transform.rotation = startRotate;
		}
		var Vitals = this.gameObject.GetComponent<Vitals>();
		if(Vitals.hp < Vitals.lastHp)
        {
			//stunned = true;
			myAnimator.Animate(3);
		}
		if (rb.velocity.magnitude > 0.5)
		{
			if (myAnimator.animating == false)
			{
				myAnimator.Animate(1);
			}
		}
		lastPathDesination = pathDestination;

	}
	//lisens for player noises
	private void OnTriggerEnter2D(Collider2D other) { 
	//if (Vector2.Distance(other.transform.position, transform.position) < 1)
	{
		if (other.gameObject.tag == "Noise" && alert == false)
			{
				startpath = false;
				pathDestination = other.gameObject.transform.position;
				soundSpot = other.gameObject.transform.position;
			}
			//each dead body remembers if it has been discovered already and ai will only react to undiscovered bodies, this prevents them from repeatidly being shocked at the same body
			if (other.gameObject.GetComponent<CustomTag>() != null)
			{
				var multiTag = other.gameObject.GetComponent<CustomTag>();

				if (multiTag.HasTag("undiscoveredBody"))
				{
					multiTag.tags.Remove("undiscoveredBody");
				}
				if (multiTag.HasTag("partOfPlayer") && !multiTag.HasTag("Sword"))
				{
					shootTimer = Time.time + 1;
					AttackMode();
				}
			}
		}
	}
    private void IsmellSomething()
    {
		var smellin = GetComponentInChildren<zombSniff>();
		if(smellin.smelt.tag == "Player")
        {

        }
		if (smellin.smelt.tag == "Corpse") 
		{
			if (!smellables.Contains(smellin.smelt)){
				smellables.Add(smellin.smelt);
			}
		}
    }
	void IsmellNothing()
    {
		var smellin = GetComponentInChildren<zombSniff>();
		if (smellables.Contains(smellin.smelt))
        {
			smellables.Remove(smellin.smelt);
        }
    }
    //function for combat with player
    public void AttackMode () {
		
			if (shootTimer < Time.time && enemyType != 1) {
				myAnimator.Animate(2);
				//shoot a bullet
				GameObject bullet =Instantiate(BulletPrefab, transform.position, transform.rotation);
				var bull = bullet.GetComponent<BulletNyooms>();
				bull.friendOrFoe = 1;
				shootTimer = Time.time +1;				
			}
		//moves towards player but will back up when too close to evade the player's sword 
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
	}
}