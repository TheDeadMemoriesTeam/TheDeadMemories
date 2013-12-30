using UnityEngine;
using System.Collections;

public class Utils
{

	static public Vector3 GetInfiniteVector3()
	{
		return new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
	}

	static public float CalculateNavmeshPathLength (Vector3 startPosition, Vector3 targetPosition)
	{
		// Create a path and set it based on start and target positions.
		NavMeshPath path = new NavMeshPath();
		NavMesh.CalculatePath(startPosition, targetPosition, -1, path);
		
		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
		
		// The first point is the enemy's position.
		allWayPoints[0] = startPosition;
		
		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		
		// The points inbetween are the corners of the path.
		for(int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}
		
		// Create a float to store the path length that is by default 0.
		float pathLength = 0;
		
		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for(int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		
		return pathLength;
	}
}
