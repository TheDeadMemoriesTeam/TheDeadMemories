using UnityEngine;
using System.Collections;

public class CameraMenu : CameraPath
{
	// Position de la camera face aux menus
	public Vector3 mainMenuCamPos;
	public Vector3 launchMenuCamPos;
	public Vector3 settingsMenuCamPos;
	public Vector3 advancedSettingsMenuCamPos;
	public Vector3 achievementsMenuCamPos;
	public Vector3 creditsMenuCamPos;


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
		goTo(mainMenuCamPos);
	}
	
	public void goToLaunchMenu()
	{
		goTo(launchMenuCamPos);
	}

	public void goToSettingsMenu()
	{
		goTo(settingsMenuCamPos);
	}

	public void goToAdvancedSettingsMenu()
	{
		goTo(advancedSettingsMenuCamPos);
	}

	public void goToAchievementsMenu()
	{
		goTo(achievementsMenuCamPos);
	}

	public void goToCreditsMenu()
	{
		goTo(creditsMenuCamPos);
	}

}