using UnityEngine;
using System.Collections;

public class PauseMenu : PauseSystem
{

	public GUITexture pausePanel;
	public GUIStyle buttonGUIStyle;
	public Font buttonFont;

	protected override void Start()
	{
		base.Start();
		//on cache le fond du menu pause
		pausePanel.pixelInset=new Rect(100, -64, 256, 64);
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

				pausePanel.pixelInset = new Rect(0, 0, Screen.width, Screen.height);

			}
		}
	}


	void OnGUI()
	{
		buttonGUIStyle = new GUIStyle();
		buttonGUIStyle.normal.textColor = Color.white;
		buttonGUIStyle.fontStyle=FontStyle.Bold;
		buttonGUIStyle.fontSize = 40;  
		buttonGUIStyle.font = buttonFont;

		if(paused)
		{
			


			GUILayout.BeginArea(new Rect(Screen.width/2-50,Screen.height/2-50, 200,200));
			
			if(GUILayout.Button("Continuer", buttonGUIStyle))
			{
				paused = false;
				
				UpdateState();

				//on cache le fond du menu pause
				pausePanel.pixelInset=new Rect(100, -64, 256, 64);
			}
			if(GUILayout.Button("Menu", buttonGUIStyle))
			{
				player.achievementManager.saveAchievements();
				Application.LoadLevel(0);
			}
			
			GUILayout.EndArea();
			
		}
	}
	
	
	protected override void UpdateState()
	{
		base.UpdateState();
		GetComponent<Inventory>().enabled = !paused;
	}
	
}
