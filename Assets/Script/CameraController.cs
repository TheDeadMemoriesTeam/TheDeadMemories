using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject target;
	private Vector3 offset;
	
	// Mouvement du perso avec la souris
	private float mouseX;
	private float mouseY;
	public float mouseSpeed = 10f;
	
	// Use this for initialization
	void Start () {
		offset = transform.position;
		// Initialisation de la position de la souris
		Input.mousePosition.Set(0,0,0);
		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = target.transform.position + offset;
		
		handleMouseRotation();
	}
	
	
	void handleMouseRotation()
	{
		float positionX = Input.mousePosition.x;
		float positionY = Input.mousePosition.y;
		float deltaX, deltaY;
		if (positionX != mouseX)
		{
			deltaX = (positionX - mouseX) * mouseSpeed * Time.deltaTime;
			mouseX = positionX;
		}
		else
			deltaX = 0;
		
		
		if (positionY != mouseY)
		{
			deltaY = (positionY - mouseY) * mouseSpeed * Time.deltaTime;
			mouseY = positionY;
		}
		else
			deltaY = 0;
		
		this.transform.Rotate(-deltaY, deltaX, 0);
	}
}
