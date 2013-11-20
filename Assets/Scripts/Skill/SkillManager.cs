using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour 
{
	//list des competances
	private List<Skills> listSkills;
	
	//variable afectant le player
	private int m_pv, m_pvMax, m_mana, m_manaMax;
	private float m_distanceP, m_distanceM;
	
	//acesseur
	public void setPv(int pv)
	{
		m_pv = pv;
	}
	
	public int getPv()
	{
		return m_pv;	
	}
	
	public void setPvMax(int pvMax)
	{
		m_pvMax = pvMax;
	}
	
	public int getPvMax()
	{
		return m_pvMax;	
	}
	
	public void setMana(int mana)
	{
		m_mana = mana;
	}
	
	public int getMana()
	{
		return m_mana;	
	}
	
	public void setManaMax(int manaMax)
	{
		m_manaMax = manaMax;
	}
	
	public int getManaMax()
	{
		return m_manaMax;	
	}
	
	public void setDistanceM(float distanceM)
	{
		m_distanceM = distanceM;
	}
	
	public float getDistanceM()
	{
		return m_distanceM;	
	}
	
	public void setDistanceP(float distanceP)
	{
		m_distanceP = distanceP;
	}
	
	public float getDistanceP()
	{
		return m_distanceP;	
	}
	
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
		
	}
	
	public void addSkill(Skills skill)
	{
		listSkills.Add(skill);
	}
	
	public Skills getSkill(int rang)
	{
		return listSkills[rang];
	}
}
