using UnityEngine;
using System.Collections;

public class ReceiveNotice : MonoBehaviour {

	public AudioClip superiorSound;
	public AudioClip subordinateSound;
	private AudioClip[][] superiorAnnoyanceSounds;
	public AudioClip[] superiorLowAnnoyanceSounds;
	public AudioClip[] superiorMedAnnoyanceSounds;
	public AudioClip[] superiorHighAnnoyanceSounds;
	private AudioClip[][] subordinateAnnoyanceSounds;
	public AudioClip[] subordinateLowAnnoyanceSounds;
	public AudioClip[] subordinateMedAnnoyanceSounds;
	public AudioClip[] subordinateHighAnnoyanceSounds;
	public bool play = false;
	public int annoyanceLevel = 0;
	public bool superior = true;
	public float rankMod = 1;

	// Use this for initialization
	void Start () {
		superiorAnnoyanceSounds = new AudioClip[][] {superiorLowAnnoyanceSounds, superiorMedAnnoyanceSounds, superiorHighAnnoyanceSounds};
		subordinateAnnoyanceSounds = new AudioClip[][] {subordinateLowAnnoyanceSounds, subordinateMedAnnoyanceSounds, subordinateHighAnnoyanceSounds};
	}
	
	// Update is called once per frame
	void Update () {
		if (play) {
			play = false;
			playSound();
		}
	}

	private void playSound() {
		audio.pitch = rankMod;
		Debug.Log (audio.pitch);
		if (superior) {
			StartCoroutine(AlertPlay(superiorSound, superiorAnnoyanceSounds));
		}
		else{
			StartCoroutine(AlertPlay(subordinateSound, subordinateAnnoyanceSounds));
		}
		//audio.pitch = 1;
	}

	IEnumerator AlertPlay(AudioClip alert, AudioClip[][] annoyanceClips) {
		audio.PlayOneShot (alert);
		yield return new WaitForSeconds (alert.length);
		if (annoyanceLevel > annoyanceClips.Length) {
						annoyanceLevel = annoyanceClips.Length;
		}
		AudioClip clip = 
		audio.clip = annoyanceClips [annoyanceLevel][Random.Range(0, annoyanceClips[annoyanceLevel].Length)];
		audio.pitch = 1;
		audio.volume = 1;
		audio.Play ();
		yield return new WaitForSeconds( clip.length );
	}
}
