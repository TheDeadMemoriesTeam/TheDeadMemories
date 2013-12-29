using UnityEngine;
using System.Collections;

public class CameraMenu : CameraPath
{
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();
	}

	
	public void goToMainMenu()
	{
		goTo(20f, 1.5f, 5f);
	}
	
	public void goToLaunchMenu()
	{
		goTo(25.4f, 1.5f, 19.4f);
	}

	public void goToSettingsMenu()
	{
		goTo(10f, 1.5f, 29f);
	}
}