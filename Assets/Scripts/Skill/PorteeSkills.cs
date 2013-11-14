using UnityEngine;
using System.Collections;

public class PorteeSkills : BaseSkills 
{
	private float m_portee;
	private int m_lvlPortee = 0;
	private int m_costIncPortee;
	
	//acsessor
	public float getPortee()
	{
		return m_portee;	
	}
	
	public void setLvlPortee(int lvl)
	{
		m_lvlPortee = lvl;	
	}
	
	public float getLvlPortee()
	{
		return m_lvlPortee;	
	}
	
	public void setCostIncPortee(int cost)
	{
		m_costIncPortee = cost;	
	}
	
	public float getCostIncPortee()
	{
		return m_costIncPortee;	
	}
	
	// Use this for initialization
	protected void Start (string name, int price, Skills father, float timeIncantation, int manaCost, int damage, int costIncDamage, float portee, int costIncPortee) 
	{
		base.Start(name, price, father, timeIncantation, manaCost, damage, costIncDamage);
		m_portee = portee;
		m_costIncPortee = costIncPortee;
		
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
