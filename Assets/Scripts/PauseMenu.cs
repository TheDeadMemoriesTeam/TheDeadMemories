using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	
	public bool paused;
	
	
	void Start() // le jeu n'est pas en pause au départ
	{
		paused = false;
	}
	
	
	void Update()
	{
		
		if(paused)	// actualise la valeur du timeScale selon si "En Pause"
		{
			Time.timeScale = 0;
			
			// on fait apparaitre le curseur
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}
		else 		// ou pas
		{
			Time.timeScale = 1;
			
			// le cursor ne peux plus bouger et est caché
			Screen.showCursor = false;
			Screen.lockCursor = true;
		}
		
		
		// si on appuie sur "Escape" change l'état du jeu
		if(Input.GetKeyDown(KeyCode.Escape))
			paused = !paused;
	}
	
	
	
	void OnGUI()
	{
		if(paused)
		{
			Pause ();
		}
	}
	
	
	void Pause()
	{
		GUILayout.BeginArea(new Rect(Screen.width/2-50,Screen.height/2-50, 100,100));
		
		if(GUILayout.Button("Continuer"))
		{
			paused = false;
		}
		if(GUILayout.Button("Menu"))
		{
			// Renvoie au menu
		}
		
		GUILayout.EndArea();
	}
	
}
