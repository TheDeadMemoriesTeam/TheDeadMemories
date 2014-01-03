using UnityEngine;
using System.Collections;

public abstract class ActiveSkills : Skills 
{
	private float m_timeIncantation;
	private int m_manaCost;
	protected Transform m_particule;
	
	//acsessor	
	public float getTimeIncantation()
	{
		return m_timeIncantation;	
	}
	
	public int getManaCost()
	{
		return m_manaCost;	
	}

	// Use this for initialization
	public ActiveSkills (string name, string description, int price, Skills father, float timeIncantation, int manaCost, Transform particule) 
		:base(name, description, price, father)
	{
		m_manaCost = manaCost;
		m_timeIncantation = timeIncantation;
		m_particule = particule;
		
	}
}
