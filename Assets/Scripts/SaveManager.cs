using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager{
	
	private AchievementManager achievementManager;
	private SkillManager skillManager;

	// Chemin vers les fichiers de sauvegarde
	private string achievementPath = "./save/achievement.dat";
	private string passiveSkillPath =  "./save/skillsPassive.dat";
	private string activeSkillPath =  "./save/skillsActive.dat";

	public SaveManager(AchievementManager otherA, SkillManager otherS)
	{
		achievementManager = otherA;
		skillManager = otherS;
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

	private void saveSkillsPassive()
	{
		List<string> skills = new List<string>();

		// Compétences passive
		// 		Consumable
		skills.Add (skillManager.getPvMax().ToString());
		skills.Add (skillManager.getManaMax().ToString());
		// 		Compétences physiques
		skills.Add (skillManager.getPhysicalResistance().ToString());
		skills.Add (skillManager.getPhysicAttack().ToString());
		skills.Add (skillManager.getDistancePhysicAttack().ToString());
		// 		Compétences magiques
		skills.Add (skillManager.getMagicResistance().ToString());
		skills.Add (skillManager.getMagicAttack().ToString());
		skills.Add (skillManager.getDistanceMagicAttack().ToString());

		// Créé le formater
		BinaryFormatter formater = new BinaryFormatter();
		// Crée le fichier
		Stream saveFile = File.Create(passiveSkillPath);
		// Sauvegarde les achivements
		formater.Serialize(saveFile, skills);
		// Libère la mémoire
		saveFile.Close();
	}

	private void saveSkillsActive()
	{
		// Compétences actives
		List<Skills> skillsSk = skillManager.getListOfSkills();
		List<string> skillsStr = new List<string>();
		
		for (int i=0; i<skillsSk.Count; i++)
		{
			if(skillsSk[i].getIsBought())
				skillsStr.Add(skillsSk[i].getName());
		}

		// Créé le formater
		BinaryFormatter formater = new BinaryFormatter();
		// Crée le fichier
		Stream saveFile = File.Create(activeSkillPath);
		// Sauvegarde les achivements
		formater.Serialize(saveFile, skillsStr);
		// Libère la mémoire
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

			achievementManager.loadAchievements(achievementsLoaded);

			file.Close();
			Debug.Log("save loaded");
		}
	}
}
