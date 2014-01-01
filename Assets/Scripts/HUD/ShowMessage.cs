using UnityEngine;
using System.Collections;

public class ShowMessage : MonoBehaviour 
{
	// Texte à afficher
	public GUIText message;
	
	// Temps pendant lequel le message est affiché
	public float timeShown = 4f;
	private float remainingTime;
	
	// Use this for initialization
	void Start () 
	{
		remainingTime = timeShown;
		message.enabled = false;
		message.pixelOffset = new Vector2(0, 0.23f*Screen.height);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (message.enabled)
		{
			// met à jour le temps restant d'affichage
			remainingTime -= Time.deltaTime;
			if (remainingTime <= 0)
				resetTime();

			// Fait un fondu pour la disparition
			Color messCol = message.color;
			message.color = new Color(messCol.r, messCol.g, messCol.b, determineAlpha());
		}
	}
	
	public void showMessage()
	{
		message.enabled = true;
	}

	// Reset la couleur du message et le temps restant d'affichage
	private void resetTime()
	{
		remainingTime = timeShown;
		message.enabled = false;
		Color messCol = message.color;
		message.color = new Color(messCol.r, messCol.g, messCol.b, 1f);
	}

	float determineAlpha()
	{
		return (remainingTime/timeShown);
	}
}
