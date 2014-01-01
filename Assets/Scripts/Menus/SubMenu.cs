using UnityEngine;
using System.Collections;

public class SubMenu : MonoBehaviour 
{
	// Caméra du menu
	protected CameraMenu cam;

	// Caméra en face du menu ou non
	protected bool inFrontOf = false;

	// Use this for initialization
	protected virtual void Start () 
	{
		cam = FindObjectOfType<CameraMenu>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public virtual void setInfFrontOf(bool state)
	{
		inFrontOf = state;
	}
}
