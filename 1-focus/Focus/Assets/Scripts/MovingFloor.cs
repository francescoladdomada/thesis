using UnityEngine;
using System.Collections;

public class MovingFloor : MonoBehaviour {

	private Vector3 startPosition;

	private Animation anim;

	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;

		anim = GetComponent<Animation> ();

		StopAnimation ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		StartAnimation ();

	}

	void OnTriggerExit2D(Collider2D coll) {

		if (coll.tag == "Player") {
			StopAnimation ();
			BackToStartPosition ();

		}
	}

	public void BackToStartPosition() {
		transform.localPosition = startPosition;
	}

	public void StartAnimation() {

		if (anim!=null) {
			anim.Rewind ();
			anim.Play ();
		}
	}

	public void StopAnimation() {

		if (anim!=null) {
			anim.Stop ();
			anim.Rewind ();
		}
	}
}
