using UnityEngine;
using System.Collections;

public class AmbientChannel : MonoBehaviour {

	public AudioClip ambientSound;
	public AlertManager alertManagerObject;
	public float annoyanceToPitchMultiple;
	private float testPitch;

	// Use this for initialization
	void Start () {
		audio.clip = ambientSound;
		audio.loop = true;
		audio.Play();

		// test vars
		testPitch = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		// get the annoyance (stress) from the AlertManager script.
		float stress = alertManagerObject.getAnnoyanceLevel();
		// calculate new pitch
		float pitch = 1.0f + stress * annoyanceToPitchMultiple;
		// don't let pitch get too crazy.
		if (pitch > 5.0f) {
			pitch = 5.0f;
		}

		audio.pitch = pitch;
		//testPitch += Time.deltaTime;
		//audio.pitch = testPitch;
	}
}
