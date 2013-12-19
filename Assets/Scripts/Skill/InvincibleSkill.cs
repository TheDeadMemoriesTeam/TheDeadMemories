using UnityEngine;
using System.Collections;

public class InvincibleSkill : ActiveSkills 
{
	private float m_time;
	
	//acsessor	
	public float getTime()
	{
		return m_time;	
	}
	
	
	// Use this for initialization
	public InvincibleSkill (string name, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float time) 
		:base(name, price, father, timeIncantation, manaCost, particule)
	{
		m_time = time;
	}
}
