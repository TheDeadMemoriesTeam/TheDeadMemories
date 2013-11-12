using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraPath : MonoBehaviour 
{
	public float smooth = 0.2f;
	
	protected List<Vector3> path; // Path to follow
	
	private Vector3 lastPosition; // Start position of the last path step.
	private Vector3 dest; // Intermediate destination in the path
	private SpeedPolicy speedPolicy; // Speed policy for the current path step.
	private int lastPathSize;
	private float time; // Start time of the current path step
	
	enum SpeedPolicy {
		SpeedUp,
		Constant,
		SlowDown
	};
	
	// Use this for initialization
	public virtual void Start () 
	{
		Time.timeScale = 1;
		
		path = new List<Vector3>();
		speedPolicy = SpeedPolicy.SpeedUp;
		lastPathSize = 0;
		time = 0;
		
		dest = transform.position;
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		
		if (Vector3.Distance(transform.position, dest) < 0.5 && path.Count > 0)
		{
			if (path.Count == 1)
				speedPolicy = SpeedPolicy.SlowDown;
			else if (path.Count > lastPathSize)
				speedPolicy = SpeedPolicy.SpeedUp;
			else
				speedPolicy = SpeedPolicy.Constant;
			
			Debug.Log (speedPolicy);
			
			lastPosition = transform.position;
			dest = path[0];
			path.RemoveAt(0);
			time = 0;
			
			lastPathSize = path.Count;
		}
		
		time += Time.deltaTime;
		
		if (speedPolicy == SpeedPolicy.SpeedUp) {
			transform.position = Vector3.Lerp(transform.position, dest, smooth/4f * time);
		}
		else if (speedPolicy == SpeedPolicy.Constant) {
			transform.position = Vector3.Lerp(lastPosition, dest, smooth * time);
		}
		else {
			transform.position = Vector3.Lerp(transform.position, dest, smooth * Time.deltaTime);
		}
		
	}
	
//  Travail en cours
//	public void goTo(float endX, float endZ)
//	{
//		
//		const float nbSteps = 6;
//		const float Ymax = 5f;
//		
//		float startX = transform.position.x;
//		float startZ = transform.position.z;
//		path.Add(new Vector3(startX + (endX - startX)/3, 4.5f, startZ + (endZ - startZ)/3));
//		path.Add(new Vector3(startX + (endX - startX)/2, 5f, startZ + (endZ - startZ)/2));
//		path.Add(new Vector3(startX + (endX - startX)*2/3, 4.5f, startZ + (endZ - startZ)*2/3));
//		path.Add(new Vector3(endX, 1.5f, endZ));
//	}

}