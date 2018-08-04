using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperation : MonoBehaviour {
	[SerializeField]
	private float speed;
	private float angle = 1f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// 移動 W:前 S:後 A:左 D:右 Space:上 LeftShift:下
		if (Input.GetKey(KeyCode.W)) {
			transform.position += transform.forward * speed;
		}
		if (Input.GetKey(KeyCode.S)) {
			transform.position -= transform.forward * speed;
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.position -= transform.right * speed;
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.position += transform.right * speed;
		}
		if (Input.GetKey(KeyCode.Space)) {
			transform.position += transform.up * (speed / 2);
		}
		if (Input.GetKey(KeyCode.LeftShift)) {
			transform.position -= transform.up * (speed / 2);
		}
		// 回転 矢印キー左右
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(0, -angle, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(0, angle, 0);
		}
	}
}
