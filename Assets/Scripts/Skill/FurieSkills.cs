using UnityEngine;
using System.Collections;

public class FurieSkills : DamageSkills 
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
	protected void Start (string name, int price, Skills father, float timeIncantation, int manaCost, int damage, float time, float damageFactor) 
	{
		base.Start(name, price, father, timeIncantation, manaCost, damage);
		m_time = time;
		m_damageFactor = damageFactor;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
