using UnityEngine;
using System.Collections;

public class SettingsMenu : SubMenu
{

	// Use this for initialization
	void Start () 
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
		Debug.Log("dessine settings");
	}
}
