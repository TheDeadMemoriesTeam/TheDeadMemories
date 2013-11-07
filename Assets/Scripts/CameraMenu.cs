using UnityEngine;
using System.Collections;

public class CameraMenu : MonoBehaviour 
{
	public float smooth = 0.1f;
	
	private Vector3 dest;
	// Use this for initialization
	void Start () 
	{
		dest = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, dest, smooth * Time.deltaTime);
	}

	
	public void goToMainMenu()
	{
		dest = new Vector3(20f, 1.5f, 5f);
	}
	
	public void goToLaunchMenu()
	{
		dest = new Vector3(25.4f, 1.5f, 19.5f);
	}
}