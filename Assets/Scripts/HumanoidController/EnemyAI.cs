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
    
    
    void Awake ()
    {
        // Setting up the references.
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
    }
    
    
    void Update ()
    {
        // If the player has been sighted and isn't dead...
		if(enemySight.personalLastSighting != Utils.GetInfiniteVector3() && player.getHitPoints() > 0f)
            // ... chase.
            Chasing();

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

}