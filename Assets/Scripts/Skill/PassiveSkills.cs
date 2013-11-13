using UnityEngine;
using System.Collections;

public abstract class PassiveSkills : Skills 
{
	private int lvlSkill = 0;
	
	//acsessor
	public void setLvlSkill(int lvl)
	{
		lvlSkill = lvl;	
	}
	
	public int getLvlSkill()
	{
		return lvlSkill;	
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
