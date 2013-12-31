using UnityEngine;
using System.Collections;

public class AdvancedSettingsMenu : SubMenu 
{
	// Terrain du jeu
	private Terrain ground;

	// variable de tailles (en %)
	private float coefWidth = 0.55f;
	private float coefHeight = 0.75f;
	private float offsetX = 0.1f;
	private float sliderSizeW = 0.5f;
	private float sliderSizeH = 0.05f;
	
	// Déterminés automatiquement
	private int windowWidth;
	private int windowHeight;
	private int marginLeft;
	private int sliderWidth;
	private int sliderHeight;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		ground = FindObjectOfType<Terrain>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI()
	{
		if (!inFrontOf)
			return;

		showAdvancedSettings();
	}

	void showAdvancedSettings()
	{
		// Détermine les dimensions de la zone de dessin
		windowWidth = (int)(Screen.width*coefWidth);
		windowHeight = (int)(Screen.height*coefHeight);
		marginLeft = (int)(Screen.width*offsetX);
		sliderWidth = (int)(Screen.width*sliderSizeW);
		sliderHeight = (int)(Screen.height*sliderSizeH);
		
		GUILayout.BeginArea(new Rect (	marginLeft,
		                              Screen.height/2 - windowHeight/2,
		                              windowWidth,
		                              windowHeight));

		GUILayout.Label ("Advanced Settings : ");

		GUILayout.Label ("Details distance : " + truncateValue(ground.detailObjectDistance, 0));
		ground.detailObjectDistance = GUILayout.HorizontalSlider (ground.detailObjectDistance, 0, 250, GUILayout.Width(sliderWidth), GUILayout.Height(sliderHeight));
		
		GUILayout.Label ("Details density : " + truncateValue(ground.detailObjectDensity, 1));
		ground.detailObjectDensity = GUILayout.HorizontalSlider (ground.detailObjectDensity, 0, 1, GUILayout.Width(sliderWidth), GUILayout.Height(sliderHeight));
		
		GUILayout.Label ("Fade Lenght : " + truncateValue(ground.treeCrossFadeLength, 0));
		ground.treeCrossFadeLength = GUILayout.HorizontalSlider (ground.treeCrossFadeLength, 0, 200, GUILayout.Width(sliderWidth), GUILayout.Height(sliderHeight));
		
		GUILayout.Label ("Wind Speed : " + truncateValue(ground.terrainData.wavingGrassStrength, 1));
		ground.terrainData.wavingGrassStrength = GUILayout.HorizontalSlider (ground.terrainData.wavingGrassStrength, 0, 1, GUILayout.Width(sliderWidth), GUILayout.Height(sliderHeight));
		
		float buttonWidth = 0.33f * windowWidth;
		float buttonHeight = 0.1f * windowHeight;
		
		// Bouton de valeurs par défaut
		if (GUI.Button(	new Rect((windowWidth-10)/2 - buttonWidth - 20,
		                         windowHeight - 3.2f*buttonHeight,
		                         buttonWidth,
		                         buttonHeight),
		               "Default Values"))
		{
			
		}
		
		// Bouton des options avancées
		if (GUI.Button(	new Rect((windowWidth-10)/2 + 10,
		                         windowHeight - 3.2f*buttonHeight,
		                         buttonWidth,
		                         buttonHeight),
		               "Return"))
		{
			setInfFrontOf(false);
			cam.goToMainMenu();
		}
		
		// Bouton de retour au menu principal
		if (GUI.Button(	new Rect((windowWidth-10)/2 - buttonWidth/2,
		                         windowHeight - 2*buttonHeight,
		                         buttonWidth,
		                         buttonHeight),
		               "Return Settings"))
		{
			setInfFrontOf(false);
			cam.goToSettingsMenu();
		}
		
		GUILayout.EndArea();
	}

	float truncateValue(float value, int nbDecimal)
	{
		float power = Mathf.Pow(10, nbDecimal);
		int temp = (int)(value*power);
		return temp / power;
	}
}
