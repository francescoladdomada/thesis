using UnityEngine;
using System.Collections;

public class MovingBird : MonoBehaviour {

	private SpriteRenderer sr;

	private AudioSource audio;

	private bool isActivated = false;

	public float moveDirectionX;
	public float moveDirectionY;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		audio = GetComponent<AudioSource> ();

		if (sr != null)
			sr.enabled = false;

		startPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActivated) {
			Vector3 newPos = transform.localPosition;
			newPos.x = newPos.x + moveDirectionX * Time.deltaTime;
			newPos.y = newPos.y + moveDirectionY * Time.deltaTime;
			transform.localPosition = newPos;
		}

	}

	public void Activate() {
		isActivated = true;
		sr.enabled = true;
		PlayAudio ();
	}

	public void Deactivate()
	{
		isActivated = false;
		sr.enabled = false;
		transform.localPosition = startPosition;
	}

	public void PlayAudio() {
		audio.Play ();
	}
}
