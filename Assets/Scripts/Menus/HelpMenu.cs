using UnityEngine;
using System.Collections;

public class HelpMenu : SubMenu 
{

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnGUI()
	{
		if (!inFrontOf)
			return;

		Debug.Log("draw");
	}

}
