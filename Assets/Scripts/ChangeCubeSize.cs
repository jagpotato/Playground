using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCubeSize : MonoBehaviour {
	private float max, min;
	private bool increase = true;
	private float speed = 0.01f;
	// Use this for initialization
	void Start () {
		max = this.transform.localScale.y * 2;
		min = this.transform.localScale.y / 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (increase) {
			this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y + speed, this.transform.localScale.z);
		} else {
			this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - speed, this.transform.localScale.z);
		}
		if (this.transform.localScale.y >= max) {
			increase = false;
		}
		if (this.transform.localScale.y <= min) {
			increase = true;
		}
	}
}
