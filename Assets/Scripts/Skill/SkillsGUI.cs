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
		if (isActive && skillsOpen)
			skillsWindowRect = GUI.Window(1, skillsWindowRect, skillsWindowOpen, "Arbre des compétences");
	}
	
	void skillsWindowOpen(int windowId)
	{
		
		GUILayout.BeginArea(new Rect(10, 20, Screen.width-10, Screen.height-20));

		List<Skills> listSkills = player.getSkillManager().getListOfSkills();

		for (int i = 0 ; i < 1/*listSkills.Count*/ ; i++)
		{
			PassiveSkills skill = listSkills[i] as PassiveSkills;
			if (listSkills[i].getIsBought())
			{
				GUI.enabled=false;
				GUI.Button(new Rect(50, 50, listSkills[i].getName().Length * 9, 30), listSkills[i].getName());

				GUI.enabled = true;
				GUI.Button(new Rect(150, 25, skill.getNameFirstAd().Length * 11, 30), skill.getNameFirstAd());
				GUI.Button(new Rect(150, 75, skill.getNameSecAd().Length * 11, 30), skill.getNameSecAd());
			}
			else if (listSkills[i].getIsUnlock())
			{
				GUI.enabled = true;
				if (GUI.Button(new Rect(50, 50, listSkills[i].getName().Length * 9, 30), listSkills[i].getName()))
					listSkills[i].setIsBought(true);
			}

		}
		
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

