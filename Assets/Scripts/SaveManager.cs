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
		saveAchievements();
		saveSkillsActive();
		saveSkillsPassive();

		Debug.Log("save completed");
	}

	private void saveAchievements()
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
		formater.Serialize(saveFile, achievementList);
		// Libère la mémoire
		saveFile.Close();

		Debug.Log("save achievements");
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

		Debug.Log("save passive skills");
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

		Debug.Log("save active skills");
	}

	// Fonction de chargement
	public void load()
	{
		loadAchievements();
		loadSkillsActive();
		loadSkillsPassive();
		Debug.Log("save loaded");
	}

	private void loadAchievements()
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

			Debug.Log("load achievements");
		}
	}

	private void loadSkillsActive()
	{
		// Si le fichier existe
		if(File.Exists(achievementPath))
		{
			// Créé le formateur
			BinaryFormatter formater = new BinaryFormatter();
			
			// Créé le fichier
			Stream file = File.Open (activeSkillPath, FileMode.Open);
			
			// Charge la liste des achievements
			List<string> skillsLoaded = formater.Deserialize(file) as List<string>;
			
			List<Skills> skills = skillManager.getListOfSkills();

			// Réinitiale la liste
			for (int i=0; i<skills.Count; i++)
			{
				skills[i].setIsBought(false);
				skills[i].setIsUnlock(false);
			}

			// Récupère les compétences déjà achetées
			int index;
			for (int i=0; i<skillsLoaded.Count; i++)
			{
				index = skills.FindIndex(
					delegate(Skills obj) {
					return obj.getName() == skillsLoaded[i];
				});
				if(index != -1)
					skills[index].setIsBought(true);
			}

			// Retrouve les compétences débloquées
			for (int i=0; i<skills.Count; i++)
				skills[i].unlockedSkill();

			skillManager.setListOfSkills(skills);
			
			file.Close();

			Debug.Log("load active skills");
		}
	}

	private void loadSkillsPassive()
	{
		// Si le fichier existe
		if(File.Exists(achievementPath))
		{
			// Créé le formateur
			BinaryFormatter formater = new BinaryFormatter();
			
			// Créé le fichier
			Stream file = File.Open (passiveSkillPath, FileMode.Open);
			
			// Charge la liste des achievements
			List<string> skillsLoaded = formater.Deserialize(file) as List<string>;

			skillManager.setPvMax( float.Parse (skillsLoaded[0]) );
			skillManager.setManaMax( float.Parse (skillsLoaded[1]) );
			// 		Compétences physiques
			skillManager.setPhysicalResistance( float.Parse (skillsLoaded[2]) );
			skillManager.setPhysicAttack( float.Parse (skillsLoaded[3]) );
			skillManager.setDistancePhysicAttack( float.Parse (skillsLoaded[4]) );
			// 		Compétences magiques
			skillManager.setMagicResistance( float.Parse (skillsLoaded[5]) );
			skillManager.setMagicAttack( float.Parse (skillsLoaded[6]) );
			skillManager.setDistanceMagicAttack( float.Parse (skillsLoaded[7]) );

			// Mise à jour extérieur
			skillManager.setPv(skillManager.getPvMax());
			skillManager.setMana(skillManager.getManaMax());
			
			file.Close();

			Debug.Log("save passive skills");
		}
	}
}
