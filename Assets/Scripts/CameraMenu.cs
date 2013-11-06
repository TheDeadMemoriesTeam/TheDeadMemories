using UnityEngine;
using System.Collections;

public class CameraMenu : MonoBehaviour 
{
	public Vector3 dest;
	// Use this for initialization
	void Start () 
	{
		dest = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 d;
		if(dest.magnitude > transform.position.magnitude)
		{
			d = transform.position - dest;
			if(d.magnitude > 0)
			{
				transform.position = new Vector3(transform.position.x<dest.x ? transform.position.x+0.1f : dest.x,
												1.5f,
												transform.position.z<dest.z ? transform.position.z+0.1f : dest.z);
			}
		}
		/*else
		{
			d = dest - transform.position;
			if(d.magnitude > 0)
			{
				transform.position = new Vector3(transform.position.x>dest.x ? transform.position.x-0.1f : dest.x,
												1.5f,
												transform.position.z>dest.z ? transform.position.z-0.1f : dest.z);
				
			}
		}*/
	}
}