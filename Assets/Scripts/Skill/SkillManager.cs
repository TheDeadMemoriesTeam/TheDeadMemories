using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour 
{
	//list des competances
	private List<Skills> listSkills;
	
	// Use this for initialization
	void Start () 
	{
		//creation de la liste
		listSkills = new List<Skills>();
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
