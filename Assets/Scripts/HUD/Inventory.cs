using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : PauseSystem
{
	// Nombre d'item par ligne d'inventaire
	private int nbItemPerLine = 3;
	// Nombre de place dans l'inventaire
	private int nbLocationInInventory;
	
	private Rect inventoryWindowRect = new Rect(Screen.width/2-130,Screen.height/2-200, 260, 250);
	
	// Liste de l'état des boutons de l'inventaire (true = occupé, false = libre)
	private bool[] listeButton;
	
	// Association <id bouton, id item> pour l'affichage de l'inventaire
	private Dictionary<int, int> inventoryItem;
	
	// liste des items existants
	private List<Item> listOfItem;
	
	// Inventaire <item, quantité possédée>
	private Dictionary<Item, int> inventory;
	
	
	
	protected void Awake()
	{
		nbLocationInInventory = nbItemPerLine*nbItemPerLine;
		
		// initialise le dictionnary pour l'affichage de l'inventaire
		inventoryItem = new Dictionary<int, int>();
		for (int i = 0 ; i < nbLocationInInventory; i++)
			inventoryItem.Add(i, -1);
		
		listeButton = new bool[nbLocationInInventory];
		
		// Liste de tous les items
		listOfItem = new List<Item>();
		listOfItem.Add(new Item(0, "Bone", "Restaure de la vie"));
		listOfItem.Add(new Item(1, "ManaPotion", "Restaure de la mana"));
		
		inventory = new Dictionary<Item, int>();
		
		for(int i=0; i<9; i++)
			listeButton[i] = false;
	}
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		if(Input.GetButtonDown("Inventory"))
		{
			paused = !paused;
			UpdateState();
		}
	}
	
	void OnGUI()
	{
		if (paused)
			inventoryWindowRect = GUI.Window(0, inventoryWindowRect, inventoryWindowMethod, "Inventaire");
	}
	
	void inventoryWindowMethod (int windowId)
	{
		//Mise a jour des items a afficher sur l'interface
		resetInventoryItem();

		// Met à jour chaque emplacement de l'inventaire en mettant l'id de l'item à afficher
		updateGUIInventory();
		
		// Dessine le GUI de l'inventaire
		drawInventory();
		
		//Regarde les buttons actionnés et execute l'utilisation de l'item
		checkButtonClick();
	}
	
	// Dessine le GUI d'un inventaire carré (3*3, 4*4 etc...)
	void drawInventory()
	{
		//Dessine l'inventaire
		GUILayout.BeginArea(new Rect(5, 50, 260, 250));
		
		for (int i = 0 ; i < nbItemPerLine ; i++)
		{
			GUILayout.BeginHorizontal();
			for (int j = 0 ; j < nbItemPerLine ; j++)
				listeButton[i*nbItemPerLine+j] = GUILayout.Button(inventoryItem[i*nbItemPerLine+j].ToString(), GUILayout.Height(50), GUILayout.Width(80));
			GUILayout.EndHorizontal();
		}
		GUILayout.EndArea();
	}
	
	// Met a jour l'inventaire avec les Id des items possédés
	void updateGUIInventory()
	{
		foreach(Item item in inventory.Keys)
			inventoryItem[computeNbItem()] = item.getId();
	}
	
	// Vérifie les boutons et si l'un est cliqué
	// si cliqué utilise l'item
	// et met a jour les valeurs de l'inventaire (quantité item -1)
	void checkButtonClick()
	{
		for (int i = 0 ; i < nbLocationInInventory ; i++)
		{
			if(listeButton[i])
			{
				chooseWork(inventoryItem[i]);
				updateQuantity(inventoryItem[i]);
				return;
			}
		}
	}
	
	// Retourne l'index du premier emplacement d'inventaire disponible
	int computeNbItem()
	{
		int i=0;
		while(inventoryItem[i] != -1)
			i++;
		return i;
	}
	
	// Remet toute les valeurs de l'inventaire à "vide"
	void resetInventoryItem()
	{
		for (int i = 0 ; i < inventoryItem.Count ; i++)
			inventoryItem[i] = -1;
	}
	
	// Ajoute l'item à l'inventaire
	// L'opération n'est effectué que si l'item existe
	public void addItem(string nameItem)
	{
		// Parcours la liste des items
		for (int i = 0 ; i < listOfItem.Count ; i++)
		{
			// Si on a trouvé l'item dans la liste des item
			if (listOfItem[i].getName() == nameItem)
			{
				// On incrémente l'inventaire s'il y a déjà des item de ce type
				if (inventory.ContainsKey(listOfItem[i]))
					inventory[listOfItem[i]]++;
				// sinon on l'ajoute que s'il reste des places dans l'inventaire
				else
				{
					if (inventory.Count >= nbLocationInInventory)
						throw new System.InvalidOperationException("Inventory full");
					inventory.Add(listOfItem[i], 1);
				}
				break;
			}
		}
	}
	
	// Utilise l'item avec l'id passé en parametre
	void chooseWork(int id)
	{
		string itemName = "";
		for (int i = 0 ; i < listOfItem.Count ; i++)
		{
			if (id == listOfItem[i].getId())
				itemName = listOfItem[i].getName();	
		}
		
		if(itemName == "Bone")
		{
			player.healthUpdate(50);
			return;
		}
		if(itemName == "ManaPotion")
		{
			player.manaUpdate(50);
			return;
		}
	}

	// Diminue la quantité de l'item avec l'id en parametre de 1 (le supprime de l'inventaire si quantité = 0)
	// L'opération n'est effectué que si l'item existe
	void updateQuantity(int id)
	{
		for(int i = 0 ; i < listOfItem.Count ; i++)
		{
			if (listOfItem[i].getId() == id)
			{
				if (inventory.ContainsKey(listOfItem[i]))
				{
					inventory[listOfItem[i]]--;
					if (inventory[listOfItem[i]] <= 0)
						inventory.Remove(listOfItem[i]);
				}
				break;
			}
		}	
	}
	
	protected override void UpdateState()
	{
		base.UpdateState();
		GetComponent<PauseMenu>().enabled = !paused;
	}
}

public class Item
{
	private int id;
	private string name;
	//private Texture2D icon;
	private string resume;
	
	public Item(int in_id, string in_name, string in_resume)
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
