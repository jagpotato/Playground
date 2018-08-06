using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour {
	[Range(1, 255)]
	 public int size;
	 public float vertexDistance = 1f;
	 public float heightMultiplier = 1f;
	 public Material material;
	 public PhysicMaterial physicMaterial;
	// Use this for initialization
	void Start () {
		Vector3[] vertices = new Vector3[size * size];
		float y;
		for (int z = 0; z < size; z++) {
			for (int x = 0; x < size; x++) {
				y = Random.value * heightMultiplier;
				vertices[z * size + x] = new Vector3(x * vertexDistance, y, -z * vertexDistance);
			}
		}
		int triangleIndex = 0;
		int[] triangles = new int[(size - 1) * (size - 1) * 6];
		for (int z = 0; z < size - 1; z++) {
			for (int x = 0; x < size - 1; x++) {
				int a = z * size + x;
				int b = a + 1;
				int c = a + size;
				int d = c + 1;
				triangles[triangleIndex] = a;
				triangles[triangleIndex + 1] = b;
				triangles[triangleIndex + 2] = c;

				triangles[triangleIndex + 3] = c;
				triangles[triangleIndex + 4] = b;
				triangles[triangleIndex + 5] = d;

				triangleIndex += 6;
			}
		}
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		GameObject ground = GameObject.Find("Ground");
		ground = GameObject.Find("Ground");
		MeshFilter meshFilter = ground.GetComponent<MeshFilter>();
		if (!meshFilter) {
			meshFilter = ground.AddComponent<MeshFilter>();
		}
		MeshRenderer meshRenderer = ground.GetComponent<MeshRenderer>();
		if (!meshRenderer) {
			meshRenderer = ground.AddComponent<MeshRenderer>();
		}
		MeshCollider meshCollider = ground.GetComponent<MeshCollider>();
		if (!meshCollider) {
			meshCollider = ground.AddComponent<MeshCollider>();
		}
		meshFilter.mesh = mesh;
		meshRenderer.sharedMaterial = material;
		meshCollider.sharedMesh = mesh;
		meshCollider.sharedMaterial = physicMaterial;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
