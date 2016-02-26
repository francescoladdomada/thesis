﻿using UnityEngine;
using System.Collections;

public class Distraction : MonoBehaviour {

	private bool isActivated = false;

	private bool isAnimationCalled = false;

	private SpriteRenderer sr;

	private Animation anim;

	private AudioSource audio;

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();


		audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animation> ();
		ps = GetComponent<ParticleSystem> ();

		if(sr!=null)
			sr.enabled = false;
		
		if(anim!=null)
			anim.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActivated && !isAnimationCalled) {
			if(anim!=null)
				anim.Play ();

			if (ps != null) {
				if(!ps.isPlaying)
					ps.Play ();
				if(!audio.isPlaying)
					PlayAudio ();
			}
			
			sr.enabled = true;
			//isAnimationCalled = true;
		}
        else
        {
			if(anim!=null)
	            anim.Stop();
			
            sr.enabled = false;
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