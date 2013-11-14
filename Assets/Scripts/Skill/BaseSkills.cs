using UnityEngine;
using System.Collections;

public class BaseSkills : DamageSkills 
{
	private int m_lvlDamage = 0;
	private int m_costIncDamage;
	private int m_lvlAd = 0;
	private int m_costIncAd;
	
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
	
	public void setLvlAd(int lvl)
	{
		m_lvlAd = lvl;	
	}
	
	public int getLvlAd()
	{
		return m_lvlAd;	
	}
	
	public void setCostIncAd(int cost)
	{
		m_costIncAd = cost;	
	}
	
	public int getCostIncAd()
	{
		return m_costIncAd;	
	}
	
	// Use this for initialization
	protected virtual void Start (string name, int price, Skills father, float timeIncantation, int manaCost, int damage, int costIncDamage, int costIncAd) 
	{
		base.Start(name, price, father, timeIncantation, manaCost, damage);
		m_costIncDamage = costIncDamage;
		m_costIncAd = costIncAd;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
