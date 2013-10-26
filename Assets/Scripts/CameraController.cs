using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	
	public GameObject target;

	public float distance = 5F;
	public float height = 2.5F;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		Vector3 offset = new Vector3(0F, height, -distance);
		transform.position = target.transform.position + target.transform.TransformDirection(offset);
		transform.LookAt(target.transform.position);
	}
}
