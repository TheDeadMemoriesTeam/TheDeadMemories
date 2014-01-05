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
		helpHeight = labelHeight + countNewLine()*10 + 20;

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
		               	LanguageManager.Instance.GetTextValue("MainMenu.Return")))
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

		int i = 0;
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add("\n");
		helpMenuTexts.Add(LanguageManager.Instance.GetTextValue("MenuHelp.Help" + (i++)));

		return helpMenuTexts;
	}
}
