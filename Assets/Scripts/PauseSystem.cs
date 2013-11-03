using UnityEngine;
using System.Collections;

public class PauseSystem : MonoBehaviour
{
	
	public bool paused;
	
	protected PlayerController player;
	
	
	protected virtual void Start() // le jeu n'est pas en pause au départ
	{
		paused = false;
		player = FindObjectOfType(System.Type.GetType("PlayerController")) as PlayerController;
		
		UpdateState(0);
	}
	
	
	protected virtual void Update()
	{
	}
	
	protected virtual void UpdateState(int ind)
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
		player.onPause();
		switch(ind)
		{
		case 0: 
			break;
		case 1:
			GetComponent<PauseMenu>().enabled = !paused;
			break;
		case 2:
			GetComponent<Inventory>().enabled = !paused;
			break;
		}
	}
	
}
