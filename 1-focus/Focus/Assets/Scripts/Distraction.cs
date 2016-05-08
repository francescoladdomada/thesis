using UnityEngine;
using System.Collections;

public class Distraction : MonoBehaviour {

	private bool isActivated = false;

	private bool isAnimationCalled = false;

	private SpriteRenderer sr;

	private Animation anim;

	private AudioSource audio;

	private ParticleSystem ps;

	public bool mustPlayOnce = false;
	private bool playedOnce = false;

	//public bool mustMove = false;
	//public float moveDirectionX;
	//public float moveDirectionY;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();

		audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animation> ();
		ps = GetComponent<ParticleSystem> ();

		if(sr!=null)
			sr.enabled = false;

		if(ps!=null)
			ps.enableEmission = false;
		
		if(anim!=null)
			anim.Stop ();

		startPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActivated/* && !isAnimationCalled*/) {
			/*
			if (mustMove) {
				Vector3 newPos = transform.localPosition;
				newPos.x = newPos.x + moveDirectionX * Time.deltaTime;
				newPos.y = newPos.y + moveDirectionY * Time.deltaTime;
				transform.localPosition = newPos;
				print (newPos);
			}*/

			if(anim!=null)
				anim.Play ();


			if (ps != null) {
				ps.enableEmission = true;

				if(!ps.isPlaying)
					ps.Play ();
			}

			if (audio != null && !audio.isPlaying) {
				if (mustPlayOnce) {
					if (!playedOnce) {
						PlayAudio ();
						playedOnce = true;
					}
				} else {
					PlayAudio ();
				}


			}
			
			sr.enabled = true;
			//isAnimationCalled = true;
		}
        else
        {
			//transform.localPosition = startPosition;
			if(anim!=null)
	            anim.Stop();

			if (ps != null) {
				ps.Stop ();
				ps.enableEmission = false;

			}

			if (audio != null)
				audio.Stop ();

            sr.enabled = false;

			playedOnce = false;
        }
	}

	public void Activate() {
		isActivated = true;
	}

    public void Deactivate()
    {
        isActivated = false;
    }

    public void PlayAudio() {
		audio.Play ();
	}
}
