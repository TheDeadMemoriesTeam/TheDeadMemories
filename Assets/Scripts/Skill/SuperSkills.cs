using UnityEngine;
using System.Collections;

public class SuperSkills : DamageSkills 
{
	private float m_portee;
	private float m_zone;
	
	//acsessor
	public float getPortee()
	{
		return m_portee;	
	}
	
	public float getZone()
	{
		return m_zone;	
	}

	// Use this for initialization
	public SuperSkills (string name, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, float portee, float zone) 
		:base(name, price, father, timeIncantation, manaCost, particule, damage)
	{
		m_portee = portee;
		m_zone = zone;
	}
}
