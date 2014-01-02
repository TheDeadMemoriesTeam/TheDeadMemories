using UnityEngine;
using System.Collections.Generic;

public class DamageShow : MonoBehaviour 
{
	// Numéro du dégat enregistré
	static int guiTextId = 0;

	// Hiérachie dans laquelle va se placer les GUIText
	private Transform hierarchy;

	// Joueur
	private PlayerController player;

	// Liste des éléments à afficher
	List<GUIText> listOfElementToShow;

	// Détermine si l'élément précédent sera affiché à droite ou à gauche
	private bool isRight = true;
	// Moitié du nombre de pixel sur lesquels les texte vont descendre
	private int traveling = 50;
	// Décalage des files de dégats par rapport au centre de l'écran (en X)
	private int offSetFromCenter = 50;
	// Vitesse de défilement des texte (en pixel par frame)
	private int speed = 1;
	// taille de la Police utilisée
	private int policeSize = 20;

	//public int nbSimultaneousElement;

	// Use this for initialization
	void Start () 
	{
		listOfElementToShow = new List<GUIText>();
		hierarchy = GameObject.Find("DamageGUIText").transform;
		player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (listOfElementToShow.Count == 0 || player.getPause())
			return;

		// Met à jour la position des éléments affichés
		for (int i = 0 ; i < listOfElementToShow.Count ; i++)
		{
			// Déplace le texte
			Vector2 currentPos = listOfElementToShow[i].pixelOffset;
			listOfElementToShow[i].pixelOffset = new Vector2(currentPos.x, currentPos.y-speed);

			// Applique un fondu sur le texte
			Color currentColor = listOfElementToShow[i].color;
			listOfElementToShow[i].color = new Color(currentColor.r,
			                                         currentColor.g,
			                                         currentColor.b,
			                                         determineAlpha(currentPos.y));

			// Supprime l'élément lorsque il a dépassé un certain seuil de l'écran
			if (listOfElementToShow[i].pixelOffset.y < Screen.height/2 - traveling)
			{
				DestroyImmediate(listOfElementToShow[i].gameObject);
				listOfElementToShow.RemoveAt(i);
			}
		}
	}

	public void addElementToDisplay(float damage)
	{
		// Ajoute le dégat pris à la liste des éléments (tronqué)
		GameObject objectText = new GameObject("Damage" + guiTextId);
		objectText.transform.parent = hierarchy;
		guiTextId++;
		GUIText txt = (GUIText)objectText.AddComponent(typeof(GUIText));
		txt.text = truncateValue(damage, 1).ToString();

		// Détermine la position du texte
		if (isRight)
			txt.pixelOffset = new Vector2(Screen.width/2 - offSetFromCenter, Screen.height/2 + traveling);
		else
			txt.pixelOffset = new Vector2(Screen.width/2 + offSetFromCenter, Screen.height/2 + traveling);
		isRight = !isRight;

		// Détermine la couleur du texte (perte ou gain de vie)
		if (damage >= 0)
			txt.color = Color.green;
		else
			txt.color = Color.red;

		txt.fontSize = policeSize;

		listOfElementToShow.Add(txt);
	}

	// Réalise une troncature à nbDecimal après la virgule 
	float truncateValue(float value, int nbDecimal)
	{
		float power = Mathf.Pow(10, nbDecimal);
		int temp = (int)(value*power);
		return temp / power;
	}

	float determineAlpha(float pos)
	{
		int sup = Screen.height/2 + traveling;
		int descent = (int)(sup - pos);
		return (1 - ((float)descent/(2*traveling)) * 1);
	}
}
