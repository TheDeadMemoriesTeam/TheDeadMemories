using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	//public float moveSpeed, jumpHeight;
	
	public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
	
    private Vector3 moveDirection = Vector3.zero;
	
	private CharacterController controller;
	
	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = new Color(255, 0, 0);
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (controller.isGrounded) {
			// Moves forward, left, right, backward
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
			// Handle jumps
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
            
        }
		// Applies move
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

	}
	
	void OnTriggerEnter (Collider other)
	{
		// Collects items
		if (other.gameObject.tag == "Item")
		{
			other.gameObject.SetActive(false);
		}
	}
}
