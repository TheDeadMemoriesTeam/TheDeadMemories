using UnityEngine;
using System.Collections;

public class BaseSkills : DamageSkills 
{
	private int m_lvlDamage = 0;
	private int m_costIncDamage;
	private string m_nameDamage;

	private float m_baseAd;
	protected float m_ad;
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

	public float getAd()
	{
		return m_ad;	
	}
	
	public float getBaseAd()
	{
		return m_baseAd;	
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
		return m_nameAd + " (" + m_lvlAd + ")";	
	}

	public string getNameDamage()
	{
		return m_nameDamage + " (" + m_lvlDamage + ")";	
	}
	
	// Use this for initialization
	public BaseSkills (string name, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, int costIncDamage, int costIncAd, string nameDamage, string nameAd, float baseAd) 
		:base(name, price, father, timeIncantation, manaCost, particule, damage)
	{
		m_costIncDamage = costIncDamage;

		m_baseAd = baseAd;
		m_ad = m_baseAd;
		m_costIncAd = costIncAd;
		m_nameAd = nameAd;
		m_nameDamage = nameDamage;
	}

	public void update()
	{
		m_damage = m_baseDamage * m_lvlDamage;
		m_ad = m_baseAd * m_lvlAd;
	}
}