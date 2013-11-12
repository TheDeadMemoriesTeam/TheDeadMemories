using UnityEngine;
using System.Collections;

public class CameraMenu : CameraPath
{
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();
	}

	
	public void goToMainMenu()
	{
		float startX = transform.position.x;
		float startZ = transform.position.z;
		float endX = 20f;
		float endZ = 5f;
		path.Add(new Vector3(startX + (endX - startX)/4, 3.5f, startZ + (endZ - startZ)/4));
		path.Add(new Vector3(startX + (endX - startX)/2, 5f, startZ + (endZ - startZ)/2));
		path.Add(new Vector3(startX + (endX - startX)*3/4, 3.5f, startZ + (endZ - startZ)*3/4));
		path.Add(new Vector3(endX, 1.5f, endZ));
		//20 1.5 5
	//25.4 1.5 19.5
	}
	
	public void goToLaunchMenu()
	{
		float startX = transform.position.x;
		float startZ = transform.position.z;
		float endX = 25.4f;
		float endZ = 19.5f;
		path.Add(new Vector3(startX + (endX - startX)/4, 3.5f, startZ + (endZ - startZ)/4));
		path.Add(new Vector3(startX + (endX - startX)/2, 5f, startZ + (endZ - startZ)/2));
		path.Add(new Vector3(startX + (endX - startX)*3/4, 3.5f, startZ + (endZ - startZ)*3/4));
		path.Add(new Vector3(endX, 1.5f, endZ));
	}
}