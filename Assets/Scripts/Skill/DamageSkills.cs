using UnityEngine;
using System.Collections;

public abstract class DamageSkills : ActiveSkills 
{
	private int m_damage;
	
	//acsessor
	public void setDamage(int damage)
	{
		m_damage = damage;	
	}
	
	public int getDamage()
	{
		return m_damage;	
	}
	
	// Use this for initialization
	protected virtual void Start (string name, int price, Skills father, float timeIncantation, int manaCost, int damage) 
	{
		base.Start(name, price, father, timeIncantation, manaCost);
		m_damage = damage;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
