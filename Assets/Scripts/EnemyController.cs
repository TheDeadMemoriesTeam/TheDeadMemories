using UnityEngine;
using System.Collections;

public class EnemyController : HumanoidController 
{
	protected PlayerController target;
	protected NavMeshAgent agent;
	protected float time_count_Attack;
	protected const float time_Attack = 1F;
	
	// Use this for initialization
	protected virtual void Start () 
	{
		target = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		gameObject.renderer.material.color = new Color(0.725F, 0.478F, 0.341F);
		agent = GetComponent<NavMeshAgent>();
		time_count_Attack = 0;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{	
		agent.destination = target.transform.position;
		Vector3 distance = transform.position-target.transform.position;
		if(distance.magnitude <= agent.stoppingDistance)
		{
			attack();
		}
	}
	
	void attack()
	{
		//code a realiser pour toi janisse ;) 
		time_count_Attack += Time.deltaTime;
		if (time_count_Attack >= time_Attack)
		{
			target.healthUpdate(-1);
			time_count_Attack = 0;
		}
	}
}