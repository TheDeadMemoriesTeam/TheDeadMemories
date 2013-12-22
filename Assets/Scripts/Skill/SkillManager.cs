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

	//resistance magique
	private float m_baseMagicResistance, m_magicResistance;

	//attaque physic
	private float m_basePhysicAttack, m_physicAttack;

	//attaque magic
	private float m_baseMagicAttack, m_magicAttack;

	//distance d'attaque
	private float m_distancePhysicAttack, m_distanceMagicAttack;
	
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
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			for (int i=0; i<listSkills.Count; i++)
			{
				if(listSkills[i].GetType() == System.Type.GetType("PassiveSkills"))
				{
					PassiveSkills tmp = listSkills[i] as PassiveSkills;
					if(tmp.getName() == "Survie")
						tmp.update(ref m_manaMax, m_baseManaMax, ref m_pvMax, m_basePvMax);
					else if(tmp.getName() == "Resistance")
						tmp.update(ref m_physicalResistance, m_basePhysicalResistance, ref m_magicResistance, m_baseMagicResistance);
					else if(tmp.getName() == "Attaque de base")
						tmp.update(ref m_physicAttack, m_basePhysicAttack, ref m_magicAttack, m_baseMagicAttack);
				}
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
}
