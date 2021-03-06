﻿using UnityEngine;
using System.Collections;

public class onCollision : MonoBehaviour {

	//	Sound info
	float timer = 0;
	public float cool = 0F;

	public AudioClip[] sounds;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		if (timer < 0) {
			timer = 0;
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		Play ();
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Play ();
	}

	public void Play() {
		if (timer == 0) {
			if(GetComponent<AudioSource>() != null) {
				audio.PlayOneShot (sounds [Random.Range (0, sounds.Length)]);
				timer = cool;
			}
		}
	}
}
