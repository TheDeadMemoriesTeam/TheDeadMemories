using UnityEngine;
using System.Collections;

public class BaseSkills : DamageSkills 
{
	private int m_lvlDamage = 0;
	private int m_costIncDamage;
	private string m_nameDamage;
	private string m_descriptionDamage;

	private float m_baseAd;
	protected float m_ad;
	private int m_lvlAd = 0;
	private int m_costIncAd;
	private string m_nameAd;
	private string m_descriptionAd;


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
		return (int)(m_costIncDamage + Mathf.Pow((m_lvlDamage/2),3)*5 + (Mathf.Cos(m_lvlDamage)*1.5*Mathf.Pow(m_lvlDamage,2)));	
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
		return (int)(m_costIncAd + Mathf.Pow((m_lvlAd/2),3)*5 + (Mathf.Cos(m_lvlAd)*1.5*Mathf.Pow(m_lvlAd,2)));		
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

	public string getDescriptionAd()
	{
		return m_descriptionAd;	
	}

	public string getNameDamage()
	{
		return m_nameDamage + " (" + m_lvlDamage + ")";	
	}

	public string getDescriptionDamage()
	{
		return m_descriptionDamage;	
	}
	
	// Use this for initialization
	public BaseSkills (string name, string description, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, int costIncDamage, int costIncAd, string nameDamage, string nameAd, string descriptionDamage, string descriptionAd, float baseAd) 
		:base(name, description, price, father, timeIncantation, manaCost, particule, damage)
	{
		m_costIncDamage = costIncDamage;

		m_baseAd = baseAd;
		m_ad = m_baseAd;
		m_costIncAd = costIncAd;
		m_nameAd = nameAd;
		m_nameDamage = nameDamage;
		m_descriptionAd = descriptionAd;
		m_descriptionDamage = descriptionDamage;
	}

	public void update()
	{
		m_damage = m_baseDamage * m_lvlDamage;
		m_ad = m_baseAd * m_lvlAd;
	}
}