using UnityEngine;
using System.Collections;

public abstract class Skills 
{
	private string m_name;
	private int m_price;
	private bool m_isBought = false;
	private bool m_isUnlock = false;
	
	private Skills m_father;
	
	//acsessor
	public string getName()
	{
		return m_name;	
	}
	
	public int getPrice()
	{
		return m_price;	
	}
	
	public void setIsBought(bool isBought)
	{
		m_isBought = isBought;	
	}
	
	public bool getIsBought()
	{
		return m_isBought;	
	}
	
	public void setIsUnlock(bool isUnlock)
	{
		m_isUnlock = isUnlock;	
	}
	
	public bool getIsUnlock()
	{
		return m_isUnlock;	
	}
	
	public Skills getFather()
	{
		return m_father;
	}
	
	// Use this for initialization
	protected virtual void Start (string name, int price, Skills father) 
	{
		m_name = name;	
		m_price = price;
		m_father = father;
		
		if(father == null)
			setIsUnlock(true);
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if(!m_isUnlock)
		{
			PassiveSkills fatherP = m_father as  PassiveSkills;
			if(fatherP != null)
			{
				if(fatherP.getLvlFirstAd() + fatherP.getLvlSecAd() > 5)
					setIsUnlock(true);
			}
			
			BaseSkills fatherB = m_father as BaseSkills;
			if(fatherB != null)
			{
				if(fatherB.getLvlDamage() + fatherB.getLvlAd() > 5)
					setIsUnlock(true);
			}
 		}
	}
}
