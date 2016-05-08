using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private float timeMax = 0.55f;
	private float currentTime = 0f;

	private bool isInside = false;

	private bool isWon = false;

	private GameObject eyeClose;

	private int counter=1;

	public bool isTutorial = false;

	// Use this for initialization
	void Start () {
		eyeClose = gameObject.transform.FindChild ("GoalClose").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isWon) {
			if (isInside) {
				currentTime += Time.deltaTime;
			}

			if (currentTime >= timeMax) {
				Win ();
			}
		} else {
			CloseTheEyeAnimation();
		}

		if (Input.GetKeyDown ("space")) {
			Win ();
		}
	}

	void CloseTheEyeAnimation() {
		Vector3 size = eyeClose.transform.localScale;
		if (size.y <= 1f) {
			size.y += 1f * Time.deltaTime;
			eyeClose.transform.localScale = size;
		}
	}

	void Win() {
		GetComponent<AudioSource> ().Play ();
		isWon = true;

		// deactivate distractions when player is in wait mode to load next level
		transform.parent.gameObject.SendMessage ("DeactivateDistractions");
		// tell the player to wait (close the eyes)
		GameObject.Find ("Player").SendMessage ("WaitForNextLevel");

		if (isTutorial)
			GameObject.Find ("Relax-Image").GetComponent<SpriteRenderer> ().enabled = true;

		// change the index of the level, but still don't show it
		LoadNewLevel ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Player") {
			isInside = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.tag == "Player") {
			isInside = false;
			//currentTime = 0f;
			isWon = false;
		}
	}


	void LoadNewLevel() {
		string currentLevelString = transform.parent.gameObject.name.Replace ("Level-", "");
		int currentLevel = int.Parse (currentLevelString);
		int newLevel = currentLevel + 1;
		GameObject.Find("GameController").SendMessage("UpdateLevelIndex", newLevel);
	}
}
