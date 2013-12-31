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

		// Zone de dessin
		GUILayout.BeginArea(new Rect (	marginLeft,
		                              Screen.height/2 - windowHeight/2,
		                              windowWidth,
		                              windowHeight));

		GUILayout.Label ("Advanced Settings : ");

		// Sliders de réglages
		float detailDistance = PlayerPrefs.GetFloat("detailDistance");
		GUILayout.Label ("Details distance : " + truncateValue(detailDistance, 0));
		ground.detailObjectDistance = GUILayout.HorizontalSlider (detailDistance, 0, 250, GUILayout.Width(sliderWidth), GUILayout.Height(sliderHeight));
		PlayerPrefs.SetFloat("detailDistance", ground.detailObjectDistance);

		float detailDensity = PlayerPrefs.GetFloat("detailDensity");
		GUILayout.Label ("Details density : " + truncateValue(detailDensity, 1));
		ground.detailObjectDensity = GUILayout.HorizontalSlider (detailDensity, 0, 1, GUILayout.Width(sliderWidth), GUILayout.Height(sliderHeight));
		PlayerPrefs.SetFloat("detailDensity", ground.detailObjectDensity);

		float fadeLenght = PlayerPrefs.GetFloat("fadeLenght");
		GUILayout.Label ("Fade Lenght : " + truncateValue(fadeLenght, 0));
		ground.treeCrossFadeLength = GUILayout.HorizontalSlider (fadeLenght, 0, 200, GUILayout.Width(sliderWidth), GUILayout.Height(sliderHeight));
		PlayerPrefs.SetFloat("fadeLenght", ground.treeCrossFadeLength);

		float windSpeed = PlayerPrefs.GetFloat("windSpeed");
		GUILayout.Label ("Wind Speed : " + truncateValue(windSpeed, 1));
		ground.terrainData.wavingGrassStrength = GUILayout.HorizontalSlider (windSpeed, 0, 1, GUILayout.Width(sliderWidth), GUILayout.Height(sliderHeight));
		PlayerPrefs.SetFloat("windSpeed", ground.terrainData.wavingGrassStrength);


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
			PlayerPrefs.SetFloat("detailDistance", 100f);
			PlayerPrefs.SetFloat("detailDensity", 0.5f);
			PlayerPrefs.SetFloat("fadeLenght", 100f);
			PlayerPrefs.SetFloat("windSpeed", 0.5f);
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
