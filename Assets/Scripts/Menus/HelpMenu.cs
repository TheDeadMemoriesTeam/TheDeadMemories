using UnityEngine;
using System.Collections.Generic;

public class HelpMenu : SubMenu 
{
	// liste des textes qui seront affichés
	private List<string> texts;

	// Position sur la scrollBar
	private Vector2 scrollPosition = Vector2.zero;

	//Fenetre d'affichage de l'aide
	private Rect helpWindowRect;

	// variable de tailles (en %)
	private float coefWidth = 0.5f;
	private float coefHeight = 0.8f;
	
	// Déterminés automatiquement
	private int windowWidth;
	private int windowHeight;
	private float helpHeight = 0;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		// Récupère la liste des textes à afficher
		texts = helpTexts();
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
		helpWindowRect = new Rect(Screen.width/2 - windowWidth/2,
		                          Screen.height/2 - windowHeight/2,
		                          windowWidth,
		                          windowHeight);
		helpWindowRect = GUI.Window(1, helpWindowRect, helpWindowOpen, LanguageManager.Instance.GetTextValue("MainMenu.Help"));
	}

	// Rempli la fenetre de l'aide
	void helpWindowOpen(int windowId)
	{
		// Zone de dessin + scroll bars
		GUILayout.BeginArea(new Rect(10, 20, windowWidth, windowHeight));

		scrollPosition = GUI.BeginScrollView(	new Rect(0, 20, windowWidth-20, windowHeight-90),
		                                     	scrollPosition,
		                                     	new Rect(0, 20, windowWidth-40, helpHeight));

		// Texte à afficher
		string textDisplay = textToDisplay();
		GUIContent content = new GUIContent(textDisplay);
		// Largeur de l'aide
		float labelWidth = windowWidth-60;
		GUIStyle myStyle = new GUIStyle();
		// Détermine la hauteur de l'aide pour la largeur précédente
		float labelHeight = myStyle.CalcHeight( content, labelWidth);
		helpHeight = labelHeight + countNewLine()*12 + 20;

		// Affiche l'aide
		GUI.Label(new Rect(10, 20, labelWidth, helpHeight), textDisplay);

		GUI.EndScrollView();

		// Bouton de retour au menu principal
		float buttonWidth = 100;
		float buttonHeight = 30;
		if (GUI.Button(	new Rect((windowWidth-10)/2 - buttonWidth/2,
		                         windowHeight - 2f*buttonHeight,
		                         buttonWidth,
		                         buttonHeight),
		               "Return"))
		{
			setInfFrontOf(false);
			cam.goToMainMenu();
		}

		GUILayout.EndArea();
	}

	string textToDisplay()
	{
		string textToDisplay = "";

		for (int i = 0 ; i < texts.Count ; i++)
		{
			if (texts[i] == "\n")
				textToDisplay += System.Environment.NewLine;
			else
				textToDisplay += texts[i];
		}

		return textToDisplay;
	}

	int countNewLine()
	{
		int nbNewLine = 0;
		for (int i = 0 ; i < texts.Count ; i++)
		{
			if (texts[i] == "\n")
				nbNewLine++;
		}
		return nbNewLine;
	}

	List<string> helpTexts()
	{
		// Liste des textes à afficher dans l'aide
		// Affichés dans l'ordre ci-dessous
		// (mettre \n pour saut d'une ligne)
		List<string> helpMenuTexts = new List<string>();

		helpMenuTexts.Add("Aide de The Dead Memories");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Controles par défaut:");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Vous pouvez vous déplacer avec les flèches directionnelles ou les touches Z, Q, S et D et sprinter en maintenant la touche SHIFT enfoncée.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Vous pouvez attaquer au corps à corps avec un clic gauche et utiliser la magie (une fois la compétance acquise) avec un clic droit.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Vous pouvez réaliser un saut avec la touche Espace.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Vous pouvez consulter votre inventaire avec la touche Tab.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Vous pouvez mettre le jeu en pause avec la touche Echap.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Votre personnage peut débloquer une foule de compétences en se rendant dans une crypte, pour en débloquer de nouvelles, il vous faudra acquérir de l'expérience en tuant vos ennemis.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Vous pouvez controler 3 types d'éléments magique : le Feu, la Glace et le Vent.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Si vous désirez changer d'éléments vous pouvez utiliser les touches F1, F2 ou F3.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("Vous pouvez lancer des sorts plus ou moins puissant suivant le temps que vous laisser le bouton droit de la souris enfoncé :");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("- Un appui bref lancera une boule de l'élément sélectionné.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("- Un appui modéré lancera une attaque de zone de l'élément sélectionné.");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("- Un appui long lancera la super attaque de l'élément sélectionné.");

		return helpMenuTexts;
	}
}
