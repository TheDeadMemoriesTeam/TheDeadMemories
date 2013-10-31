using UnityEngine;
using System.Collections;

public class Inventory : PauseSystem
{

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		// si on appuie sur "Escape" change l'état du jeu
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			paused = !paused;
			
			UpdateState();
		}
	}
	
	protected override void UpdateState()
	{
		base.UpdateState();
	}
}
