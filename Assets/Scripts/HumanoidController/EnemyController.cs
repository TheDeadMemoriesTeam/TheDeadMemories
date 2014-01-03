using UnityEngine;
using System.Collections;

public class EnemyController : HumanoidController
{

	public float shootDistance = 4f;            // Distance from where the player will be shot
	public float chaseSpeed = 5.5f;             // The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 3f;            // The amount of time to wait when the last sighting is reached.
	public float patrolSpeed = 2.5f;            // The nav mesh agent's speed when patrolling.
	public float patrolWaitTime = 0.5f;         // The amount of time to wait when the patrol way point is reached.
	

	protected PlayerController player;          // Reference to the player.
	private EnemySight enemySight;              // Reference to the EnemySight script.
	private NavMeshAgent nav;                   // Reference to the nav mesh agent.

	private bool shooting;
	private float chaseTimer;                   // A timer for the chaseWaitTime.
	private float patrolTimer;                  // A timer for the patrolWaitTime.

	private int wayPointIndex = 0;              // A counter for the way point array.
	private WaysNetwork waysNetwork;			// Object that will store all way points.
	private Vector3[] patrolWayPoints;        // An array of transforms for the patrol route.


	protected float timeCountAttack;
	protected float timeAttack;
	protected float probabilityAttack;
	protected int xp;
	
	public Rigidbody[] droppableItems;
	public float[] itemsDropProbability;
	
	private bool inCrypte = false;

	private Animator anim;
	private EnemyHashIDs hashIDs;

	protected override void Awake ()
	{
		base.Awake();

		// Setting up the references.
		enemySight = GetComponent<EnemySight>();
		nav = GetComponent<NavMeshAgent>();
		player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		shooting = false;
		waysNetwork = null;
		patrolWayPoints = new Vector3[0];

		// Animations
		hashIDs = GetComponent<EnemyHashIDs>();
		anim = GetComponentInChildren<Animator>();
		anim.SetLayerWeight(0,1f);
	}

	// Use this for initialization
	protected override void Start () 
	{
		// Check attributs droppableItems and itemsDropProbability
		bool validAttributs = true;
		if (droppableItems.Length != itemsDropProbability.Length) // Same size
			validAttributs = false;
		else
			for (int i = 0; i < droppableItems.Length; i++)
				if (droppableItems[i] == null || itemsDropProbability[i] < 0 || itemsDropProbability[i] > 1) // Valid values
					validAttributs = false;
		if (!validAttributs) {
			Debug.LogError("The enemy has its droppable items that are not correctly configured.");
			Application.Quit();
		}
	}
	
	// Update is called once per frame
	protected override void Update () 
	{	
		base.Update();

		// Destroy the ennemy if he is dead
		if (skillManager.getPv() <= 0)
		{
			((SpawnManager)FindObjectOfType(System.Type.GetType("SpawnManager"))).decNbEnnemies();
			player.experienceUpdate(xp);
			dropItems();
			Destroy(gameObject);
			return;
		}

		shooting = false;
		anim.SetBool(hashIDs.isHitting, false);

		// If the player is in sight and is alive...
		if(enemySight.playerInSight && enemySight.getDistanceToPlayer() < shootDistance && player.getSkillManager().getPv() > 0f)
			// ... shoot.
			Shooting();
		
		// If the player has been sighted and isn't dead...
		else if(enemySight.personalLastSighting != Utils.GetInfiniteVector3() && player.getSkillManager().getPv() > 0f)
			// ... chase.
			Chasing();
		
		// Otherwise...
		else
			// ... patrol.
			Patrolling();

		if(nav.velocity.magnitude > 1f)
			anim.SetBool(hashIDs.isWalking, true);
		else
			anim.SetBool(hashIDs.isWalking, false);
	}

	void Shooting ()
	{
		// Stop the enemy where it is.
		nav.Stop();
		shooting = true;

		anim.SetBool(hashIDs.isHitting, true);
	}
	
	
	void Chasing ()
	{
		// Create a vector from the enemy to the last sighting of the player.
		Vector3 sightingDeltaPos = (Vector3)enemySight.personalLastSighting - transform.position;
		// If the the last personal sighting of the player is not close...
		if(sightingDeltaPos.sqrMagnitude > 4f)
			// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
			nav.destination = (Vector3)enemySight.personalLastSighting;
		
		// Set the appropriate speed for the NavMeshAgent.
		nav.speed = chaseSpeed;
		
		// If near the last personal sighting...
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			// Try to find the player in a possible direction
			if (chaseTimer == 0f && enemySight.playerLastDirection != Utils.GetInfiniteVector3())
			{
				nav.SetDestination(nav.destination + enemySight.playerLastDirection);
			}

			// ... increment the timer.
			chaseTimer += Time.deltaTime;

			// If the timer exceeds the wait time...
			if(chaseTimer >= chaseWaitTime)
			{
				// ... reset last global sighting, the last personal sighting and the timer.
				enemySight.personalLastSighting = Utils.GetInfiniteVector3();
				chaseTimer = 0f;
			}
		}
		else
			// If not near the last sighting personal sighting of the player, reset the timer.
			chaseTimer = 0f;
	}
	
	
	void Patrolling ()
	{
		if (patrolWayPoints.Length == 0) {
			generatePatrolWayPoints();
			return;
		}
		
		// Set an appropriate speed for the NavMeshAgent.
		nav.speed = patrolSpeed;
		
		// If near the next waypoint or there is no destination...
		if(nav.destination == Utils.GetInfiniteVector3() || nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(wayPointIndex == patrolWayPoints.Length - 1)
					generatePatrolWayPoints();
				else
					wayPointIndex++;
				
				// Reset the timer.
				patrolTimer = 0;
			}
		}
		else
			// If not near a destination, reset the timer.
			patrolTimer = 0;
		
		// Set the destination to the patrolWayPoint.
		if (wayPointIndex < patrolWayPoints.Length)
			nav.destination = patrolWayPoints[wayPointIndex];
	}
	
	void generatePatrolWayPoints()
	{
		// Get the WaysNetwork where the patrol will occur
		waysNetwork = getNearestWaysNetwork();
		// Determine the start node (the closest node to the transform position).
		Transform from = waysNetwork.getNearestNode(transform.position);
		// Choose a random destination
		Transform to = waysNetwork.getRandomNode();
		if (waysNetwork.getNbNodes() >= 2) {
			while (to == from)
				to = waysNetwork.getRandomNode();
		}
		// Generate path
		patrolWayPoints = waysNetwork.getShortestPath(from.position, to.position);
		wayPointIndex = 0;
	}
	
	WaysNetwork getNearestWaysNetwork()
	{
		WaysNetwork[] waysNetworks = FindObjectsOfType(System.Type.GetType("WaysNetwork")) as WaysNetwork[];
		
		WaysNetwork nearestWaysNetwork = null;
		float minDistance = float.PositiveInfinity;
		for (int i = 0; i < waysNetworks.Length; i++)
		{
			float d = Vector3.Distance(transform.position, waysNetworks[i].transform.position);
			if (d < minDistance) {
				minDistance = d;
				nearestWaysNetwork = waysNetworks[i];
			}
		}
		
		return nearestWaysNetwork;
	}
	
	void dropItems()
	{
		for (int i=0; i < droppableItems.Length; i++)
		{
			if (Random.value < itemsDropProbability[i])
				Instantiate(droppableItems[i], transform.position, Quaternion.identity);
		}
	}

	public bool isShooting()
	{
		return shooting;
	}

	public bool isInCrypt()
	{
		return inCrypte;
	}

	public void setInCrypts(bool b)
	{
		inCrypte = b;	
	}
}