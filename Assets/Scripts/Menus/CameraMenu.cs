using UnityEngine;
using System.Collections;

public class CameraMenu : CameraPath
{
	public GameObject mainMenu;
	public GameObject launchMenu;
	public SubMenu settingsMenu;
	public SubMenu advancedSettingsMenu;
	public SubMenu achievementsMenu;
	public SubMenu creditsMenu;
	private Vector3 creditsPos;

	public Vector3 cameraPositionOffset;

	private bool arrived;
	private SubMenu callbackObj;

	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		arrived = true;
		callbackObj = null;

		Vector3 offset = new Vector3(0, 0, 1.7f);
		creditsPos = GameObject.Find("creditsTomb").transform.position  + cameraPositionOffset + offset;
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();

		arrived = isArrived();
		if (arrived) {
			if (callbackObj != null) {
				callbackObj.setInfFrontOf(true);
				callbackObj = null;
			}
		}
	}

	
	public void goToMainMenu()
	{
		goTo(mainMenu.transform.position + cameraPositionOffset);
		callbackObj = null;
	}
	
	public void goToLaunchMenu()
	{
		goTo(launchMenu.transform.position + cameraPositionOffset);
		callbackObj = null;
	}

	public void goToSettingsMenu()
	{
		goTo(settingsMenu.transform.position + cameraPositionOffset);
		callbackObj = settingsMenu;
	}

	public void goToAdvancedSettingsMenu()
	{
		Vector3 offset = new Vector3(2f, 4.1f, -6f);
		goTo(advancedSettingsMenu.transform.position + cameraPositionOffset + offset);
		callbackObj = advancedSettingsMenu;
	}

	public void goToAchievementsMenu()
	{
		goTo(achievementsMenu.transform.position + cameraPositionOffset);
		callbackObj = achievementsMenu;
	}

	public void goToCreditsMenu()
	{
		goTo(creditsPos);
		callbackObj = creditsMenu;
	}

}