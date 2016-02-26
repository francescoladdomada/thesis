﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private float speed = 1f;

	private SpriteRenderer sRenderer;

	private AudioSource[] sounds;

	private bool canMove = false;

	private GameObject gaze;

	private bool isDying = false;

	private GameController gameController;

    private int currentLevel;

	private bool isWaitingForNextLevel = false;

    private GazePointDataComponent _gazePointDataComponent;
    private UserPresenceComponent _userPresenceComponent;


    private string operatingSystem;

	// Use this for initialization
	void Start () {
		sRenderer = GetComponent<SpriteRenderer> ();

		sounds = GetComponents<AudioSource> ();

		gaze = GameObject.Find ("Gaze");

		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();

		operatingSystem = DetectOS ();

		if (operatingSystem == "WIN") {
			_gazePointDataComponent = GetComponent<GazePointDataComponent>();
			_userPresenceComponent = GetComponent<UserPresenceComponent>();
		}



        currentLevel = gameController.GetCurrentLevel();

    }
	
	// Update is called once per frame
	void Update () {
		// die animation
		if (isDying) {
			Vector3 size = transform.localScale;
			size.x -= Time.deltaTime*2;
			size.y = size.x;
			if(size.x>0)
				transform.localScale = size;
		}

		// controls
		if (canMove) {
			if (!isWaitingForNextLevel)
				Move ();
			
			CloseTheEyes ();
		}
		// timer to start to move after dying
		else
			Invoke("StartToMove", 1f);


		ResetScene ();
	}

	void Move() {
		Vector3 cursorPosition = GetCursorPosition ();
        gaze.transform.position = Vector2.Lerp(transform.position, cursorPosition, speed*150*Time.deltaTime);
        transform.position = Vector2.Lerp(transform.position, gaze.transform.position, speed*Time.deltaTime);

        //float step = speed * Time.deltaTime;
        //gaze.transform.position = Vector3.MoveTowards(gaze.transform.position, cursorPosition, 50*step);
        //transform.position = Vector3.MoveTowards(transform.position, cursorPosition, 2*step);
    }

	void CloseTheEyes() {
		if (EyesAreClosed() && isWaitingForNextLevel) {
			isWaitingForNextLevel = false;
			gameController.SendMessage ("ShowNewLevel");
			RespawnFromStartPosition ();
		}
	}


	bool EyesAreClosed() {
		if(operatingSystem=="MAC")
			return Input.GetMouseButtonDown(0);
		else
			return !_userPresenceComponent.IsUserPresent;
	}

	Vector3 GetCursorPosition() {
		Vector3 mousePosition = new Vector3(0,0,0);
		if (operatingSystem == "MAC") {
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
            return mousePosition;
        } else {
            EyeXGazePoint p = _gazePointDataComponent.LastGazePoint;
            if (p.IsValid)
            {
                Vector3 pW = Camera.main.ScreenToWorldPoint(p.Screen);
                mousePosition = pW;
                mousePosition.z = 0;
                return mousePosition;
            }
		}

		return transform.position;
	}

	void Die() {
		StopMoving ();
		//sRenderer.enabled = false;
		PlayDieSound ();
		PlayParticle ();
		isDying = true;
		Invoke ("RespawnFromStartPosition", 0.9f);
	}

	void PlayParticle() {
		GameObject ps = (GameObject)Instantiate(Resources.Load ("Prefabs/DieParticle"), gameObject.transform.position, gameObject.transform.rotation);
		Destroy (ps, 0.7f);
	}

	void RespawnFromStartPosition() {
		StopMoving ();
		PlayRespawnSound ();

        currentLevel = gameController.GetCurrentLevel();

        transform.position = gameController.GetCurrentLevelStartPosition();
		transform.localScale = new Vector3 (1, 1, 1);
		sRenderer.enabled = true;
		isDying = false;

	}

	void StartToMove() {
		canMove = true;
	}

	void StopMoving() {
		canMove = false;
	}

	void PlayDieSound() {
		sounds [0].pitch = Random.Range (0.4f, 0.6f);
		sounds [0].Play ();
	}

	void PlayRespawnSound() {
		sounds [1].Play ();
	}

	void ResetStartPosition(int levelIndex) {
		transform.position = gameController.GetCurrentLevelStartPosition ();
		isWaitingForNextLevel = false;
	}

	void WaitForNextLevel() {
		isWaitingForNextLevel = true;
	}


	void ResetScene() {
		if (Input.GetKeyDown ("r")) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	public string DetectOS() {
		if (Application.platform == RuntimePlatform.OSXEditor
			|| Application.platform == RuntimePlatform.OSXPlayer
			|| Application.platform == RuntimePlatform.OSXWebPlayer)
			return "MAC";

		if (Application.platform == RuntimePlatform.WindowsEditor
			|| Application.platform == RuntimePlatform.WindowsPlayer
			|| Application.platform == RuntimePlatform.WindowsWebPlayer)
			return "WIN";

		return "";
	}
}