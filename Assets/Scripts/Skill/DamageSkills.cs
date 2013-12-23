using UnityEngine;
using System.Collections;

public abstract class DamageSkills : ActiveSkills 
{
	protected float m_damage;
	protected float m_baseDamage;
	
	//acsessor
	public void setDamage(float damage)
	{
		m_damage = damage;	
	}
	
	public float getDamage()
	{
		return m_damage;	
	}

	public void setBaseDamage(float baseDamage)
	{
		m_baseDamage = baseDamage;	
	}
	
	public float getBaseDamage()
	{
		return m_baseDamage;	
	}
	
	// Use this for initialization
	public DamageSkills (string name, string description, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float baseDamage) 
		:base(name, description, price, father, timeIncantation, manaCost, particule)
	{
		m_baseDamage = baseDamage;
		m_damage = m_baseDamage;
	}
}
