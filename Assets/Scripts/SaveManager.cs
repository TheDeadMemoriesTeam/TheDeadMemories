using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager{
	
	private AchievementManager achievementManager;

	// Chemin vers les fichiers de sauvegarde
	private string achievementPath = "./save/achievement.dat";
	private string skillPath;

	public SaveManager(AchievementManager other)
	{
		achievementManager = other;
	}

	// Fonction de sauvegarde
	public void save()
	{
		List<Achievement> achievements = achievementManager.getAchievementsUnlocked();

		List<string> achievementList = new List<string>();
		achievementList.Capacity = achievements.Capacity;
		for (int i=0; i<achievements.Count; i++)
		     achievementList.Add(achievements[i].getName());

		// Créé le formater
		BinaryFormatter formater = new BinaryFormatter();
		// Crée le fichier
		Stream saveFile = File.Create(achievementPath);
		// Sauvegarde les achivements
		//formater.Serialize(saveFile, "toto");
		formater.Serialize(saveFile, achievementList);

		saveFile.Close();
		Debug.Log("save completed");
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
			List<string> achievementsLoaded = formater.Deserialize(file) as List<string>;

			achievementManager.loadAchievements(achievementsLoaded);

			file.Close();
			Debug.Log("save loaded");
		}
	}
}
