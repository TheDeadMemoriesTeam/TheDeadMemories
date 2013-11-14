using UnityEngine;
using System.Collections;

public class BaseSkills : DamageSkills 
{
	private int m_lvlDamage = 0;
	private int m_costIncDamage;
	
	//acsessor
	public void setLvlDamage(int lvl)
	{
		m_lvlDamage = lvl;	
	}
	
	public int getLvlDamage()
	{
		return m_lvlDamage;	
	}
	
	public void setCostIncDamage(int cost)
	{
		m_costIncDamage = cost;	
	}
	
	public int getCostIncDamage()
	{
		return m_costIncDamage;	
	}
	
	// Use this for initialization
	protected virtual void Start (string name, int price, Skills father, float timeIncantation, int manaCost, int damage, int costIncDamage) 
	{
		base.Start(name, price, father, timeIncantation, manaCost, damage);
		m_costIncDamage = costIncDamage;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
