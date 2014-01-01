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
		base.Start();
		timeCredit = 0;
		currentText = 0;
		intervalTime = 15f;
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
			case "txt_Coord":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Coordinateurs du projet: \n Guillaume Pascal \n Romain Seyer";
				break;
			case "txt_Dev":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Developpeurs : \n Geoffrey Clauzier \n Christophe Gaillard";
				break;
			case "txt_Dev2":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Clement Janisset \n Arnaud Moncel";
				break;
			case "txt_Dev3":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Guillaume Pascal  \n Alexandre Raberin";
				break;
			case "txt_DevPonct":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Avec la participation ponctuelle de : \n Elodie Estivale \n Volodia Mandaud";
				break;
			case "txt_Mode":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Graphistes : \n Thibaut Grandclement \n Romain Seyer";
				break;
			case "txt_ModePonct":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Avec la participation ponctuelle de : \n Thibault Bertrand \n Gregory Gounon";
				break;
			case "txt_ModePonct2":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Alexis Masclaux \n Bastien Mogeot";
				break;
			case "txt_ModePonct3":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Leo Solvignon \n Damien Valla";
				break;
			case "txt_Musique":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Musique : \n Dawid Jaworski \n Plastic3";
				break;
			case "txt_Musique2":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Koke Nez Gmez \n Iska";
				break;
			case "txt_Bruitage":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Bruitage : \n universal-soundbank.com \n sound-fishing.net \n Clement Janisset";
				break;
			case "txt_Scenario":
				curText = listCredit[i].GetComponent<TextMesh>();
				curText.text = "Scenario : ";
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
		Debug.Log (listCredit [currentText].name.ToString ());
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
