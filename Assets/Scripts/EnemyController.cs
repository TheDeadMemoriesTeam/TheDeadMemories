using UnityEngine;
using System.Collections;

public class EnemyController : HumanoidController 
{
	
	protected PlayerController target;
	protected NavMeshAgent agent;
	protected float timeCountAttack;
	protected int damageAttack = -1;
	protected int damageMagic = -5;
	protected int manaCost = -50;
	protected float timeAttack = 1F;
	protected float probabilityAttack = 0.1F;
	protected int xp;
	
	public Rigidbody medikit;
	protected float dropProbability = 0.077F;
	
	// Use this for initialization
	protected virtual void Start () 
	{
		//medikit = (Rotator)FindObjectOfType(System.Type.GetType("Rotator"));
		target = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		gameObject.renderer.material.color = new Color(0.725F, 0.478F, 0.341F);
		agent = GetComponent<NavMeshAgent>();
		timeCountAttack = 0;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{	
		if (pv <= 0)
		{
			((SpawnManager)FindObjectOfType(System.Type.GetType("SpawnManager"))).decNbEnnemies();
			target.experienceUpdate(xp);
			
			// Lache un medikit à la position de l'ennemi mort
			if (Random.value < dropProbability)
			{
				Instantiate(medikit, transform.position, Random.rotation);
			}
			DestroyImmediate(gameObject);
			return;
		}
		timeCountAttack += Time.deltaTime;
		agent.destination = target.transform.position;
		Vector3 distance = transform.position-target.transform.position;
		if(distance.magnitude <= agent.stoppingDistance)
		{
			attack();
		}
	}
	
	void attack()
	{
		if (timeCountAttack >= timeAttack)
		{
			if (Random.value > probabilityAttack)
			{
				if (getMana() >= -manaCost)
				{
					if (Random.value > 0.5)
						target.healthUpdate(damageAttack);
					else
					{
						target.healthUpdate(damageMagic);
						manaUpdate(manaCost);
					}					
				}
				else
					target.healthUpdate(damageAttack);
				target.setTimeNotTouched(0);
			}
			timeCountAttack = 0;
		}
	}
}