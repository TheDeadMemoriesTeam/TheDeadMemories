using UnityEngine;
using System.Collections;

public abstract class PassiveSkills : Skills 
{
	private int m_lvlFirstAd = 0;
	private int m_costIncFirstAd;
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
	public PassiveSkills (string name, int price, Skills father, int costIncFirstAd, int costIncSecAd) 
		:base (name, price, father)
	{
		m_costIncFirstAd = costIncFirstAd;
		m_costIncSecAd = costIncSecAd;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
