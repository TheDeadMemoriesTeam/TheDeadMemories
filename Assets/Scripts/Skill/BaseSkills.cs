using UnityEngine;
using System.Collections;

public class BaseSkills : DamageSkills 
{
	private int m_lvlDamage = 0;
	private int m_costIncDamage;
	private string m_nameDamage;

	private int m_lvlAd = 0;
	private int m_costIncAd;
	private string m_nameAd;
	
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

	public string getNameAd()
	{
		return m_nameAd;	
	}

	public string getNameDamage()
	{
		return m_nameDamage;	
	}
	
	// Use this for initialization
	public BaseSkills (string name, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, int costIncDamage, int costIncAd, string nameDamage, string nameAd) 
		:base(name, price, father, timeIncantation, manaCost, particule, damage)
	{
		m_costIncDamage = costIncDamage;
		m_costIncAd = costIncAd;
		m_nameAd = nameAd;
		m_nameDamage = nameDamage;
	}
}
