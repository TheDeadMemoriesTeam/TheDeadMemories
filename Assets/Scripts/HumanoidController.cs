using UnityEngine;
using System.Collections;

// Permet de gérer la vie
public class HumanoidController : MonoBehaviour 
{
	
	protected int pv, pvMax;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public virtual void healthUpdate(int change)
	{
		pv += change;
		if (pv > pvMax)
			pv = pvMax;
	}
	
	public int getHitPoints()
	{
		return pv;
	}
	
	public int getMaxHitPoints()
	{
		return pvMax;
	}
}
