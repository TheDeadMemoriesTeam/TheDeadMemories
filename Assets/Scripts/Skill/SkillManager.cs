using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour 
{
	//list des competences
	private List<Skills> listSkills;

	//variable afectant le joueur
	//mana
	private float m_baseManaMax, m_manaMax, m_mana;

	//pv
	private float m_basePvMax, m_pvMax, m_pv;

	//resistance physic
	private float m_basePhysicalResistance, m_physicalResistance;

	//resistance magic
	private float m_baseMagicResistance, m_magicResistance;

	//attaque physic
	private float m_basePhysicAttack, m_physicAttack;

	//attaque magic
	private float m_baseMagicAttack, m_magicAttack;

	//critique physic
	private float m_baseCriticPhysic, m_criticPhysic;

	//critique magic
	private float m_baseCriticMagic, m_criticMagic;

	//distance d'attaque
	private float m_distancePhysicAttack, m_distanceMagicAttack;

	//skills spetiales
	private bool m_invincible = false;
	private float m_invincibleTime = 0f;
	private bool m_furie = false;
	private float m_furieTime = 0f;
	private float m_skillDelay = 10f;
	
	//acesseur
	//mana
	public void setBaseManaMax(float baseManaMax)
	{
		m_baseManaMax = baseManaMax;
		m_manaMax = m_baseManaMax;
		m_mana = m_manaMax;
	}

	public void setManaMax(float manaMax)
	{
		m_manaMax = manaMax;
	}
	
	public float getManaMax()
	{
		return m_manaMax;	
	}

	public void setMana(float mana)
	{
		m_mana = mana;
	}
	
	public float getMana()
	{
		return m_mana;	
	}

	//pv
	public void setBasePvMax(float basePvMax)
	{
		m_basePvMax = basePvMax;
		m_pvMax = m_basePvMax;
		m_pv = m_pvMax;
	}

	public void setPvMax(float pvMax)
	{
		m_pvMax = pvMax;
	}
	
	public float getPvMax()
	{
		return m_pvMax;	
	}

	public void setPv(float pv)
	{
		m_pv = pv;
	}
	
	public float getPv()
	{
		return m_pv;	
	}

	//resistance physic
	public void setBasePhysicalResistance(float basePhysicalResistance)
	{
		m_basePhysicalResistance = basePhysicalResistance;
		m_physicalResistance = m_basePhysicalResistance;
	}
	
	public void setPhysicalResistance(float physicalResistance)
	{
		m_physicalResistance = physicalResistance;
	}
	
	public float getPhysicalResistance()
	{
		return m_physicalResistance;	
	}

	//resistance magic
	public void setBaseMagicResistance(float baseMagicResistance)
	{
		m_baseMagicResistance = baseMagicResistance;
		m_magicResistance = m_baseMagicResistance;
	}
	
	public void setMagicResistance(float magicResistance)
	{
		m_magicResistance = magicResistance;
	}
	
	public float getMagicResistance()
	{
		return m_magicResistance;	
	}

	//Attack physic
	public void setBasePhysicAttack(float basePhysicAttack)
	{
		m_basePhysicAttack = basePhysicAttack;
		m_physicAttack = m_basePhysicAttack;
	}
	
	public void setPhysicAttack(float physicAttack)
	{
		m_physicAttack = physicAttack;
	}
	
	public float getPhysicAttack()
	{
		return m_physicAttack;	
	}

	//Attack magic
	public void setBaseMagicAttack(float baseMagicAttack)
	{
		m_baseMagicAttack = baseMagicAttack;
		m_magicAttack = m_baseMagicAttack;
	}
	
	public void setMagicAttack(float magicAttack)
	{
		m_magicAttack = magicAttack;
	}
	
	public float getMagicAttack()
	{
		return m_magicAttack;	
	}

	//Critic physic
	public void setBaseCriticPhysic(float baseCriticPhysic)
	{
		m_baseCriticPhysic = baseCriticPhysic;
		m_criticPhysic = m_baseCriticPhysic;
	}
	
	public void setCriticPhysic(float criticPhysic)
	{
		m_criticPhysic = criticPhysic;
	}
	
	public float getCriticPhysic()
	{
		return m_criticPhysic;	
	}

	//Critic magic
	public void setBaseCriticMagic(float baseCriticMagic)
	{
		m_baseCriticMagic = baseCriticMagic;
		m_criticMagic = m_baseCriticMagic;
	}
	
	public void setCriticMagic(float criticMagic)
	{
		m_criticMagic = criticMagic;
	}
	
	public float getCriticMagic()
	{
		return m_criticMagic;	
	}

	//distance d'attaque
	public void setDistanceMagicAttack(float distanceMagicAttack)
	{
		m_distanceMagicAttack = distanceMagicAttack;
	}
	
	public float getDistanceMagicAttack()
	{
		return m_distanceMagicAttack;	
	}
	
	public void setDistancePhysicAttack(float distancePhysicAttack)
	{
		m_distancePhysicAttack = distancePhysicAttack;
	}
	
	public float getDistancePhysicAttack()
	{
		return m_distancePhysicAttack;	
	}

	//skills spetiales
	public void setInvincible(bool invincible)
	{
		m_invincible = invincible;
	}

	public bool getInvincible()
	{
		return m_invincible;
	}

	public bool getInvincibleTime()
	{
		return m_invincibleTime == 0f;
	}

	public void setFurie(bool furie)
	{
		m_furie = furie;
	}

	public bool getFurie()
	{
		return m_furie;
	}

	public bool getFurieTime()
	{
		return m_furieTime == 0f;
	}
	
	//constructor
	public SkillManager()
	{
		//creation de la liste
		listSkills = new List<Skills>();		
	}
	
	// Use this for initialization
	void Start () 
	{
	}

	public void updateSpetialSkill()
	{
		InvincibleSkill skillInv = listSkills[2] as InvincibleSkill;
		if(skillInv.getIsBought())
		{
			if(m_invincible && m_invincibleTime == 0f)
				m_invincibleTime = Time.time;
			else if(Time.time - m_invincibleTime >= m_skillDelay)
				m_invincibleTime = 0f;
			else if(Time.time - m_invincibleTime >= skillInv.getTime())
				m_invincible = false;
			 
		}

		FurieSkills skillFurie = listSkills[5] as FurieSkills;
		if(skillFurie.getIsBought())
		{
			if(m_furie && m_furieTime == 0f)
				m_furieTime = Time.time;
			else if(Time.time - m_furieTime >= m_skillDelay)
				m_furieTime = 0f;
			else if(Time.time - m_furieTime >= skillFurie.getTime())
				m_furie = false;
		}
	}

	//fonction pour l'update des skills
	public void updateSkill () 
	{
		for (int i=0; i<listSkills.Count; i++)
		{
			if(listSkills[i].GetType() == System.Type.GetType("PassiveSkills"))
			{
				PassiveSkills tmp = listSkills[i] as PassiveSkills;
				if(tmp.getName() == "Survie")
					tmp.update(ref m_pvMax, m_basePvMax, ref m_manaMax, m_baseManaMax);
				else if(tmp.getName() == "Résistance")
					tmp.update(ref m_physicalResistance, m_basePhysicalResistance, ref m_magicResistance, m_baseMagicResistance);
				else if(tmp.getName() == "Attaque de base")
					tmp.update(ref m_physicAttack, m_basePhysicAttack, ref m_magicAttack, m_baseMagicAttack);
				else if(tmp.getName() == "Critique")
					tmp.update(ref m_criticPhysic, m_baseCriticPhysic, ref m_criticMagic, m_baseCriticMagic);
			}
			else if(listSkills[i].GetType() == System.Type.GetType("BaseSkills"))
			{
				BaseSkills tmp = listSkills[i] as BaseSkills;
				tmp.update();
			}
		}
	}
	
	public void addSkill(Skills skill)
	{
		listSkills.Add(skill);
	}
	
	public Skills getSkill(int rang)
	{
		return listSkills[rang];
	}

	public List<Skills> getListOfSkills()
	{
		return listSkills;
	}

	public void setListOfSkills(List<Skills> other)
	{
		listSkills = other;
	}
}
