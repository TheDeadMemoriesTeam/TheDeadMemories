using UnityEngine;
using System.Collections;

public class SettingsMenu : SubMenu
{
	//Fenetre d'affichage des achievements accomplis
	private Rect settingsWindowRect;
	
	// Position sur la scrollBar gauche
	private Vector2 scrollPositionLeft = Vector2.zero;
	// Position sur la scrollBar gauche
	private Vector2 scrollPositionRight = Vector2.zero;
	
	// variables de placement (en pixels)
	private int spaceBetweenItem = 30;
	private int paddingTop = 35;
	private int letterSize = 9;

	// variable de tailles (en %)
	private float coefWidth = 0.75f;
	private float coefHeight = 0.8f;
	
	// Déterminés automatiquement
	private int windowWidth;
	private int windowHeight;
	private int maxButtonSize = 0;

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

		// Détermine les dimensions de la fenetre à afficher
		windowWidth = (int)(Screen.width*coefWidth);
		windowHeight = (int)(Screen.height*coefHeight);
		
		// Fenetre à afficher
		settingsWindowRect = new Rect(	Screen.width/2 - windowWidth/2,
		                             	Screen.height/2 - windowHeight/2,
		                             	windowWidth,
		                             	windowHeight);
		settingsWindowRect = GUI.Window(2, settingsWindowRect, settingsWindowOpen, "Settings");
	}

	// Rempli la fenetre des achievements accomplis
	void settingsWindowOpen(int windowId)
	{
		// Zone de dessin
		GUILayout.BeginArea(new Rect(10, 20, windowWidth, windowHeight));

		GUI.Label(	new Rect(20, 10, 200, 30),
					"Available Resolutions :");

		/**********************************************
		 *************** Partie Gauche ****************
		**********************************************/

		// Récupère les résolutions disponibles
		Resolution[] resolutions = Screen.resolutions;

		// Scroll bar gauche
		scrollPositionLeft = GUI.BeginScrollView(	new Rect(0, paddingTop, windowWidth/2, 0.55f*windowHeight),
		                                         	scrollPositionLeft,
		                                         	new Rect(0, paddingTop, windowWidth/2-20, resolutions.Length * (spaceBetweenItem + 10)));

		// Récupère la résolution actuelle
		Resolution currentResolution = Screen.currentResolution;
		// Affiche les résolutions possibles
		for (int i = 0 ; i < resolutions.Length ; i++)
		{
			// Grise le bouton si c'est la résolution actuelle
			if (resolutions[i].width == currentResolution.width 
			    && resolutions[i].height == currentResolution.height)
				GUI.enabled = false;
			else
				GUI.enabled = true;

			string textButton = resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString();
			int resButtonWidth = textButton.Length * letterSize;
			if (GUI.Button(new Rect(20,
			                   		paddingTop + i*(spaceBetweenItem + 10),
			                   	 	maxButtonSize,
			                    	spaceBetweenItem),
			           		textButton))
			{
				Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
				PlayerPrefs.SetInt("ResolutionWidth", resolutions[i].width);
				PlayerPrefs.SetInt("ResolutionHeight", resolutions[i].height);
			}

			if (resButtonWidth > maxButtonSize)
				maxButtonSize = resButtonWidth;
		}
		GUI.enabled = true;

		GUI.EndScrollView();


		GUI.Label(	new Rect(windowWidth/2, 10, 200, 30),
		          "Available Graphics Quality :");

		/**********************************************
		 *************** Partie Droite ****************
		**********************************************/

		string[] qualities = QualitySettings.names;
		
		// Scroll bar droite
		scrollPositionRight = GUI.BeginScrollView(	new Rect(windowWidth/2, paddingTop, windowWidth/2-20, 0.55f*windowHeight),
		                                          	scrollPositionRight,
		                                          	new Rect(windowWidth/2, paddingTop, windowWidth/2-40, qualities.Length * (spaceBetweenItem + 10)));

		// Récupère le niveau de qualité actuel
		int levelQuality = QualitySettings.GetQualityLevel();
		// Affiche les qualités graphiques possibles
		for (int i = 0 ; i < qualities.Length ; i++)
		{
			// Grise le bouton si c'est le niveau de qualité déjà utilisé
			if (i == levelQuality)
				GUI.enabled = false;
			else
				GUI.enabled = true;

			if (GUI.Button(new Rect(windowWidth/2 + 20,
			                        paddingTop + i*(spaceBetweenItem + 10),
			                        100,
			                        spaceBetweenItem),
			               qualities[i]))
			{
				QualitySettings.SetQualityLevel(i, true);
				PlayerPrefs.SetInt("qualityLevel", i);
			}
		}
		GUI.enabled = true;

		GUI.EndScrollView();


		float buttonWidth = 0.33f * windowWidth;
		float buttonHeight = 0.1f * windowHeight;

		// Bouton de valeurs par défaut
		if (GUI.Button(	new Rect((windowWidth-10)/2 - buttonWidth - 20,
		                         windowHeight - 3.2f*buttonHeight,
		                         buttonWidth,
		                         buttonHeight),
		               "Default Values"))
		{
			// Restaure les valeurs par défaut
			QualitySettings.SetQualityLevel(4, true);
			PlayerPrefs.SetInt("qualityLevel", 4);

			Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
			PlayerPrefs.SetInt("ResolutionWidth", Screen.currentResolution.width);
			PlayerPrefs.SetInt("ResolutionHeight", Screen.currentResolution.height);
		}

		// Bouton des options avancées
		if (GUI.Button(	new Rect((windowWidth-10)/2 + 10,
		                         windowHeight - 3.2f*buttonHeight,
		                         buttonWidth,
		                         buttonHeight),
		               "Advanced Settings"))
		{
			setInfFrontOf(false);
			cam.activeRayCast();
			cam.goToAdvancedSettingsMenu();
		}

		// Bouton de retour au menu principal
		if (GUI.Button(	new Rect((windowWidth-10)/2 - buttonWidth/2,
		                         windowHeight - 2*buttonHeight,
		                         buttonWidth,
		                         buttonHeight),
		               "Return"))
		{
			setInfFrontOf(false);
			cam.goToMainMenu();
		}

		GUILayout.EndArea();
	}
}
