using UnityEngine;
using System.Collections;

public class PassiveSkills : Skills 
{
	private float m_firstAd;
	private int m_lvlFirstAd = 0;
	private int m_costIncFirstAd;

	private float m_secAd;
	private int m_lvlSecAd = 0;
	private int m_costIncSecAd;
	
	//acsessor
	public void setLvlFirstAd(int lvl)
	{
		m_lvlFirstAd = lvl;	
	}

	public float getLvlFirstAd()
	{
		return m_lvlFirstAd;
	}
	
	public void setLvlSecAd(int lvl)
	{
		m_lvlSecAd = lvl;	
	}

	public float getLvlSecAd()
	{
		return m_lvlSecAd;
	}
	
	public void setCostIncFirstAd(int cost)
	{
		m_costIncFirstAd = cost;	
	}
	
	public int setCostIncFirstAd()
	{
		return m_costIncFirstAd;	
	}
	
	public void setCostIncSecAd(int cost)
	{
		m_costIncSecAd = cost;	
	}
	
	public int setCostIncSecAd()
	{
		return m_costIncSecAd;	
	}
	
	// Use this for initialization
	public PassiveSkills (string name, int price, Skills father, int costIncFirstAd, int costIncSecAd, float firstAd, float secAd) 
		:base (name, price, father)
	{
		m_costIncFirstAd = costIncFirstAd;
		m_costIncSecAd = costIncSecAd;
		m_firstAd = firstAd;
		m_secAd = secAd;
	}

	public void update(ref float firstAd, float baseFirstAd, ref float secAd, float basesecAd)
	{
		firstAd = baseFirstAd + m_firstAd * m_lvlFirstAd;
		secAd = basesecAd + m_secAd * m_lvlSecAd;
	}
}
