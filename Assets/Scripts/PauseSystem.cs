using UnityEngine;
using System.Collections;

public class PauseSystem : MonoBehaviour
{
	
	public bool paused;
	
	
	protected virtual void Start() // le jeu n'est pas en pause au départ
	{
		paused = false;
		UpdateState();
	}
	
	
	protected virtual void Update()
	{
	}
	
	protected virtual void UpdateState()
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
	}
	
}
