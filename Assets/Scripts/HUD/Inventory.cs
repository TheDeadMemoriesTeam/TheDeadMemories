using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : PauseSystem
{
	private Hashtable inv;
	
	private Rect inventoryWindowRect = new Rect(Screen.width/2-130,Screen.height/2-200, 260, 250);
	private Dictionary<int, string> dictionaryItem;
	
	private bool[] listeButton = new bool[9];
	
	private Texture2D boneIcon;
	
	private Dictionary<int, string> inventoryItem = new Dictionary<int, string>()
	{
		{0, string.Empty},
		{1, string.Empty},
		{2, string.Empty},
		{3, string.Empty},
		{4, string.Empty},
		{5, string.Empty},
		{6, string.Empty},
		{7, string.Empty},
		{8, string.Empty}
	};
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		for(int i=0; i<9; i++)
			listeButton[i] = false;
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
			/*GUILayout.BeginArea(new Rect(Screen.width/2-50,Screen.height/2-50, 100,100));
			
			foreach(string keys in inv.Keys)
			{
				if(getButton(keys))
				{
					chooseWork(keys);
					inv[keys] = (int)inv[keys]-1;
					if((int)inv[keys] <= 0)
						inv.Remove(keys);
					return;
				}
			}
			
			GUILayout.EndArea();*/	
			
			inventoryWindowRect = GUI.Window(0, inventoryWindowRect, inventoryWindowMethod, "Inventaire");
		}
	}
	
	protected override void UpdateState()
	{
		base.UpdateState();
		GetComponent<PauseMenu>().enabled = !paused;
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
	
	void inventoryWindowMethod (int windowId)
	{
		//Définition des Items
		item bone = new item(0, "Bone", "Restaure de la vie");
		item manaPotion = new item(1, "ManaPotion", "restaure de la mana");
		
		//Mise a jour des items a afficher sur l'interface
		inventoryItem[0] = string.Empty;
		inventoryItem[1] = string.Empty;
		inventoryItem[2] = string.Empty;
		inventoryItem[3] = string.Empty;
		inventoryItem[4] = string.Empty;
		inventoryItem[5] = string.Empty;
		inventoryItem[6] = string.Empty;
		inventoryItem[7] = string.Empty;
		inventoryItem[8] = string.Empty;
		foreach(string keys in inv.Keys)
		{
			switch (keys)
			{
			case "Bone":
				if ((int)inv[keys]>0)
					inventoryItem[computeNbItem()] = bone.getName();
				break;
				
			case "ManaPotion":
				if ((int)inv[keys]>0)
					inventoryItem[computeNbItem()] = manaPotion.getName();
				break;
			}
		}
		
		
		
		//Dessine l'inventaire
		GUILayout.BeginArea(new Rect(5, 50, 260, 250));
		
		GUILayout.BeginHorizontal();
		listeButton[0] = GUILayout.Button(inventoryItem[0], GUILayout.Height(50), GUILayout.Width(80));
		listeButton[1] = GUILayout.Button(inventoryItem[1], GUILayout.Height(50), GUILayout.Width(80));
		listeButton[2] = GUILayout.Button(inventoryItem[2], GUILayout.Height(50), GUILayout.Width(80));
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		listeButton[3] = GUILayout.Button(inventoryItem[3], GUILayout.Height(50), GUILayout.Width(80));
		listeButton[4] = GUILayout.Button(inventoryItem[4], GUILayout.Height(50), GUILayout.Width(80));
		listeButton[5] = GUILayout.Button(inventoryItem[5], GUILayout.Height(50), GUILayout.Width(80));
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		listeButton[6] = GUILayout.Button(inventoryItem[6], GUILayout.Height(50), GUILayout.Width(80));
		listeButton[7] = GUILayout.Button(inventoryItem[7], GUILayout.Height(50), GUILayout.Width(80));
		listeButton[8] = GUILayout.Button(inventoryItem[8], GUILayout.Height(50), GUILayout.Width(80));
		GUILayout.EndHorizontal();
		
		GUILayout.EndArea();
		
		//Regarde les buttons actionnés et execute l'utilisation de l'item
		for (int i=0; i<9; i++)
		{
			if(listeButton[i])
			{
				chooseWork(inventoryItem[i]);
				inv[inventoryItem[i]] = (int)inv[inventoryItem[i]]-1;
				if((int)inv[inventoryItem[i]] <= 0)
					inv.Remove(inventoryItem[i]);
				return;
			}
		}
	}
	
	//Compte le nombre d'item qu'on a sur soi
	//Utile pour connaitre a quel emplacement du inventoryItem on peut placer un item
	int computeNbItem()
	{
		int i=0;
		while(inventoryItem[i] != string.Empty)
		{
			i++;
		}
		return i;
	}
}

public class item
{
	private int id;
	private string name;
	//private Texture2D icon;
	private string resume;
	
	public item(int in_id, string in_name, string in_resume)
	{
		id = in_id;
		name = in_name;
		//icon = in_icon;
		resume = in_resume;
	}
	
	public int getId(){return id;}
	public string getName(){return name;}
	public string getResume(){return resume;}
}
