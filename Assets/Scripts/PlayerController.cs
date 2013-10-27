using UnityEngine;
using System.Collections;

public class PlayerController : HumanoidController 
{
	public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
	public float rotationFactor = 5.0F;
    public float gravity = 20.0F;
	
    private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	
	private CharacterController controller;
	
	private int xp=0;
	
	// Use this for initialization
	void Start () 
	{
		gameObject.renderer.material.color = new Color(255, 0, 0);
		controller = GetComponent<CharacterController>();
		pvMax = 200;
		pv = pvMax;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pv <= 0) {
			return;
		}
		
        if (controller.isGrounded) 
		{
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
		
		// Rotation
		rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
		rotation *= rotationFactor;
		transform.Rotate(rotation);
		
		if (Input.GetButton("Fire1"))
		{
			EnemyController[] target = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
			for (int i=0; i<5; i++)
			{
				Vector3 distance = transform.position-target[i].transform.position;
				if(distance.magnitude <= 4f)
				{
					target[i].healthUpdate(-1);
					return;
				}	
			}
		}
	}
	
	
	
	void OnTriggerEnter (Collider other)
	{
		// Collects items
		if (other.gameObject.tag == "Item")
		{
			other.gameObject.SetActive(false);
			healthUpdate(50);
		}
		if (other.gameObject.tag == "Weapon")
		{
			other.gameObject.SetActive(false);
			healthUpdate(-50);
			experienceUpdate(40);
		}
	}
	
	public override void healthUpdate(int change)
	{
		base.healthUpdate(change);
	}
	
	private void experienceUpdate(int change)
	{
		xp += change;
	}
	
	public int getExperience()
	{
		return xp;
	}
}
