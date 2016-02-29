using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerExit2D(Collider2D coll) {
		if (coll.tag == "Player") {
			coll.gameObject.SendMessage ("Die");

            GameObject[] distractors = GameObject.FindGameObjectsWithTag("Distraction");
            foreach (GameObject distractor in distractors)
            {
                distractor.SendMessage("Deactivate");
            } 
		
		}
	}

}
