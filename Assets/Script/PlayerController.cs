using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed, jumpHeight;
	
	private bool isJumping;
	
	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = new Color(255, 0, 0);
		isJumping=false;
	}
	
	// Update is called once per frame
	void Update () {
		/* Sauts */
			// esquive avant
		if (Input.GetKey(KeyCode.Space) && !isJumping)
		{
			if (Input.GetKey(KeyCode.Z) || Input.GetKey (KeyCode.UpArrow))
			{
				rigidbody.AddForce((Vector3.up * jumpHeight + Vector3.forward * moveSpeed)/2);
				isJumping = true;
			}
				// esquive arriere
			else if (Input.GetKey(KeyCode.S) || Input.GetKey (KeyCode.DownArrow))
			{
				rigidbody.AddForce(Vector3.up * jumpHeight - Vector3.forward * moveSpeed /2);
				isJumping = true;
			}
				// esquive gauche
			else if (Input.GetKey(KeyCode.Q) || Input.GetKey (KeyCode.LeftArrow))
			{
				rigidbody.AddForce(Vector3.up * jumpHeight + Vector3.left * moveSpeed /2);
				isJumping = true;
			}
				// esquive droite
			else if (Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
			{
				rigidbody.AddForce(Vector3.up * jumpHeight + Vector3.right * moveSpeed /2);
				isJumping = true;
			}
				// sauter
			else
			{
				rigidbody.AddForce(Vector3.up * jumpHeight);
				isJumping = true;
			}
		}
		
		/* Déplacements*/
		else if (Input.GetKey(KeyCode.Z) || Input.GetKey (KeyCode.UpArrow))
		{
			// avant
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				rigidbody.AddForce(new Vector3((float)1,0,(float)0.5) * moveSpeed * Time.deltaTime);
			// avant
			else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
				rigidbody.AddForce(new Vector3((float)-1,0,(float)0.5) * moveSpeed * Time.deltaTime);
			// avant
			else
				rigidbody.AddForce(Vector3.forward * moveSpeed * Time.deltaTime);
		}
		
		else if (Input.GetKey(KeyCode.S) || Input.GetKey (KeyCode.DownArrow))
		{
			// arriere droite
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				rigidbody.AddForce(new Vector3((float)1,0,(float)-0.5) * moveSpeed * Time.deltaTime);
			// arriere gauche
			else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
				rigidbody.AddForce(new Vector3((float)-1,0,(float)-0.5) * moveSpeed * Time.deltaTime);
			// arriere
			else
				rigidbody.AddForce(Vector3.back * moveSpeed * Time.deltaTime);
		}
		
		else if (Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
			rigidbody.AddForce(Vector3.right * moveSpeed * Time.deltaTime);		
		else if (Input.GetKey(KeyCode.Q) || Input.GetKey (KeyCode.LeftArrow))
			rigidbody.AddForce(Vector3.left * moveSpeed * Time.deltaTime);
	}
	
	void OnTriggerEnter (Collider other)
	{
		// Effet vache qui rit
		// -> other.gameObject.collider.gameObject.collider.gameObject
		if (other.gameObject.tag == "Item")
		{
			other.gameObject.SetActive(false);
		}
		if (other.gameObject.tag == "Ground")
		{
			isJumping = false;
		}
	}
}
