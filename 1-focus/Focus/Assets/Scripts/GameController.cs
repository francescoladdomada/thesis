using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	int startLevel = 1;
	int currentLevel = 1;
	int finalLevel = 7;

	// Use this for initialization
	void Start () {
		for(int i=startLevel;i<=finalLevel;i++) {
			GameObject.Find ("Level-" + i).SendMessage ("Deactivate");
		}
		GameObject.Find ("Level-" + startLevel).SendMessage ("Activate");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateLevelIndex(int i) {
		currentLevel = i;
	}

	void ShowNewLevel() {
		int oldLevel = currentLevel - 1;

		GameObject.Find("Level-"+oldLevel).SendMessage("Deactivate");
		GameObject.Find("Level-"+currentLevel).SendMessage("Activate");
	}

	void ResetPlayerStartPosition() {
		GameObject.Find ("Player").SendMessage ("ResetStartPosition", currentLevel);
	}

	public int GetCurrentLevel() {
		return currentLevel;
	}

	public Vector3 GetCurrentLevelStartPosition() {
		return GameObject.Find ("Level-" + currentLevel).transform.FindChild ("StartPosition").transform.position;
	}
}
