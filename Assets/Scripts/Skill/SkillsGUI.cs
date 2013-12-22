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

		List<Skills> listSkills = player.getSkillManager().getListOfSkills();
		//print("acheté : " + listSkills[0].getIsBought() + " débloqué : " + listSkills[0].getIsUnlock());
		for (int i = 0 ; i < 1/*listSkills.Count*/ ; i++)
		{
			if (listSkills[i].getIsBought())
			{
				PassiveSkills passiveSkillRank1 = listSkills[i] as PassiveSkills;

				GUI.enabled = false;
				GUI.Button(new Rect(50, 50, listSkills[i].getName().Length * 9, 30), listSkills[i].getName());

				GUI.enabled = true;
				if (GUI.Button(new Rect(125, 25, passiveSkillRank1.getNameFirstAd().Length * 11, 30), passiveSkillRank1.getNameFirstAd()))
				{
					// todo update les incrémentation
				}
				if (GUI.Button(new Rect(125, 75, passiveSkillRank1.getNameSecAd().Length * 11, 30), passiveSkillRank1.getNameSecAd()))
				{
					// todo update les incrémentation
				}

				if (listSkills[i+1].getIsBought())
				{
					PassiveSkills passiveSkillRank2 = listSkills[i+1] as PassiveSkills;

					GUI.enabled = false;
					GUI.Button(new Rect(195, 50, listSkills[i+1].getName().Length * 9, 30), listSkills[i+1].getName());

					GUI.enabled = true;
					if (GUI.Button(new Rect(300, 25, passiveSkillRank2.getNameFirstAd().Length * 11, 30), passiveSkillRank2.getNameFirstAd()))
					{
						// todo update les incrémentation
					}
					if (GUI.Button(new Rect(300, 75, passiveSkillRank2.getNameSecAd().Length * 11, 30), passiveSkillRank2.getNameSecAd()))
					{
						// todo update les incrémentation
					}

					if (listSkills[i+2].getIsBought())
					{
						GUI.enabled = false;
						GUI.Button(new Rect(450, 50, listSkills[i+2].getName().Length * 9, 30), listSkills[i+2].getName());
					}
					else if (listSkills[i+2].getIsUnlock())
					{
						GUI.enabled = true;
						if (GUI.Button(new Rect(450, 50, listSkills[i+2].getName().Length * 9, 30), listSkills[i+2].getName()))
							listSkills[i+2].setIsBought(true);
					}

				}
				else if (listSkills[i+1].getIsUnlock())
				{
					GUI.enabled = true;
					if (GUI.Button(new Rect(195, 50, listSkills[i+1].getName().Length * 9, 30), listSkills[i+1].getName()))
						listSkills[i+1].setIsBought(true);
				}

			}
			else if (listSkills[i].getIsUnlock())
			{
				GUI.enabled = true;
				if (GUI.Button(new Rect(50, 50, listSkills[i].getName().Length * 9, 30), listSkills[i].getName()))
					listSkills[i].setIsBought(true);
			}

		}

		//for (int i = 0 ; i < listSkills.Count ; i++)
			//GUI.Button(new Rect(i*50, i*50, listSkills[i].getName().Length * 9, 30), listSkills[i].getName());
		
		GUILayout.EndArea();
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

