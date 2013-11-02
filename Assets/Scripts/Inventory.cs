using UnityEngine;
using System.Collections;

public class Inventory : PauseSystem
{
	private PlayerController player;
	private Hashtable inv;
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		player = FindObjectOfType(System.Type.GetType("PlayerController")) as PlayerController;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		if(Input.GetButtonDown("Inventory"))
		{
			paused = !paused;
			if (paused)
			{
				inv = player.getInv();
			}
	
			UpdateState();
		}
	}
	
	void OnGUI()
	{
		if (paused)
		{
			GUILayout.BeginArea(new Rect(Screen.width/2-50,Screen.height/2-50, 100,100));
			
			foreach(string keys in inv.Keys)
			{
				if(GUILayout.Button(keys + " " + inv[keys].ToString()))
				{
					chooseWork(keys);
					inv[keys] = (int)inv[keys]-1;
					if((int)inv[keys] <= 0)
						inv.Remove(keys);
					return;
				}	
			}
			
			GUILayout.EndArea();	
		}
	}
	
	protected override void UpdateState()
	{
		base.UpdateState();
	}
	
	void chooseWork(string key)
	{
		if(key == "Bone")
		{
			player.healthUpdate(50);
			return;
		}
		if(key == "ManaPotion")
		{
			player.manaUpdate(50);
			return;
		}
	}
}
