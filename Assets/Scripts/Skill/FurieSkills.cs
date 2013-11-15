using UnityEngine;
using System.Collections;

public class FurieSkills : ActiveSkills 
{
	private float m_time;
	private float m_damageFactor;
	
	//acsessor
	public float getTime()
	{
		return m_time;	
	}
	
	public float getDamageFactor()
	{
		return m_damageFactor;	
	}
	
	// Use this for initialization
	public FurieSkills (string name, int price, Skills father, float timeIncantation, int manaCost, float time, float damageFactor) 
		:base(name, price, father, timeIncantation, manaCost)
	{
		m_time = time;
		m_damageFactor = damageFactor;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
