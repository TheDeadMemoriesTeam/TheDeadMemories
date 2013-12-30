using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	// Caméra du menu
	protected CameraMenu cam;

	// Use this for initialization
	protected virtual void Start () 
	{
		cam = FindObjectOfType<CameraMenu>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnMouseEnter() 
	{
		renderer.material.color = Color.red;
	}
	
	void OnMouseExit() 
	{
		renderer.material.color = Color.white;
	}
}
