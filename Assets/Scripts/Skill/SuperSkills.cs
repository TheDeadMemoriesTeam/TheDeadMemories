using UnityEngine;
using System.Collections;

public class SuperSkills : DamageSkills 
{
	private float m_zone;
	
	//acsessor
	public float getZone()
	{
		return m_zone;	
	}

	// Use this for initialization
	public SuperSkills (string name, string description, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, float zone) 
		:base(name, description, price, father, timeIncantation, manaCost, particule, damage)
	{
		m_zone = zone;
	}

	public void launch(Vector3 position, float distance, float damage, bool furie, float damageFurie, float factorCritique)
	{
		for(int i=0; i<5; i++)
		{
			Transform meteorTransform = (Transform)GameObject.Instantiate(m_particule,
			                                                                  new Vector3(Random.Range(position.x - m_zone, position.x + m_zone),
			            													  35,
			            													  Random.Range(position.z - m_zone, position.z + m_zone)),
			                                                                  Quaternion.identity);
			MeteorController meteor = meteorTransform.GetComponent<MeteorController>() as MeteorController;
			meteor.init(distance + m_zone, damage + m_damage, furie, damageFurie, factorCritique);
		}
	}
}
