using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneration : MonoBehaviour {
	private Mesh mesh;
	[SerializeField]
	private Material material;
	private GameObject meshGenerator;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
		Vector3[] vertices = {
      new Vector3(-1f, -1f, 0),
      new Vector3(-1f, 1f, 0),
      new Vector3(1f, 1f, 0),
      new Vector3(1f, -1f, 0)
	  };
		int[] triangles = {
			0, 1, 2,
			0, 2, 3
		};
		mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		meshGenerator = GameObject.Find("MeshGenerator");
		meshFilter = meshGenerator.GetComponent<MeshFilter>();
		if (!meshFilter) {
			meshFilter = meshGenerator.AddComponent<MeshFilter>();
		}
		meshRenderer = meshGenerator.GetComponent<MeshRenderer>();
		if (!meshRenderer) {
			meshRenderer = meshGenerator.AddComponent<MeshRenderer>();
		}
		meshFilter.mesh = mesh;
		meshRenderer.sharedMaterial = material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
