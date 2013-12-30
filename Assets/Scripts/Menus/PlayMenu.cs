using UnityEngine;
using System.Collections;

public class PlayMenu : Menu 
{
	// Boutons disponibles
	public bool isNewButton = false;
	public bool isLoadButton = false;
	public bool isReturn = false;

	// Use this for initialization
	void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnMouseUp()
	{
		if(isReturn)
		{
			cam.goToMainMenu();
			return;
		}

		if(isLoadButton)
		{
			// TODO
			return;
		}

		if(isNewButton)
		{
			Application.LoadLevel("testScene");
			return;
		}
	}
}
