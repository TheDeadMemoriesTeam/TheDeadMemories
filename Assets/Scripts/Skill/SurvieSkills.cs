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
	public SurvieSkills (string name, int price, Skills father, int costIncFirstAd, int costIncSecAd, int pvUp, int manUp) 
		:base(name, price, father, costIncFirstAd, costIncSecAd)
	{
		m_pvUp = pvUp;
		m_manaUp = manUp;
	}

	public void update(ref int manaMax, int baseManaMax, ref int pvMax, int basePvMax)
	{
		pvMax = basePvMax + m_pvUp * getLvlFirstAd();
		manaMax = baseManaMax + m_manaUp * getLvlSecAd();
	}
}
