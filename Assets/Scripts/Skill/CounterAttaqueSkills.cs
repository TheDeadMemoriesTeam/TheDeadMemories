using UnityEngine;
using System.Collections;

public class CounterAttaqueSkills : PassiveSkills 
{
	private int m_damageCounterAttaque;
	private float m_counterAttaqueFactor;
	
	//acsessor
	public int getDamageCounterAttaque()
	{
		return m_damageCounterAttaque;	
	}
	
	public float getCounterAttaqueFactor()
	{
		return m_counterAttaqueFactor;	
	}
	
	// Use this for initialization
	protected void Start (string name, int price, Skills father, int costIncFirstAd, int costIncSecAd, int damageCounterAttaque, float counterAttaqueFactor) 
	{
		base.Start (name, price, father, costIncFirstAd, costIncSecAd);
		m_damageCounterAttaque = damageCounterAttaque;
		m_counterAttaqueFactor = counterAttaqueFactor;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
