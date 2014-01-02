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

	public void launch(Vector3 position)
	{
		for(int i=0; i<5; i++)
			GameObject.Instantiate(m_particule, new Vector3(Random.Range(position.x - m_zone, position.x + m_zone), 35, Random.Range(position.z - m_zone, position.z + m_zone)), Quaternion.identity);
	}
}
