using UnityEngine;
using System.Collections;

public class CameraMenu : CameraPath
{
	// active ou non le lancé de rayon en face de la caméra
	private bool rayCast = false;

	// Référence vers les différents menus
	private AchievMenu achievMenu;
	private SettingsMenu settingsMenu;
	private AdvancedSettingsMenu advencedSettingsMenu;
	private Credits credits;

	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		achievMenu = FindObjectOfType<AchievMenu>();
		settingsMenu = FindObjectOfType<SettingsMenu>();
		advencedSettingsMenu = FindObjectOfType<AdvancedSettingsMenu>();
		credits = FindObjectOfType<Credits>();
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();

		if (rayCast)
		{
			RaycastHit rayHit;
			Ray ray = new Ray(transform.position, transform.forward);
			if (Physics.Raycast(ray, out rayHit, 10f))
			{
				// Si on est en face du menu achievement
				if (rayHit.collider.gameObject.name == "achievTomb")
				{
					desactiveRayCast();
					achievMenu.setInfFrontOf(true);
				}
				// Si on est en face du menu Settings
				else if (rayHit.collider.gameObject.name == "settingsTomb")
				{
					desactiveRayCast();
					settingsMenu.setInfFrontOf(true);
				}
				// Si on est en face des Crédits
				else if (rayHit.collider.gameObject.name == "creditsTomb")
				{
					desactiveRayCast();
					credits.setInfFrontOf(true);
				}
				// Si on est en face du menu options avancées
				else if (rayHit.collider.gameObject.name == "advencedSettingsTomb")
				{
					desactiveRayCast();
					advencedSettingsMenu.setInfFrontOf(true);
				}
			}
		}
	}

	
	public void goToMainMenu()
	{
		goTo(150f, 1.5f, 27.8f);
	}
	
	public void goToLaunchMenu()
	{
		goTo(140f, 1.5f, 58f);
	}

	public void goToSettingsMenu()
	{
		goTo(165f, 1.5f, 57.8f);
	}

	public void goToAchievementsMenu()
	{
		goTo(180f, 1.5f, 47.8f);
	}

	public void goToCreditsMenu()
	{
		goTo(130.1f, 1.5f, 39.5f);
	}

	public void goToAdvancedSettingsMenu()
	{
		goTo(180f, 1.5f, 77.8f);
	}

	public void activeRayCast()
	{
		Invoke("setActiveRayCast", 0.5f);
	}

	private void setActiveRayCast()
	{
		rayCast = true;
	}

	public void desactiveRayCast()
	{
		rayCast = false;
	}
}