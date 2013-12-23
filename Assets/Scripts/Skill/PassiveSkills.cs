using UnityEngine;
using System.Collections;

public class PassiveSkills : Skills 
{
	private float m_firstAd;
	private string m_nameFirstAd;
	private int m_lvlFirstAd = 0;
	private int m_costIncFirstAd;

	private float m_secAd;
	private string m_nameSecAd;
	private int m_lvlSecAd = 0;
	private int m_costIncSecAd;
	
	//acsessor
	public void setLvlFirstAd(int lvl)
	{
		m_lvlFirstAd = lvl;	
	}

	public int getLvlFirstAd()
	{
		return m_lvlFirstAd;
	}
	
	public void setLvlSecAd(int lvl)
	{
		m_lvlSecAd = lvl;	
	}

	public int getLvlSecAd()
	{
		return m_lvlSecAd;
	}
	
	public void setCostIncFirstAd(int cost)
	{
		m_costIncFirstAd = cost;	
	}
	
	public int getCostIncFirstAd()
	{
		return m_costIncFirstAd;	
	}
	
	public void setCostIncSecAd(int cost)
	{
		m_costIncSecAd = cost;	
	}
	
	public int getCostIncSecAd()
	{
		return m_costIncSecAd;	
	}

	public string getNameFirstAd()
	{
		return m_nameFirstAd + " (" + m_lvlFirstAd + ")";	
	}

	public string getNameSecAd()
	{
		return m_nameSecAd + " (" + m_lvlSecAd + ")";	
	}
	
	// Use this for initialization
	public PassiveSkills (string name, string description, int price, Skills father, int costIncFirstAd, int costIncSecAd, float firstAd, float secAd, string nameFirstAd, string nameSecAd) 
		:base (name, description, price, father)
	{
		m_costIncFirstAd = costIncFirstAd;
		m_costIncSecAd = costIncSecAd;
		m_firstAd = firstAd;
		m_secAd = secAd;
		m_nameFirstAd = nameFirstAd;
		m_nameSecAd = nameSecAd;
	}

	public void update(ref float firstAd, float baseFirstAd, ref float secAd, float basesecAd)
	{
		firstAd = baseFirstAd + m_firstAd * m_lvlFirstAd;
		secAd = basesecAd + m_secAd * m_lvlSecAd;
	}
}
