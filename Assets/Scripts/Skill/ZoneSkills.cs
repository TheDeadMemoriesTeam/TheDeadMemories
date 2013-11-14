using UnityEngine;
using System.Collections;

public class ZoneSkills : BaseSkills
{
	private float m_zone;
	private int m_lvlZone = 0;
	private int m_costIncZone;
	
	//acsessor
	public float getZone()
	{
		return m_zone;	
	}
	
	public void setLvlZone(int lvl)
	{
		m_lvlZone = lvl;	
	}
	
	public float getLvlZone()
	{
		return m_lvlZone;	
	}
	
	public void setCostIncZone(int cost)
	{
		m_costIncZone = cost;	
	}
	
	public float getCostIncZone()
	{
		return m_costIncZone;	
	}
	
	// Use this for initialization
	protected void Start (string name, int price, Skills father, float timeIncantation, int manaCost, int damage, int costIncDamage, float zone, int costIncZone) 
	{
		base.Start(name, price, father, timeIncantation, manaCost, damage, costIncDamage);
		m_zone = zone;
		m_costIncZone = costIncZone;
		
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
