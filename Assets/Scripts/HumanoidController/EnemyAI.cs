using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float shootDistance = 4f;            // Distance from where the player will be shot
    public float chaseSpeed = 5f;               // The nav mesh agent's speed when chasing.
    public float chaseWaitTime = 5f;            // The amount of time to wait when the last sighting is reached.
	public float patrolSpeed = 2f;              // The nav mesh agent's speed when patrolling.
    public float patrolWaitTime = 1f;           // The amount of time to wait when the patrol way point is reached.
    public Transform[] patrolWayPoints;         // An array of transforms for the patrol route.
    
    
    private EnemySight enemySight;              // Reference to the EnemySight script.
    private NavMeshAgent nav;                   // Reference to the nav mesh agent.
    private PlayerController player;            // Reference to the player.
    private float chaseTimer;                   // A timer for the chaseWaitTime.
    private float patrolTimer;                  // A timer for the patrolWaitTime.
    private int wayPointIndex = 0;              // A counter for the way point array.
    private Transform waysNetwork;				// Object that will store all way points.
    
    void Awake ()
    {
        // Setting up the references.
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		waysNetwork = null;
    }
    
    
    void Update ()
    {
        // If the player is in sight and is alive...
        if(enemySight.playerInSight && enemySight.getDistanceToPlayer() < shootDistance && player.getHitPoints() > 0f)
            // ... shoot.
            Shooting();
        
        // If the player has been sighted and isn't dead...
        else if(enemySight.personalLastSighting != Utils.GetInfiniteVector3() && player.getHitPoints() > 0f)
            // ... chase.
            Chasing();
        
        // Otherwise...
        else
            // ... patrol.
            Patrolling();
    }
    
    
    void Shooting ()
    {
        // Stop the enemy where it is.
        nav.Stop();
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
                    wayPointIndex = 0;
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
        nav.destination = patrolWayPoints[wayPointIndex].position;
    }
	
	void generatePatrolWayPoints()
	{
		waysNetwork = getNearestWaysNetwork();
		patrolWayPoints = waysNetwork.gameObject.GetComponentsInChildren<Transform>();
		wayPointIndex = 0;
	}
	
	Transform getNearestWaysNetwork()
	{
		GameObject[] waysNetworks = GameObject.FindGameObjectsWithTag("WaysNetwork");
		
		Transform nearestWaysNetwork = null;
		float minDistance = float.PositiveInfinity;
		for (int i = 0; i < waysNetworks.Length; i++)
		{
			float d = Vector3.Distance(transform.position, waysNetworks[i].transform.position);
			if (d < minDistance) {
				minDistance = d;
				nearestWaysNetwork = waysNetworks[i].transform;
			}
		}
		
		return nearestWaysNetwork;
	}
}