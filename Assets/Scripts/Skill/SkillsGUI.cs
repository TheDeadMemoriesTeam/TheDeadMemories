using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillsGUI : MonoBehaviour 
{
	private bool isActive = false;
	private bool skillsOpen = false;
	
	//Fenetre d'achat des skills
	private Rect skillsWindowRect = new Rect(0,0, Screen.width, Screen.height);
	
	public PlayerController player;
	
	public GUIText openSkills;
	
	private int marginBetweenSkillTree = 100; 	// Marge en pixel entre 2 arbres de compétences 
	
	// Use this for initialization
	void Start () 
	{
		openSkills.text = "Press P to buy Skills";
		openSkills.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isActive)
			return;
		
		if (Input.GetKeyUp(KeyCode.P))
		{
			//les stats skills
			player.getSkillManager().update();
			// Met le joueur en pause pour qu'il ne se déplace pas en meme temps qu'il achète ses skills
			player.onPause();
			// Change l'état affiché/masqué du panneau d'achat des skills
			skillsOpen = !skillsOpen;
			// Affiche ou masque le curseur
			Screen.showCursor = !Screen.showCursor;
			Screen.lockCursor = !Screen.lockCursor;
		}
		
		// Affiche ou non le message
		if (!skillsOpen)
			openSkills.enabled = true;
		else
			openSkills.enabled = false;
	}
	
	void OnGUI()
	{
		// Ouvre la fenetre des arbres de compétences
		if (isActive && skillsOpen)
			skillsWindowRect = GUI.Window(1, skillsWindowRect, skillsWindowOpen, "Arbre des compétences");
	}
	
	void skillsWindowOpen(int windowId)
	{
		
		GUILayout.BeginArea(new Rect(10, 20, Screen.width-10, Screen.height-20));

		showGUIPassiveSkills(0, 2);

		//for (int i = 0 ; i < listSkills.Count ; i++)
			//GUI.Button(new Rect(i*50, i*50, listSkills[i].getName().Length * 9, 30), listSkills[i].getName());
		
		GUILayout.EndArea();
	}

	void showGUIPassiveSkills(int beginIndex, int skillTree)
	{
		List<Skills> listSkills = player.getSkillManager().getListOfSkills();

		int heightFromTop = -50;	// On démarre à 50 pixels du bord supérieur
		// Parcours de la liste des skills pour le nombre de sous arbre demandés
		for (int i = beginIndex ; i < skillTree*3 ; i+=3)
		{
			heightFromTop += marginBetweenSkillTree;

			// Si le premier skill est déjà acheté
			if (listSkills[i].getIsBought())
			{
				PassiveSkills passiveSkillRank1 = listSkills[i] as PassiveSkills;
				
				// Le skill étant acheté on l'affiche avec un bouton grisé
				GUI.enabled = false;
				GUI.Button(new Rect(50, heightFromTop, listSkills[i].getName().Length * 9, 30), listSkills[i].getName());
				
				// On affiche les boutons des 2 branches de skills qui suivent
				GUI.enabled = passiveSkillRank1.getCostIncFirstAd() <= player.getExperience();
				if (GUI.Button(new Rect(125, heightFromTop-25, passiveSkillRank1.getNameFirstAd().Length * 11, 30), passiveSkillRank1.getNameFirstAd()))
					upgradePassiveLittleSkill(passiveSkillRank1);

				GUI.enabled = passiveSkillRank1.getCostIncSecAd() <= player.getExperience();
				if (GUI.Button(new Rect(125, heightFromTop+25, passiveSkillRank1.getNameSecAd().Length * 11, 30), passiveSkillRank1.getNameSecAd()))
					upgradePassiveLittleSkill(passiveSkillRank1, false);

				// Vérifie si le second skill peut etre débloqué
				listSkills[i+1].unlockedSkill();
				
				// Si le second skill est déjà acheté
				if (listSkills[i+1].getIsBought())
				{
					PassiveSkills passiveSkillRank2 = listSkills[i+1] as PassiveSkills;
					
					// Le skill étant acheté on l'affiche avec un bouton grisé
					GUI.enabled = false;
					GUI.Button(new Rect(195, heightFromTop, listSkills[i+1].getName().Length * 9, 30), listSkills[i+1].getName());
					
					// On affiche les boutons des 2 branches de skills qui suivent
					GUI.enabled = passiveSkillRank2.getCostIncFirstAd() <= player.getExperience();
					if (GUI.Button(new Rect(300, heightFromTop-25, passiveSkillRank2.getNameFirstAd().Length * 11, 30), passiveSkillRank2.getNameFirstAd()))
						upgradePassiveLittleSkill(passiveSkillRank2);

					GUI.enabled = passiveSkillRank2.getCostIncFirstAd() <= player.getExperience();
					if (GUI.Button(new Rect(300, heightFromTop+25, passiveSkillRank2.getNameSecAd().Length * 11, 30), passiveSkillRank2.getNameSecAd()))
						upgradePassiveLittleSkill(passiveSkillRank2, false);

					// Vérifie si le dernier skill peut etre débloqué
					listSkills[i+2].unlockedSkill();
					
					// Si le dernier skill est déjà acheté
					if (listSkills[i+2].getIsBought())
					{
						// Le skill étant acheté on l'affiche avec un bouton grisé
						GUI.enabled = false;
						GUI.Button(new Rect(450, heightFromTop, listSkills[i+2].getName().Length * 9, 30), listSkills[i+2].getName());
					}
					// Si le dernier skill est débloqué
					else if (listSkills[i+2].getIsUnlock())
					{
						// On active ou non le bouton si on a suffisamment d'expérience
						GUI.enabled = listSkills[i+2].getPrice() <= player.getExperience();
						
						// Si on achète le skill on met à jour l'expérience du joueur
						if (GUI.Button(new Rect(450, heightFromTop, listSkills[i+2].getName().Length * 9, 30), listSkills[i+2].getName()))
							unlockSkill(listSkills[i+2]);
					}
					
				}
				// Si le deuxième skill est débloqué
				else if (listSkills[i+1].getIsUnlock())
				{
					// On active ou non le bouton si on a suffisamment d'expérience
					GUI.enabled = listSkills[i+1].getPrice() <= player.getExperience();
					
					// Si on achète le skill on met à jour l'expérience du joueur
					if (GUI.Button(new Rect(195, heightFromTop, listSkills[i+1].getName().Length * 9, 30), listSkills[i+1].getName()))
						unlockSkill(listSkills[i+1]);
				}
				
			}
			// Si le premier skill est débloqué
			else if (listSkills[i].getIsUnlock())
			{
				// On active ou non le bouton si on a suffisamment d'expérience
				GUI.enabled = listSkills[i].getPrice() <= player.getExperience();
				
				// Si on achète le skill on met à jour l'expérience du joueur
				if (GUI.Button(new Rect(50, heightFromTop, listSkills[i].getName().Length * 9, 30), listSkills[i].getName()))
					unlockSkill(listSkills[i]);
			}
			
		}
	}

	void upgradePassiveLittleSkill(PassiveSkills skillRank, bool first = true)
	{
		if (first)
		{
			int newLevel = skillRank.getLvlFirstAd() + 1;
			skillRank.setLvlFirstAd(newLevel);
			player.experienceUpdate(-skillRank.getCostIncFirstAd());
		}
		else
		{
			int newLevel = skillRank.getLvlSecAd() + 1;
			skillRank.setLvlSecAd(newLevel);
			player.experienceUpdate(-skillRank.getCostIncSecAd());
		}
	}

	void unlockSkill(Skills skill)
	{
		skill.setIsBought(true);
		player.experienceUpdate(-skill.getPrice());
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			isActive = true;
			return;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			isActive = false;
			openSkills.enabled = false;
			return;
		}
	}
}

