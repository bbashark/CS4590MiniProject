using UnityEngine;
using System.Collections;

public class QuietOnSet : MonoBehaviour {
	public bool activate;
	private AudioSource[] objectsWithSound;
	private bool rec = false;
	public float pauseTime;
	private float startTime;
	public AudioClip superiorSound;
	private bool firstRun = true;
	public AudioClip quietVoiceClip;

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(activate){
			if(firstRun) {
				objectsWithSound = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];//.FindGameObjectsWithTag("PlaysAudio");
				firstRun = false;
				audio.volume = 0.8f;
				audio.PlayOneShot (superiorSound);
				StartCoroutine("PlayFullAlert");
			}

			foreach(AudioSource i in objectsWithSound) {
				StartCoroutine(FadeAudio(i));
			}
			activate = false;
			rec = true;
			startTime = Time.time;
		}

		if(rec){
			if(Time.time - startTime > pauseTime) {
				Debug.Log ("light blink");
				renderer.enabled = !renderer.enabled;
				startTime = Time.time;
			}

		}
	
	}

	IEnumerator FadeAudio(AudioSource i){
		yield return new WaitForSeconds (superiorSound.length);
		yield return new WaitForSeconds (superiorSound.length);
		yield return new WaitForSeconds(quietVoiceClip.length);
		if(i.audio.volume > 0) {
			i.audio.volume -= 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator PlayFullAlert() {
		yield return new WaitForSeconds (superiorSound.length);
		audio.PlayOneShot (superiorSound);
		yield return new WaitForSeconds (superiorSound.length);
		audio.clip = quietVoiceClip;
		audio.Play ();
		yield return new WaitForSeconds(quietVoiceClip.length);
	}
}
