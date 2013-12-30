using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	// Caméra du menu
	public CameraMenu cam;

	// Use this for initialization
	protected virtual void Start () 
	{
		if (cam == null)
			cam = FindObjectOfType<CameraMenu>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnMouseEnter() 
	{
		if (renderer != null)
			renderer.material.color = Color.red;
	}
	
	void OnMouseExit() 
	{
		if (renderer != null)
			renderer.material.color = Color.white;
	}
}
