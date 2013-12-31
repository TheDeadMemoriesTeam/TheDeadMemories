using UnityEngine;
using System.Collections;

public class Credits : SubMenu 
{

	// Use this for initialization
	protected override void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI()
	{
		if (!inFrontOf)
			return;
		Debug.Log("dessine credits");
	}
}
