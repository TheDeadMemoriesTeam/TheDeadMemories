using UnityEngine;
using System.Collections;

public class PauseMenu : PauseSystem
{

	private bool isOption;
	public AudioManager aManager;
	public Terrain terrain;
	
	protected override void Start()
	{
		base.Start();
		isOption = false;
		terrain = Terrain.activeTerrain;
	}
	
	
	protected override void Update()
	{		
		if(!paused)
		{
			// si on appuie sur "Escape" change l'état du jeu
			if(Input.GetButtonDown("Menu"))
			{
				paused = true;
				
				UpdateState();
			}
		}
	}
	
	void OnGUI()
	{
		if(paused)
		{
			GUILayout.BeginArea(new Rect(Screen.width/2-50,Screen.height/2-50, 100,100));
			
			if(GUILayout.Button("Continuer"))
			{
				paused = false;
				
				UpdateState();
			}

			if(GUILayout.Button("Option"))
			{
				isOption = true;
			}

			if(GUILayout.Button("Menu"))
			{
				Application.LoadLevel(0);
			}
			
			GUILayout.EndArea();

			if (isOption == true)
			{
				option();
			}
		}
	}

	void option()
	{
		GUILayout.BeginArea(new Rect(Screen.width/2+70,Screen.height/2-50, 150,100));

		if(GUILayout.Button("Changer piste"))
		{
			aManager.changeTrack();
		}
		if(aManager.getPlayState())
		{
			if(GUILayout.Button ("Desactiver musique"))
			{
				aManager.changePlayState();
				aManager.pauseTrack();
			}
		}
		else
		{
			if(GUILayout.Button ("Réactiver musique"))
			{
				aManager.changePlayState();
				aManager.startTrack();
			}
		}

		GUILayout.Label ("Distance de rendu:");
		terrain.detailObjectDistance = GUILayout.HorizontalSlider (terrain.detailObjectDistance, 0, 250);

		GUILayout.EndArea();
	}
	
	
	protected override void UpdateState()
	{
		base.UpdateState();
		GetComponent<Inventory>().enabled = !paused;

		isOption = false;

		SkillsGUI[] GUIskills = FindObjectsOfType<SkillsGUI>();
		for (int i = 0 ; i  < GUIskills.Length ; i++)
			GUIskills[i].enabled = !paused;	
	}
	
}
