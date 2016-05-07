using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject Player1;
	private GameObject Player2;


	// Use this for initialization
	void Start () {
		Player1 = GameObject.Find ("Player1");
		Player2 = GameObject.Find ("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 ((Player1.transform.position.x-Player2.transform.position.x)/2f+Player2.transform.position.x, 0, -10f);
	}
}
