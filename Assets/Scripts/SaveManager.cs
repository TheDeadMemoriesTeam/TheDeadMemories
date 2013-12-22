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

			// Récupère les achiements
			List<Achievement> achievementsUnlocked = achievementManager.getAchievementsUnlocked();
			List<Achievement> achievementsLocked = achievementManager.getAchievementsLocked();

			// Rassemble tous les achievements
			for (int i=0; i<achievementsUnlocked.Count; i++)
				achievementsLocked.Add(achievementsUnlocked[i]);

			int index;
			for (int i=0; i<achievementsLoaded.Count; i++)
			{
				index = achievementsLocked.FindIndex(
					delegate(Achievement obj) {
					return obj.getName() == achievementsLoaded[i];
				});
				if (index>=0)
				{
					achievementsUnlocked.Add(achievementsLocked[index]);
					achievementsLocked.RemoveAt(index);
				}
			}

			achievementManager.setAchievements(achievementsLocked, achievementsUnlocked);

			file.Close();
		}
	}
}
