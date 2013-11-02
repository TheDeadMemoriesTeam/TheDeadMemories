using UnityEngine;
using System.Collections;

public class PauseMenu : PauseSystem
{
	
	protected override void Start()
	{
		base.Start();
	}
	
	
	protected override void Update()
	{		
		// si on appuie sur "Escape" change l'état du jeu
		if(Input.GetButtonDown("Menu"))
		{
			paused = !paused;
			player.onPause();
			
			UpdateState();
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
				player.onPause();
			}
			if(GUILayout.Button("Menu"))
			{
				// Renvoie au menu
			}
			
			GUILayout.EndArea();
			
			UpdateState();
		}
	}
	
	
	protected override void UpdateState()
	{
		base.UpdateState();
	}
	
}
