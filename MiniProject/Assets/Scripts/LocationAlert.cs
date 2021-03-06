﻿using UnityEngine;
using System.Collections;

public class LocationAlert : Alert {

	public AudioClip locationFindingClip, moveCamVerbalClip;
	private Vector3 desiredLocation;
	private GameObject positionCollider;
	private GameObject player;
	private bool active = false;
	private float pauseGap, startPause, startPauseTime, pause;

	public override void ActivateAlert() {
		startPauseTime = Time.time;
		active = true;
		//Debug.Log ("also do new activate stuff");
		base.ActivateAlert ();
		StartCoroutine ("PlayVoiceMessage");
		//replace with raycast from player's click to geo, record vector 3
		desiredLocation = new Vector3 (0f, 1f, -247f);

		positionCollider = new GameObject ();
		positionCollider.transform.position = desiredLocation;
		positionCollider.transform.parent = this.gameObject.transform;
		positionCollider.AddComponent<BoxCollider> ();
		positionCollider.AddComponent<AudioSource> ();
		positionCollider.GetComponent<BoxCollider> ().isTrigger = true;


	}

	public override void ResolveAlert() {
		audio.pitch = 1;
		active = false;
		base.ResolveAlert ();
		//Debug.Log ("child of alert resolved");
		GameObject.Destroy (positionCollider);
	}

	// Use this for initialization
	void Start () {
		startPause = activateClip.length + moveCamVerbalClip.length;
		pauseGap = 1.0f;
		player = GameObject.FindGameObjectWithTag("Player");
		randomAlert = true;
	}

	void Update() {
		if(active && Time.time - startPauseTime > startPause) {
			//run location finding clip with pitch changing based on distance
			float xDist = Mathf.Abs(positionCollider.transform.position.x - player.transform.position.x);
			xDist *= xDist;
			float zDist = Mathf.Abs(positionCollider.transform.position.z - player.transform.position.z);
			zDist *= zDist;
			float dist = Mathf.Sqrt(xDist + zDist);
			float distPitch = 2 - (dist * 1f/50f);
			pauseGap = (dist * 1f/50f) + 0.3f;

			if(!audio.isPlaying && pause > pauseGap) {
				pause = 0;
				positionCollider.audio.clip = locationFindingClip;
				positionCollider.audio.pitch = distPitch;
				positionCollider.audio.Play();
			} else if(!audio.isPlaying){
				pause += 0.1f;
			}

			//Debug.Log ("pause: "+ pauseGap);
		}
	}

	IEnumerator PlayVoiceMessage() {
		yield return new WaitForSeconds(activateClip.length);
		audio.clip = moveCamVerbalClip;
		audio.Play ();
	}

}
