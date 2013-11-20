using UnityEngine;
using System.Collections;

public class EnemyController : HumanoidController 
{
	
	protected PlayerController target;
	protected NavMeshAgent agent;
	protected float timeCountAttack;
	protected int damageAttack;
	protected int damageMagic;
	protected int manaCost;
	protected float timeAttack;
	protected float probabilityAttack;
	protected int xp;
	
	public Rigidbody[] droppableItems;
	public float[] itemsDropProbability;
	
	private bool inCrypte = false;
	
	// Use this for initialization
	protected virtual void Start () 
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
		
		// Init the instance
		target = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		agent = GetComponent<NavMeshAgent>();
		timeCountAttack = 0;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{	
		base.Update();
		if (skillManager.getPv() <= 0)
		{
			((SpawnManager)FindObjectOfType(System.Type.GetType("SpawnManager"))).decNbEnnemies();
			target.experienceUpdate(xp);
			dropItems();
			Destroy(gameObject);
			return;
		}
		
		if(!inCrypte)
		{
//			agent.destination = target.transform.position;
			timeCountAttack += Time.deltaTime;
			Vector3 distance = transform.position-target.transform.position;
		
			if(distance.magnitude <= agent.stoppingDistance)
			{
				transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
//				attack();
			}
		}
//		//les enemis vont vers une direction aleatoire pendent un certain temp et change au boutd'un moment
//		else if((int)Time.timeSinceLevelLoad%10 == 0)
//		{			
//			agent.destination = Random.onUnitSphere*100;
//		}
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
				target.achievementManager.updateTimeNotTouched(0);
			}
			timeCountAttack = 0;
		}
	}
	
	void dropItems()
	{
		for (int i=0; i < droppableItems.Length; i++)
		{
			if (Random.value < itemsDropProbability[i])
				Instantiate(droppableItems[i], transform.position, Quaternion.identity);
		}
	}
	
	public void setInCrypts(bool b)
	{
		inCrypte = b;	
	}
}