using UnityEngine;
using System.Collections;

public class SubMenu : MonoBehaviour 
{
	// Caméra en face du menu ou non
	protected bool inFrontOf = false;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void setInfFrontOf(bool state)
	{
		inFrontOf = state;
	}
}
