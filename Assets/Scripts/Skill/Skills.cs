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

	// TO REMOVE
	public bool getLvlToUnlock()
	{
		BaseSkills fatherB = m_father as BaseSkills;
		bool b = false;
		if(fatherB != null)
		{
			b = fatherB.getLvlDamage() + fatherB.getLvlAd() >= lvlToUnlock;
		}
		return b;
	}
	public int getLvld()
	{
		BaseSkills fatherB = m_father as BaseSkills;;
		return fatherB.getLvlDamage();
	}

	public int getLvla()
	{
		BaseSkills fatherB = m_father as BaseSkills;;
		return fatherB.getLvlAd();
	}
	public string getifskill()
	{
		PorteeSkills fatherB1 = m_father as PorteeSkills;
		if (fatherB1 != null)
			return "cest un skill portee";
		ZoneSkills fatherB2 = m_father as ZoneSkills;
		if (fatherB2 != null)
			return "cest un skill zone";
		BaseSkills fatherB = m_father as BaseSkills;
		if (fatherB != null)
			return "cest un skill base";
		SuperSkills fatherB3 = m_father as SuperSkills;
		if (fatherB3 != null)
			return "cest un skill super";
		DamageSkills fatherB4 = m_father as DamageSkills;
		if (fatherB4 != null)
			return "cest un skill damage";
		ActiveSkills fatherB5 = m_father as ActiveSkills;
		if (fatherB5 != null)
			return "cest un skill active";
		Skills fatherB6 = m_father as Skills;
		if (fatherB6 != null)
			return "cest un skill skills";

		return "null";
	}
	public Skills getParent()
	{
		return m_father;
	}
	// TO REMOVE

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
