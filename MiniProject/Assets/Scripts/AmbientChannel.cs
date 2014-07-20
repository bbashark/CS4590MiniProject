using UnityEngine;
using System.Collections;

public class AmbientChannel : MonoBehaviour {

	public AudioClip ambientSound;
	public AlertManager alertManagerObject;
	public float annoyanceToPitchMultiple;

	// Use this for initialization
	void Start () {
		audio.clip = ambientSound;
		audio.loop = true;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		// get the annoyance (stress) from the AlertManager script.
		float stress = alertManagerObject.getAnnoyanceLevel();
		// calculate new pitch
		float pitch = 1.0f + stress * annoyanceToPitchMultiple;
		// don't let pitch get too crazy.
		if (pitch > 10.0f) {
			pitch = 10.0f;
		}

		audio.pitch = pitch;
	}
}
