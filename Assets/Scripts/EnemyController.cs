using UnityEngine;
using System.Collections;

public class EnemyController : HumanoidController 
{
	protected PlayerController target;
	protected NavMeshAgent agent;
	protected float timeCountAttack;
	protected int damageAttack = -1;
	protected float timeAttack = 1F;
	protected float probabilityAttack = 0.1F;
	
	// Use this for initialization
	protected virtual void Start () 
	{
		target = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		gameObject.renderer.material.color = new Color(0.725F, 0.478F, 0.341F);
		agent = GetComponent<NavMeshAgent>();
		timeCountAttack = 0;
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
		timeCountAttack += Time.deltaTime;
		if (timeCountAttack >= timeAttack)
		{
			if (Random.value > probabilityAttack)
			target.healthUpdate(damageAttack);
			timeCountAttack = 0;
		}
	}
}