using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

	public float direction_x;
	public float direction_y;
	public float speed;

	private Vector3 start_position;

	private float x_limit = 1f;
	private float y_limit = 1f;

	private float x_left;
	private float x_right;

	private float y_left;
	private float y_right;


	private string current_direction = "left";

	// Use this for initialization
	void Start () {
		start_position = transform.localPosition;

		x_left = start_position.x - x_limit;
		x_right = start_position.x + x_limit;

		y_left = start_position.y - y_limit;
		y_right = start_position.y + y_limit;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		Vector3 reachPoint = transform.localPosition;
		if (transform.localPosition.x <= x_left) {
			current_direction = "right";
		}

		if (transform.localPosition.x >= x_right) {
			current_direction = "left";
		}

		if (current_direction == "right") {
			reachPoint.x = x_right;
			reachPoint.y = y_right;
		} else {
			reachPoint.x = x_left;
			reachPoint.y = y_left;
		}
		transform.localPosition = Vector3.MoveTowards (transform.localPosition, reachPoint, speed * Time.deltaTime);
	*/
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.name == "Player") {
			coll.gameObject.SendMessage ("Die");
		}
	}
}
