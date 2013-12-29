using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class AchievementsSaveReader : MonoBehaviour 
{

	// Chemin vers les fichiers de sauvegarde
	private string achievementPath = "./save/achievement.dat";

	// Liste des achievements accomplis
	private List<string> achievementsCompleted;

	// Use this for initialization
	void Start ()
	{
		readAchievementsSave();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	private void readAchievementsSave()
	{
		// Si le fichier existe
		if(File.Exists(achievementPath))
		{
			// Créé le formateur
			BinaryFormatter formater = new BinaryFormatter();
			
			// Créé le fichier
			Stream file = File.Open (achievementPath, FileMode.Open);
			
			// Charge la liste des achievements
			achievementsCompleted = formater.Deserialize(file) as List<string>;
			
			file.Close();
			
			Debug.Log("load achievements");
		}
		else
			achievementsCompleted = null;
	}

	public List<string> getAchievementsCompleted()
	{
		return achievementsCompleted;
	}
}
