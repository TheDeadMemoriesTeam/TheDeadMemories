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

	// achievements débloqués
	private AchievementsSaveReader asr;

	// Use this for initialization
	void Start () 
	{
		base.Start();
		AchievementsSaveReader asr = FindObjectOfType<AchievementsSaveReader>();
		if(asr.getAchievementsCompleted() == null)
		{
			GameObject txt3DAchiev = GameObject.Find("txt_Achiev");
			txt3DAchiev.renderer.enabled = false;
		}
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
			cam.activeRayCast();
			cam.goToSettingsMenu(); // TODO
			return;
		}

		if (isAchievButton)
		{
			cam.activeRayCast();
			cam.goToAchievementsMenu(); // TODO
			return;
		}

		if (isCreditsButton)
		{
			cam.activeRayCast();
			cam.goToCreditsMenu(); // TODO
			return;
		}
	}
}
