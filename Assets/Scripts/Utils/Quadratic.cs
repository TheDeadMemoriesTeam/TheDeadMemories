using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quadratic {

	public float a;
	public float b;
	public float c;
	
	
	// Apply the function to x
	public float getValueFor(float x)
	{
		return a*x*x + b*x + c;
	}
	
	// Apply the inverse of the function on y 
	public List<float> getArgOf(float y)
	{
		float c1 = c - y;
		float delta = b*b - 4*a*c1;
		List<float> res = new List<float>();
		
		if (delta > 0)
		{
			res.Add((-b-Mathf.Sqrt(delta))/(2*a));
			res.Add((-b+Mathf.Sqrt(delta))/(2*a));
		}
		else if (delta == 0)
		{
			res.Add (-b/(2*a));
		}
		return res;
	}
	
	// Return the vertex of the parabola
	// (The vertex of a parabole is "the top" of the parabola.
	public Vector2 getVertex()
	{
		float x = -b/(2*a);
		return new Vector2(x, getValueFor(x));
	}
	
	public bool isAPositive()
	{
		return a > 0;
	}
}
