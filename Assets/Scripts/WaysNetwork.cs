using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class WaysNetwork : MonoBehaviour {

	public Transform[] headArc;
	public Transform[] tailArc;

	private Transform[] nodes;

	private Dictionary<Vector3, Dictionary<Vector3, float>> weightedAdjacencyList;


	// Use this for initialization
	void Start () {
		nodes = gameObject.GetComponentsInChildren<Transform>();
		List<Transform> nodesList = nodes.ToList();
		nodesList.Remove(transform);
		nodes = nodesList.ToArray();

		if (nodes.Length < 2)
			Debug.Log ("WaysNetwork: Less that two nodes where detected. This can lead to expected behaviours.");

		// Check attributs droppableItems and itemsDropProbability
		bool validAttributs = true;
		if (headArc.Length != tailArc.Length) // Same size
			validAttributs = false;
		else
			for (int i = 0; i < headArc.Length; i++)
				if (headArc[i] == tailArc[i] /*|| headArc[i].parent != this || tailArc[i].parent != this*/) // Valid values
					validAttributs = false;
		if (!validAttributs) {
			Debug.LogError("The WaysNetwork has its arcs that are not correctly configured.");
			Application.Quit();
		}

		generateAdjacencyList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void generateAdjacencyList()
	{
		weightedAdjacencyList = new Dictionary<Vector3, Dictionary<Vector3, float>>();
		for (int i = 0; i < nodes.Length; i++)
		{
			weightedAdjacencyList.Add(nodes[i].position, new Dictionary<Vector3, float>());
			for (int j = 0; j < headArc.Length; j++) {
				if (headArc[j] == nodes[i]) {
					float weight = Utils.CalculateNavmeshPathLength(nodes[i].position, tailArc[j].position);
					weightedAdjacencyList[nodes[i].position].Add(tailArc[j].position, weight);
				}
			}
		}
	}

	public Vector3[] getShortestPath(Vector3 from, Vector3 to)
	{
		if (!weightedAdjacencyList.ContainsKey(from) || !weightedAdjacencyList.ContainsKey(to))
			return new Vector3[0];

		// Init everything
		Dictionary<Vector3, float> d = new Dictionary<Vector3, float>();
		Dictionary<Vector3, Vector3?> pi = new Dictionary<Vector3, Vector3?>();

		foreach(KeyValuePair<Vector3, Dictionary<Vector3, float>> entry in weightedAdjacencyList)
		{
			d.Add(entry.Key, float.PositiveInfinity);
			pi.Add(entry.Key, null);
		}
		d[from] = 0;

		// Start Dijkstra
		//List<Vector3> E = new List<Vector3>();
		List<Vector3> F = new List<Vector3>();
		foreach(KeyValuePair<Vector3, Dictionary<Vector3, float>> entry in weightedAdjacencyList)
		{
			F.Add(entry.Key);
		}

		while (F.Count > 0)
		{
			// Extract the node with the minimal distance
			Vector3 u = F.First();
			float dmin = float.PositiveInfinity;
			F.Sort ( (i1, i2) => ((d[i1] < d[i2]) ? -1 : ((d[i1] == d[i2]) ? 0 : 1)) );
			u = F[0];
			F.RemoveAt(0);

			//E.Add(u);
			foreach(KeyValuePair<Vector3, float> entry in weightedAdjacencyList[u])
			{
				float alt = d[u] + entry.Value;
				//Debug.Log ("..." + d[entry.Key] + "<>" + alt + "(" + d[u] + "--" + entry.Value + ")");
				if (d[entry.Key] > alt) {
					//Debug.Log ("update");
					d[entry.Key] = alt;
					pi[entry.Key] = u;
				}
			}
		}

		// Get the sortest path to the destination
		List<Vector3> path = new List<Vector3>();
		if (pi[to] != null)
		{
			path.Insert(0, to);
			Vector3 u = to;
			while (pi[u] != null)
			{
				path.Insert(0, pi[u] ?? default(Vector3));
				u = pi[u] ?? default(Vector3);
			}
		}
		if (path.Count < 1 ||path.First() != from || path.Last() != to) {
			Debug.Log("WaysNetwork: Can not find a path between two nodes. "
			          + "You should add some arcs to avoid this. "
			          + "(Position of the affected WaysNetwork: " + transform.position.ToString() + ")");
		}
		Debug.Log (path.Count);
		return path.ToArray();
	}

	public int getNbNodes()
	{
		return nodes.Length;
	}

	public Transform getRandomNode()
	{
		return nodes[Random.Range(0, nodes.Length-1)];
	}

	public Transform getNearestNode(Vector3 position)
	{
		Transform nearestNode = null;
		float minDistance = float.PositiveInfinity;
		for (int i = 0; i < nodes.Length; i++)
		{
			float d = Vector3.Distance(position, nodes[i].position);
			if (d < minDistance) {
				minDistance = d;
				nearestNode = nodes[i];
			}
		}
		
		return nearestNode;
	}
}
