using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager{
	
	private AchievementManager achievement;
	private List<Achievement> achievements;

	// Chemin vers les fichiers de sauvegarde
	private string achievementPath = "./save/achievement.dat";
	private string skillPath;

	public SaveManager(AchievementManager achievementManager)
	{
		achievement = achievementManager;
	}

	// Fonction de sauvegarde
	public void save()
	{
		//achievement = FindObjectOfType(System.Type.GetType("AchievementManager")) as AchievementManager;
		achievements = achievement.getAchievementsUnlocked();
		Debug.Log ("a que coucou");

		// Créé le formater
		BinaryFormatter formater = new BinaryFormatter();
		// Crée le fichier
		Stream saveFile = File.Create(achievementPath);
		// Sauvegarde les achivements
		formater.Serialize(saveFile, "toto");

		saveFile.Close();
	}

	// Fonction de chargement
	public void load()
	{
		// Si le fichier existe
		if(File.Exists(achievementPath))
		{
			// Créé le formateur
			BinaryFormatter formater = new BinaryFormatter();

			// Créé le fichier
			Stream file = File.Open (achievementPath, FileMode.Open);

			// Charge la liste des achievements
			//achievements = formater.Deserialize(file) as List<Achievement>;
			object test = formater.Deserialize(file);

			Debug.Log (test);

			file.Close();
		}
	}
}
