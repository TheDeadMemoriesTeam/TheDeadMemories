using UnityEngine;
using System.Collections;

public class MainMenu : Menu 
{
	// Boutons disponibles
	public bool isQuitButton = false;
	public bool isPlayButton = false;
	public bool isSettingsButton = false;
	public bool isAchievButton = false;
	public bool isCreditsButton = false;

	// Use this for initialization
	void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnMouseUp()
	{
		if(isQuitButton)
			Application.Quit();

		if(isPlayButton)
		{	
			cam.goToLaunchMenu();
			return;
		}
		
		if (isSettingsButton)
		{
			cam.goToSettingsMenu(); // TODO
			return;
		}

		if (isAchievButton)
		{
			// TODO
			return;
		}

		if (isCreditsButton)
		{
			// TODO
			return;
		}
	}
}
