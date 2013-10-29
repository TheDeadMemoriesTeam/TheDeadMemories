using UnityEngine;
using System.Collections;

public class AchivementManager : MonoBehaviour {
	
	// Texture achivement a mettre ici
	//...
	
	// Son achivement
	public AudioClip soundAchivement; 
	
	// Booléens des achivements
	private bool firstMove = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void playerFirstMove()
	{
		if (!firstMove)
		{
			Debug.Log("Achivement First Move !");
			// unlockAchivement(...);
			firstMove = !firstMove;
		}
	}
	
	void unlockAchivement(/* texture de l'achivement */)
	{
		// Affiche la texture à l'écran (passé en parametre)
		audio.PlayOneShot(soundAchivement);
	}
}