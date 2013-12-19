using UnityEngine;
using System.Collections;

public abstract class DamageSkills : ActiveSkills 
{
	private float m_damage;
	
	//acsessor
	public void setDamage(float damage)
	{
		m_damage = damage;	
	}
	
	public float getDamage()
	{
		return m_damage;	
	}
	
	// Use this for initialization
	public DamageSkills (string name, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage) 
		:base(name, price, father, timeIncantation, manaCost, particule)
	{
		m_damage = damage;
	}
}
