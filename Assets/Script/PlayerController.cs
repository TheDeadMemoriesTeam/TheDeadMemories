using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
	
    private Vector3 moveDirection = Vector3.zero;
	
	private CharacterController controller;
	
	// Mouvement du perso avec la souris
	private float mouseX;
	public float mouseSpeed = 10f;
	
	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = new Color(255, 0, 0);
		controller = GetComponent<CharacterController>();
		
		// Initialisation de la position de la souris
		Input.mousePosition.Set(0,0,0);
		mouseX = Input.mousePosition.x;
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
		//handleMouseRotation();
	}
	
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
		
		this.transform.Rotate(0, 0, deltaX);
		
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
