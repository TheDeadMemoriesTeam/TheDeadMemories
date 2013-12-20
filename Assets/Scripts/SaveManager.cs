using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour {

	private AchievementManager achievement;
	private List<Achievement> achievements;

	// Use this for initialization
	void Start () {

		// Chargement à la suite
	}

	// Fonction de sauvegarde
	public void save()
	{
		achievement = FindObjectOfType(System.Type.GetType("AchievementManager")) as AchievementManager;
		achievements = achievement.getAchievementsUnlocked();

		// Créé le formater
		BinaryFormatter formater = new BinaryFormatter();
		// Crée le fichier
		Stream saveFile = File.Create("./save/achievement.dat");
		// Sauvegarde les achivements
		formater.Serialize(saveFile, "toto");
	}
}
