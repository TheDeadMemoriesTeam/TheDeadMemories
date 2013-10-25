using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject target;
	public Vector3 offset;
	
	// Mouvement du personnage avec la souris
	private float mouseX;
	public float mouseSpeed = 10f;
	
	// Use this for initialization
	void Start () {
		// Initialise la position de la caméra
		transform.position = target.transform.position + offset;
		// Initialise l'angle de la caméra
		this.transform.rotation.Set(25,2,0,1);
		// Initialise la rotation du personnage
		mouseX = Input.mousePosition.x;
	}
	
	// Update is called once per frame
	void Update () {
		// Mise à jour de la position de la caméra
		transform.position = target.transform.position + offset;
		transform.LookAt(target.transform);
		handleMouseRotation();
	}
	
	// Change la position de la caméra en fonction
	// des mouvements de la souris
	void handleMouseRotation()
	{
		float positionX = Input.mousePosition.x;
		float deltaX;
		if (positionX != mouseX)
		{
			deltaX = (positionX - mouseX) * mouseSpeed * Time.deltaTime;
			mouseX = positionX;
		}
		else
			deltaX = 0;
		// Mise à jour de la rotation du personnage
		target.gameObject.transform.Rotate(0, deltaX, 0);
	}
}
