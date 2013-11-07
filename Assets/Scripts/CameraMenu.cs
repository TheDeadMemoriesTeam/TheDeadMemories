using UnityEngine;
using System.Collections;

public class CameraMenu : MonoBehaviour 
{
	public float smooth = 0.1f;
	
	private Vector3 dest;
	private Vector3 step;
	
	private float time;
	
	private bool translate;
	
	// Use this for initialization
	void Start () 
	{
		dest = transform.position;
		step = transform.position;
		
		time = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(translate)
		{
			time += Time.deltaTime;
			if(time>0.5)
			{
				transform.position = Vector3.Lerp(transform.position, dest, smooth * Time.deltaTime);
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, step, smooth * Time.deltaTime);
			} 
			if((int)transform.position.magnitude - (int)dest.magnitude == 0)
			{
				translate = false;
				time = 0;
			}
		}
	}

	
	public void goToMainMenu()
	{
		translate = true;
		step = new Vector3(22.7f, 5f, 12.25f);
		dest = new Vector3(20f, 1.5f, 5f);
		//20 1.5 5
	//25.4 1.5 19.5
	}
	
	public void goToLaunchMenu()
	{
		translate = true;
		step = new Vector3(22.7f, 5f, 12.25f);
		dest = new Vector3(25.4f, 1.5f, 19.5f);
	}
}