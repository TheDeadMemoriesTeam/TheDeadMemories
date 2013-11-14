using UnityEngine;
using System.Collections;

public class DegatSkills : MonoBehaviour 
{
	private float damage;
	
	//acsessor
	public void setDamage(float degat)
	{
		damage = degat;	
	}
	
	public float getDamage()
	{
		return damage;	
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
