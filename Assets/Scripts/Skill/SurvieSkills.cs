using UnityEngine;
using System.Collections;

public class SurvieSkills : PassiveSkills 
{
	private int m_pvUp;
	private int m_manaUp;
	
	//acsessor
	public int getPvUp()
	{
		return m_pvUp;	
	}
	
	public int getManaUp()
	{
		return m_manaUp;	
	}
	
	// Use this for initialization
	protected void Start (string name, int price, Skills father, int costIncFirstAd, int costIncSecAd, int pvUp, int manUp) 
	{
		base.Start (name, price, father, costIncFirstAd, costIncSecAd);
		m_pvUp = pvUp;
		m_manaUp = manUp;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
	}
}
