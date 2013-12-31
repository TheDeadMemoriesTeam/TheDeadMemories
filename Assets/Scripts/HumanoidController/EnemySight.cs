using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 120f;           // Number of degrees, centred on forward, for the enemy see.
	public float viewingDistance = 24f;             // Distance from where the player will be viewed
    public bool playerInSight;                      // Whether or not the player is currently sighted.
    public Vector3 personalLastSighting;            // Last place this enemy spotted the player.
	public float hearingDistance = 8f;              // Distance from where the player will be heard
	public Vector3 playerLastDirection;             // Last direction of the player
    
    private NavMeshAgent nav;                       // Reference to the NavMeshAgent component.
	private Animator anim;							// Reference to the Animator.
    private PlayerController player;                // Reference to the player.
    private Animator playerAnim;                    // Reference to the player's animator component.
	private PlayerHashIDs playerHashIds;
	
	private float alertDistance = 25f;              // Distance from where ennemies can be alerted
    
    void Awake ()
    {
        // Setting up the references.
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
        // Set the personal sighting and the previous sighting to the reset position.
        personalLastSighting = Utils.GetInfiniteVector3();

		playerLastDirection = Utils.GetInfiniteVector3();
    }
	
	void Start()
	{
		player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		playerAnim = player.GetComponentInChildren<Animator>();
		playerHashIds = player.GetComponent<PlayerHashIDs>();
	}
    
    
    void Update ()
    {
		if (personalLastSighting != Utils.GetInfiniteVector3())
			playerLastDirection = player.transform.position - personalLastSighting;
		else
			playerLastDirection = Utils.GetInfiniteVector3();

		playerInSight = false;
		if (getDistanceToPlayer() < viewingDistance)
			updateSighting();
    }
    

	private void updateSighting()
    {
        // By default the player is not in sight.
        playerInSight = false;
        
		// If the ennemi was not alerted and if the player is not stopped
		// This is to decrease the performance impact of the raycasting
		if (personalLastSighting != player.transform.position)
		{
	        // Create a vector from the enemy to the player and store the angle between it and forward.
	        Vector3 direction = player.transform.position - transform.position;
			direction.y = 0;
	        float angle = Vector3.Angle(direction, transform.forward);
	        
	        // If the angle between forward and where the player is, is less than half the angle of view...
	        if(angle < fieldOfViewAngle * 0.5f)
	        {
	            RaycastHit hit;
	            
	            // ... and if a raycast towards the player hits something...
				Debug.DrawRay(transform.position, direction.normalized * viewingDistance, new Color(1f, 0, 0, 1f));
	            if(Physics.Raycast(transform.position, direction.normalized, out hit, viewingDistance))
	            {
	                // ... and if the raycast hits the player...
	                if(hit.collider.gameObject.tag == "Player")
	                {
	                    // ... the player is in sight.
	                    playerInSight = true;
	                    
	                    // Set the players current position.
						Debug.Log ("sight");
						playerPosition(player.transform.position);
	                }
	            }
			}

        }
		else {
			playerInSight = true;
		}
        

        // If the player is walking/running...
		int walking = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
		if(walking == playerHashIds.movingState ||
		   walking == playerHashIds.beginMovingState ||
		   walking == playerHashIds.beginMovingState)
        {
            // ... and if the player is within hearing range...
            if(CalculatePathLength(player.transform.position) <= hearingDistance) {
                // ... set the last personal sighting of the player to the player's current position.
				Debug.Log("heared");
            	playerPosition(player.transform.position);
			}
        }
    }
    
    
    private float CalculatePathLength (Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if(nav.enabled)
            nav.CalculatePath(targetPosition, path);
        
        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        
        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;
        
        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;
        
        // The points inbetween are the corners of the path.
        for(int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }
        
        // Create a float to store the path length that is by default 0.
        float pathLength = 0;
        
        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for(int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }
        
        return pathLength;
    }
	
	public float getDistanceToPlayer()
	{
		return Vector3.Distance(transform.position, player.transform.position);
	}
	
	void alertOthers(Vector3 pos, int broadcastingLevel)
	{
		const int maxBroadcastingLevel = 2;
		if ( broadcastingLevel < maxBroadcastingLevel)
		{
			// if (maxBroadcastingLevel == 0)
			//  	audio.PlayOneShot(alertSound);
			
			EnemySight[] ennemies = FindObjectsOfType(System.Type.GetType("EnemySight")) as EnemySight[];
			for (int i = 0; i < ennemies.Length; i++)
			{
				Vector3 vect = ennemies[i].transform.position - transform.position;
				if (vect.magnitude < alertDistance) {
					ennemies[i].playerPosition(pos, broadcastingLevel);
				}
			}
		}
		
	}

	void playerPosition(Vector3 pos)
	{
		playerPosition(pos, 0);
	}

	void playerPosition(Vector3 pos, int broadcastingLevel)
	{
		if (personalLastSighting != pos)
		{
			if (broadcastingLevel != 0)
				Debug.Log("alerted");
			personalLastSighting = pos;
			alertOthers(pos, ++broadcastingLevel);
		}
	}
}