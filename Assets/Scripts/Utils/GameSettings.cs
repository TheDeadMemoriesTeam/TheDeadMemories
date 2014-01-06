using UnityEngine;
using System.Collections;

// Charge les réglages du jeu (qualité graphique, résolution, etc..)
public class GameSettings : MonoBehaviour 
{
	//Terrain du jeu
	private Terrain ground;

	// Use this for initialization
	void Start () 
	{
		//PlayerPrefs.DeleteAll();
		ground = FindObjectOfType<Terrain>();
		// Initialise les valeurs des régalges du jeu
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel", 3), true);
		Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width),
		                     PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height),
		                     true);

		ground.detailObjectDistance = PlayerPrefs.GetFloat("detailDistance", 100f);
		ground.detailObjectDensity = PlayerPrefs.GetFloat("detailDensity", 0.5f);
		ground.treeCrossFadeLength = PlayerPrefs.GetFloat("fadeLenght", 100f);
		ground.terrainData.wavingGrassStrength = PlayerPrefs.GetFloat("windSpeed", 0.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
