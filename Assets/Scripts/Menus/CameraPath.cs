using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraPath : MonoBehaviour 
{
	
	private Quadratic parabolaXYCoef;
	private Quadratic parabolaZYCoef;
	private Vector3 dest;
	private Vector3 velocity;
	
	
	// Use this for initialization
	public virtual void Start () 
	{
		Time.timeScale = 1;
		
		parabolaXYCoef = new Quadratic();
		parabolaZYCoef = new Quadratic();
		velocity = Vector3.zero;
		dest = transform.position;
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		if (dest != transform.position)
			getNextPosition();
	}
	

	public void goTo(Vector3 d)
	{
		goTo (d.x, d.y, d.z);
	}

	public void goTo(float endX, float endY, float endZ)
	{
		float height = 3.5f; // Parabola vertex height (vertex is the point at the top)
		
		// Init points that will define the parabola
		Vector3 pos1 = transform.position; // Start
		Vector3 pos2 = new Vector3((pos1.x+endX)/2, pos1.y+height, (pos1.z+endZ)/2); // Top middle
		Vector3 pos3 = new Vector3(endX, endY, endZ); // End
		dest = pos3;
		
		// Parabola: y = ax² + bx + c
		
		// Project on X-axis and Y-axis
		// Determine a, b and c
		Matrix mat = new Matrix(4, 3);
		mat[0, 0] = pos1.x * pos1.x; mat[1, 0] = pos1.x; mat[2, 0] = 1; mat[3, 0] = pos1.y;
		mat[0, 1] = pos2.x * pos2.x; mat[1, 1] = pos2.x; mat[2, 1] = 1; mat[3, 1] = pos2.y;
		mat[0, 2] = pos3.x * pos3.x; mat[1, 2] = pos3.x; mat[2, 2] = 1; mat[3, 2] = pos3.y;
		
		Matrix reduced = mat.reduce();
		parabolaXYCoef.a = reduced[3, 0];
		parabolaXYCoef.b = reduced[3, 1];
		parabolaXYCoef.c = reduced[3, 2];
		
		// Project on Z-axis and Y-axis
		// Determine a, b and c
		mat = new Matrix(4, 3);
		mat[0, 0] = pos1.z * pos1.z; mat[1, 0] = pos1.z; mat[2, 0] = 1; mat[3, 0] = pos1.y;
		mat[0, 1] = pos2.z * pos2.z; mat[1, 1] = pos2.z; mat[2, 1] = 1; mat[3, 1] = pos2.y;
		mat[0, 2] = pos3.z * pos3.z; mat[1, 2] = pos3.z; mat[2, 2] = 1; mat[3, 2] = pos3.y;
		
		reduced = mat.reduce();
		parabolaZYCoef.a = reduced[3, 0];
		parabolaZYCoef.b = reduced[3, 1];
		parabolaZYCoef.c = reduced[3, 2];
	}
	
	public void getNextPosition()
	{
		float normalizedStep = 8.5f;
		float step = normalizedStep * Time.deltaTime;
		
		// Init nextPos to the current position
		Vector3 nextPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		// Determine X and Y value for next position
		Vector3 directionVect = (dest - nextPos).normalized;
		float angle = Vector3.Angle(directionVect, Vector3.right)/360*2*Mathf.PI;
		if (Vector3.Angle (directionVect, Vector3.forward) > 90f)
			angle = -angle;
		nextPos.x = nextPos.x + step * Mathf.Cos(angle);
		nextPos.y = parabolaXYCoef.getValueFor(nextPos.x);
		nextPos.z = nextPos.z + step * Mathf.Sin(angle);
		/*
		// Validate the Y value, to have at least one result for the Z component below.
		float vertY = parabolaZYCoef.getVertex().y;
		bool isAPositive = parabolaXYCoef.isAPositive();
		if ((isAPositive && vertY > nextPos.y) || (!isAPositive && vertY < nextPos.y))
		{
			nextPos.y = parabolaZYCoef.getVertex().y;
		}
		
		// Determine the Z value for the next position
		List<float> res = parabolaZYCoef.getArgOf(nextPos.y);
		if (res.Count == 2) {
			nextPos.z = res[0];
			Vector3 vec1 = nextPos;
			nextPos.z = res[1];
			Vector3 vec2 = nextPos;
			// If we are far of the top vertex, choose the nearest vector
			if ((vec1 - vec2).magnitude > 2*step)
			{
				if ((transform.position - vec1).magnitude < (transform.position - vec2).magnitude)
					nextPos.z = res[0];
				else
					nextPos.z = res[1];
			}
			// Else, if we are close to the top vertex
			else {
				// Determine the result that give to (nextPos - transform.position) a similar direction with velocity.
				float dot0 = Vector3.Angle(vec1 - transform.position, velocity);
				float dot1 = Vector3.Angle(vec2 - transform.position, velocity);
				if (dot0 < dot1)
					nextPos.z = res[0];
				else
					nextPos.z = res[1];
			}
		}
		else if (res.Count == 1) {
			nextPos.z = res[0];
		}
		else {
			throw new System.Exception("CameraPath.getNextPosition: Invalid result of parabolaZYCoef.getArgOf.");
		}*/
		
		// Update the current position
		//if ((nextPos - transform.position).magnitude > (dest - transform.position).magnitude)
		if ( Mathf.Sign(nextPos.x - dest.x) != Mathf.Sign(transform.position.x - dest.x) )
		{
			velocity = Vector3.zero;
			transform.position = nextPos = dest;
		}
		else
		{
			velocity = (nextPos - transform.position)/Time.deltaTime;
			transform.position = nextPos;
		}
	}

	public bool isArrived()
	{
		return (dest == transform.position);
	}

}