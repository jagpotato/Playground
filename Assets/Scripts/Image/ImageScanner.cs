using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScanner : MonoBehaviour {
	private List<GameObject> scannerCube = new List<GameObject>();
  [SerializeField]
  private GameObject cube;
  [SerializeField]
  private int cubeNum;
  private Vector3 scannerPosition;
  private float end;
	[SerializeField]
  void Start () {
		Vector3 cubePosition = this.transform.position;
    for (int i = 0; i < cubeNum; i++) {
			scannerCube.Add(Instantiate(cube, cubePosition, Quaternion.identity, this.transform));
      cubePosition.x += cube.transform.localScale.x;
    }
    scannerPosition = this.transform.position;
    end = scannerPosition.y - (cubeNum - 1);
  }
  
  void Update () {
    if (scannerPosition.y >= end) {
      // scannerPosition.y -= 0.01f;
      // this.transform.position = scannerPosition;
    }
  }
}
