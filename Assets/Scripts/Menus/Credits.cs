using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Credits : SubMenu 
{
	//Timer du credit pour afficher des credit a interval de temps régulié.
	float timeCredit;
	
	//Intervale de temps entre deux credits
	float intervalTime;
	
	//Numero du texte en cours d'affichage
	int currentText;
	
	//Liste des Texte 3D des credits
	public GameObject[] listCredit;
	
	public bool isReturn = false;
	
	// Use this for initialization
	protected override void Start () 
	{
		timeCredit = 0;
		currentText = 0;
		intervalTime = 4f;
		initText ();
		listCredit [currentText].renderer.enabled = true;
		Color couleur = new Color(1f, 1f, 1f, 0f);
		listCredit [currentText].renderer.material.color = couleur;
	}
	
	void initText()
	{
		TextMesh curText;
		for(int i=0; i<listCredit.Length; i++)
		{
			switch(listCredit[i].name.ToString())
			{
			case "txt_Credit":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Credit";
				break;
			case "txt_Dev":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Developpeur: \n BA";
				break;
			case "txt_Mode":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Modeleur";
				break;
			case "txt_Musique":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Musique";
				break;
			case "txt_Bruitage":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Bruitage";
				break;
			case "txt_Scenario":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Scenario";
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		if (!inFrontOf)
		{
			return;
		}
		manageText ();
		
		Debug.Log("dessine credits");
	}
	
	//Gere s'il faut changer de texte ou non
	void manageText()
	{
		if(timeCredit > intervalTime)
		{
			timeCredit = 0;
			changeText();
		}
		timeCredit += Time.deltaTime;
		Color couleur = new Color(1f, 1f, 1f, (Mathf.Sin((timeCredit/intervalTime)*Mathf.PI)));
		listCredit [currentText].renderer.material.color = couleur;
	}
	
	//Change le texte en cours d'affichage
	void changeText()
	{
		listCredit [currentText].renderer.enabled = false;
		if(currentText >= listCredit.Length-1)
		{
		}
		else
		{
			currentText += 1;
			listCredit [currentText].renderer.enabled = true;
		}
		
	}
	
	//Redemarre les credits a zero
	protected void restartCred()
	{
		currentText = 0;
		timeCredit = 0;
	}
}
