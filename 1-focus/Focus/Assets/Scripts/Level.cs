using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activate() {
		GetComponent<SpriteRenderer> ().enabled = true;
		Vector3 newPos = transform.position;
		newPos.x = 0;
		transform.position = newPos;
		ActivateChild ("StartPosition");
		ActivateChild ("Goal");
		ActivateChild ("Walls");
		ActivateChild ("Distractions");

		GameObject.Find ("Relax-Image").GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void Deactivate() {
		GetComponent<SpriteRenderer> ().enabled = false;
		DeactivateChild ("StartPosition");
		DeactivateChild ("Goal");
		DeactivateChild ("Walls");
		DeactivateChild ("Distractions");
	}

	public void ActivateSlow() {
		Invoke ("Activate", 2f);
	}

	public void DeactivateSlow() {
		Invoke ("Deactivate", 2f);
	}

	public void DeactivateDistractions() {
		DeactivateChild ("Distractions");

	}

	private void DeactivateChild(string childName) {
		transform.FindChild (childName).gameObject.SetActive (false);
	}

	private void ActivateChild(string childName) {
		transform.FindChild (childName).gameObject.SetActive (true);
	}
}
