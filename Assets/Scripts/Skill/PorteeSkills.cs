using UnityEngine;
using System.Collections;

public class PorteeSkills : BaseSkills 
{
	private float m_portee;
	
	//acsessor
	public float getPortee()
	{
		return m_portee;	
	}
	
	// Use this for initialization
	protected void Start (string name, int price, Skills father, float timeIncantation, int manaCost, int damage, int costIncDamage, int costIncPortee, float portee) 
	{
		base.Start(name, price, father, timeIncantation, manaCost, damage, costIncDamage, costIncPortee);
		m_portee = portee;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
