using UnityEngine;
using System.Collections.Generic;

public class AchievMenu : SubMenu 
{
	private AchievementManager am;

	//Fenetre d'affichage des achievements accomplis
	private Rect achievsWindowRect;

	// Position sur la scrollBar
	private Vector2 scrollPosition = Vector2.zero;

	// variables de placement (en pixels)
	private int spaceBetweenItem = 30;
	private int letterSize = 10;
	private int paddingTop = 20;

	// variable de tailles (en %)
	private float coefWidth = 0.4f;
	private float coefHeight = 0.66f;

	// Déterminés automatiquement
	private int windowWidth;
	private int windowHeight;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		// Récupère la sauvegarde des achievements accomplis
		AchievementsSaveReader asr = FindObjectOfType<AchievementsSaveReader>();

		am = FindObjectOfType<AchievementManager>();
		am.loadAchievements(asr.getAchievementsCompleted());
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
		achievsWindowRect = new Rect(Screen.width/2 - windowWidth/2,
		                             Screen.height/2 - windowHeight/2,
		                             windowWidth,
		                             windowHeight);
		achievsWindowRect = GUI.Window(1, achievsWindowRect, achievementsWindowOpen, "Achievements Completed");
	}

	// Rempli la fenetre des achievements accomplis
	void achievementsWindowOpen(int windowId)
	{
		List<Achievement> achievementsCompleted = am.getAchievementsUnlocked();

		// Zone de dessin + scroll bars
		GUILayout.BeginArea(new Rect(10, 20, windowWidth, windowHeight));
		
		scrollPosition = GUI.BeginScrollView(	new Rect(0, 10, windowWidth-20, 0.63f*windowHeight),
		                                        scrollPosition,
		                                        new Rect(0, 10, windowWidth-40, achievementsCompleted.Count*spaceBetweenItem + paddingTop));

		// Affiche la liste des achievements acquis
		for (int i = 0 ; i < achievementsCompleted.Count ; i++)
		{
			GUI.Label(	new Rect(20,
			                     paddingTop + i*spaceBetweenItem,
			                     achievementsCompleted[i].getName().Length * letterSize,
			                     spaceBetweenItem),
			          	new GUIContent(	achievementsCompleted[i].getName(),
			               				generateToolTip(achievementsCompleted[i].getDescription())));
		}

		GUI.EndScrollView();

		// Affiche la description de l'achievement survolé
		GUI.Label(	new Rect(20,
		                     0.63f*windowHeight + 10,
		                   	 windowWidth - 50,
		                   	 0.16f*windowHeight),
		          	GUI.tooltip);

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

	string generateToolTip(string description)
	{
		return 	"Description : "
				+ System.Environment.NewLine
				+ description;
	}
}
