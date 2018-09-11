using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public GameObject sphere1, sphere2, sphere3;
	public float speed;
	private int time = 0;
	private float angle = 0f;
	private float radius = 10f;
	private Vector3 pos1, pos2, pos3;
	void Start () {
		pos1 = Vector3.zero;
		pos2 = Vector3.zero;
		pos3 = Vector3.zero;
	}
	
	void Update () {
		// if (time < 100) {
		// 	sphere1.transform.position += (transform.up + transform.right) * speed;
		// } else if (time == 100) {
		// 	sphere1.transform.position = new Vector3(0, 0, 0);
		// }
		// time++;
		pos1.x = Mathf.Sin(angle) * radius;
		pos1.z = Mathf.Cos(angle) * radius;
		pos1.y += 0.001f;
		sphere1.transform.position = pos1;

		pos2.x = Mathf.Sin(angle - 0.3f) * radius;
		pos2.z = Mathf.Cos(angle - 0.3f) * radius;
		pos2.y = pos1.y - 0.1f;
		sphere2.transform.position = pos2;

		pos3.x = Mathf.Sin(angle - 0.6f) * radius;
		pos3.z = Mathf.Cos(angle - 0.6f) * radius;
		pos3.y = pos2.y - 0.1f;
		sphere3.transform.position = pos3;

		angle += 0.03f;
		// if (time > 10) {
		// 	pos2.x = Mathf.Sin(Time.time / 10 * speed * 10) * 10f;
		//   pos2.z = Mathf.Cos(Time.time / 10 * speed * 10) * 10f;
		//   pos2.y += 0.01f;
		// 	sphere2.transform.position = pos2;
		// }
	}
}
