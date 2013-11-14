using UnityEngine;
using System.Collections;

public abstract class ActiveSkills : Skills 
{
	private float timeIncantation;
	
	//acsessor
	public void setIncantation(float time)
	{
		timeIncantation = time;	
	}
	
	public float getIncantation()
	{
		return timeIncantation;	
	}
	
	// Use this for initialization
	protected override void Start () 
	{
		
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
	
	}
}
