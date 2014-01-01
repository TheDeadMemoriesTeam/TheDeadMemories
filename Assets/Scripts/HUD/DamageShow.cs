using UnityEngine;
using System.Collections.Generic;

public class DamageShow : MonoBehaviour 
{
	// Numéro du dégat enregistré
	static int guiTextId = 0;

	// Hiérachie dans laquelle va se placer les GUIText
	private Transform hierarchy;

	// Liste des éléments à afficher
	List<GUIText> listOfElementToShow;

	// Détermine si l'élément précédent sera affiché à droite ou à gauche
	private bool isRight = true;

	//public int nbSimultaneousElement;

	// Use this for initialization
	void Start () 
	{
		listOfElementToShow = new List<GUIText>();
		hierarchy = GameObject.Find("DamageGUIText").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (listOfElementToShow.Count == 0)
			return;


		for (int i = 0 ; i < listOfElementToShow.Count ; i++)
			Debug.Log(listOfElementToShow[i].text);
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
			txt.pixelOffset = new Vector2(Screen.width/2 - 50, Screen.height/2 - 50);
		else
			txt.pixelOffset = new Vector2(Screen.width/2 + 50, Screen.height/2 - 50);
		isRight = !isRight;

		// Détermine la couleur du texte (perte ou gain de vie)
		if (damage >= 0)
			txt.color = Color.green;
		else
			txt.color = Color.red;

		listOfElementToShow.Add(txt);
	}

	// Réalise une troncature à nbDecimal après la virgule 
	float truncateValue(float value, int nbDecimal)
	{
		float power = Mathf.Pow(10, nbDecimal);
		int temp = (int)(value*power);
		return temp / power;
	}
}
