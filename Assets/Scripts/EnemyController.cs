using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	public PlayerController target;
	private Vector3 distance;
	NavMeshAgent agent;
	
	// Use this for initialization
	void Start () 
	{
		target = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		gameObject.renderer.material.color = new Color(0.725F, 0.478F, 0.341F);
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		agent.destination = target.transform.position;
		distance = transform.position-target.transform.position;
		if(distance.magnitude <= agent.stoppingDistance)
		{
			target.healthUpdate(-1);
		}
	}
}