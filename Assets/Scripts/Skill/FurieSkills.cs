using UnityEngine;
using System.Collections;

public class FurieSkills : ActiveSkills 
{
	private float m_time;
	private float m_damageFactor;
	
	//acsessor
	public float getTime()
	{
		return m_time;	
	}
	
	public float getDamageFactor()
	{
		return m_damageFactor;	
	}
	
	// Use this for initialization
	public FurieSkills (string name, string description, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float time, float damageFactor) 
		:base(name, description, price, father, timeIncantation, manaCost, particule)
	{
		m_time = time;
		m_damageFactor = damageFactor;
	}
}
