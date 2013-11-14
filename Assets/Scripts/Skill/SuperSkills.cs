using UnityEngine;
using System.Collections;

public class SuperSkills : DamageSkills 
{
	private float m_portee;
	private float m_zone;
	
	//acsessor
	public float getPortee()
	{
		return m_portee;	
	}
	
	public float getZone()
	{
		return m_zone;	
	}

	// Use this for initialization
	protected void Start (string name, int price, Skills father, float timeIncantation, int manaCost, int damage, float portee, float zone) 
	{
		base.Start(name, price, father, timeIncantation, manaCost, damage);
		m_portee = portee;
		m_zone = zone;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
