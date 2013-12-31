using UnityEngine;
using System.Collections;

// Charge les réglages du jeu (qualité graphique, résolution, etc..)
public class GameSettings : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		//PlayerPrefs.DeleteAll();
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel", 3), true);
		Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width),
		                     PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height),
		                     true);

		print("Level Quality Loaded : " + QualitySettings.GetQualityLevel() +
		      " Résolution Loaded : " + Screen.currentResolution.width + " x " + Screen.currentResolution.height);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
