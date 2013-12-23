using UnityEngine;
using System.Collections;

public abstract class Skills 
{
	private string m_name;
	private int m_price;
	private bool m_isBought = false;
	private bool m_isUnlock = false;
	
	private int lvlToUnlock = 10;
	
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

	// Use this for initialization
	public Skills (string name, int price, Skills father) 
	{
		m_name = name;	
		m_price = price;
		m_father = father;
		
		if(father == null)
			setIsUnlock(true);
	}
	
	public void unlockedSkill () 
	{
		if(!m_isUnlock)
		{
			PassiveSkills fatherP = m_father as  PassiveSkills;
			if(fatherP != null)
			{
				if(fatherP.getLvlFirstAd() + fatherP.getLvlSecAd() >= lvlToUnlock)
					setIsUnlock(true);
			}
			
			BaseSkills fatherB = m_father as BaseSkills;
			if(fatherB != null)
			{
				if(fatherB.getLvlDamage() + fatherB.getLvlAd() >= lvlToUnlock)
					setIsUnlock(true);
			}
 		}
	}
}
