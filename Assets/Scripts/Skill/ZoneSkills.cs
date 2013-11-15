using UnityEngine;
using System.Collections;

public class ZoneSkills : BaseSkills
{
	private float m_zone;
	
	//acsessor
	public float getZone()
	{
		return m_zone;	
	}
	
	// Use this for initialization
	public ZoneSkills (string name, int price, Skills father, float timeIncantation, int manaCost, int damage, int costIncDamage, int costIncZone, float zone) 
		:base(name, price, father, timeIncantation, manaCost, damage, costIncDamage, costIncZone)
	{
		m_zone = zone;
		
	}
}
