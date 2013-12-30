using UnityEngine;
using System.Collections;

public class SettingsMenu : SubMenu
{
	//Fenetre d'affichage des achievements accomplis
	private Rect settingsWindowRect;
	
	// Position sur la scrollBar gauche
	private Vector2 scrollPositionLeft = Vector2.zero;
	
	// variables de placement (en pixels)
	public int spaceBetweenItem = 30;
	public int paddingTop = 35;
	public int letterSize = 9;

	// variable de tailles (en %)
	public float coefWidth = 0.75f;
	public float coefHeight = 0.8f;
	
	// Déterminés automatiquement
	private int windowWidth;
	private int windowHeight;
	private int maxButtonSize = 0;

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

		// Récupère les résolutions disponibles
		Resolution[] resolutions = Screen.resolutions;

		// Scroll bar gauche
		scrollPositionLeft = GUI.BeginScrollView(	new Rect(0, paddingTop, windowWidth/2, 0.68f*windowHeight),
		                                         	scrollPositionLeft,
		                                         	new Rect(0, paddingTop, windowWidth/2-20, resolutions.Length * spaceBetweenItem));

		// Affiche les résolutions possibles
		for (int i = 0 ; i < resolutions.Length ; i++)
		{
			string textButton = resolutions[i].width.ToString() + "*" + resolutions[i].height.ToString();
			int resButtonWidth = textButton.Length * letterSize;
			if (GUI.Button(new Rect(20,
			                   		paddingTop + i*spaceBetweenItem,
			                   	 	maxButtonSize,
			                    	spaceBetweenItem),
			           		textButton))
			{
				Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
			}

			if (resButtonWidth > maxButtonSize)
				maxButtonSize = resButtonWidth;
		}
		
		GUI.EndScrollView();

		// Bouton de retour au menu principal
		float buttonWidth = 0.33f * windowWidth;
		float buttonHeight = 0.1f * windowHeight;
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
