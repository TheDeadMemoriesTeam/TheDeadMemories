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
		Debug.Log ("Succés sauvegardés");

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
			List<string> test = formater.Deserialize(file) as List<string>;
			//string test = formater.Deserialize(file) as string;

			Debug.Log ("Succés chargés");
			for (int i=0; i<test.Count; i++)
				Debug.Log (test[i]);

			List<Achievement> achievementsUnlocked = achievement.getAchievementsUnlocked();
			List<Achievement> achievementLocked = achievement.getAchievementsLocked();
			int index;

			Debug.Log (achievementsUnlocked.Count);

			for (int i=0; i<test.Count; i++)
			{
				index = achievementLocked.FindIndex(
					delegate(Achievement obj) {
					return obj.getName() == test[i];
				});
				achievementsUnlocked.Add (achievementLocked[index]);
				achievementLocked.RemoveAt(index);
			}

			Debug.Log (achievementsUnlocked.Count);

			file.Close();
		}
	}
}
