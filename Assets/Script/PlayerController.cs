using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed, turnSpeed, jumpHeight;
	
	private bool isJumping;
	
	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = new Color(255, 0, 0);
		isJumping=false;
	}
	
	// Update is called once per frame
	void Update () {
		// Si le perso doit avancer
		if (Input.GetKey(KeyCode.Z) || Input.GetKey (KeyCode.UpArrow))
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		// Si le perso doit reculer
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
		// Si le perso doit tourner à gauche
		else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.up * -turnSpeed * Time.deltaTime);
		// Si le perso doit tourner à droite
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
		// Si le perso doit sauter
		else if (Input.GetKey(KeyCode.Space) && !isJumping)
		{
			rigidbody.AddForce(Vector3.up * jumpHeight);
			isJumping = true;
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		// Effet vache qui rit
		// -> other.gameObject.collider.gameObject.collider.gameObject
		if (other.gameObject.tag == "Item")
		{
			other.gameObject.SetActive(false);
		}
		//if (other.gameObject.tag == "Ground")
		//{
			isJumping = false;
		//}
	}
}
