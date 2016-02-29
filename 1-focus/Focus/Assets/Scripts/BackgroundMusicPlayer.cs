using UnityEngine;
using System.Collections;

public class BackgroundMusicPlayer : MonoBehaviour {

	void Awake() {
		//DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
