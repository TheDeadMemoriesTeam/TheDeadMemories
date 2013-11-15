using UnityEngine;
using System.Collections;

public abstract class ActiveSkills : Skills 
{
	private float m_timeIncantation;
	private int m_manaCost;
	
	//acsessor	
	public float getIncantation()
	{
		return m_timeIncantation;	
	}
	
	public int getManaCost()
	{
		return m_manaCost;	
	}
	
	// Use this for initialization
	public ActiveSkills (string name, int price, Skills father, float timeIncantation, int manaCost) 
		:base(name, price, father)
	{
		m_manaCost = manaCost;
		m_timeIncantation = timeIncantation;
		
	}
}
