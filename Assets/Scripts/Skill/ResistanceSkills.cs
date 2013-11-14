using UnityEngine;
using System.Collections;

public class ResistanceSkills : PassiveSkills 
{
	private int m_resistanceMagic;
	private int m_resistancePhysic;
	
	//acsessor
	public int getResistanceMagic()
	{
		return m_resistanceMagic;	
	}
	
	public int getResistancePhysic()
	{
		return m_resistancePhysic;	
	}
	
	// Use this for initialization
	protected void Start (string name, int price, Skills father, int costIncFirstAd, int costIncSecAd, int resistanceMagic, int resistancePhysic) 
	{
		base.Start (name, price, father, costIncFirstAd, costIncSecAd);
		m_resistanceMagic = resistanceMagic;
		m_resistancePhysic = resistancePhysic;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
