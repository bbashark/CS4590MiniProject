using UnityEngine;
using System.Collections;

public class ReceiveNotice : MonoBehaviour {

	public AudioClip superiorSound;
	public AudioClip subordinateSound;
	public AudioClip[] annoyanceClips;
	public bool play = false;
	public int annoyanceLevel = 0;
	public bool superior = true;
	public float rankMod = 1;

	// Use this for initialization
	void Start () {
	
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
			StartCoroutine(AlertPlay(superiorSound));
		}
		else{
			StartCoroutine(AlertPlay(subordinateSound));
		}
		//audio.pitch = 1;
	}

	IEnumerator AlertPlay(AudioClip alert) {
		audio.PlayOneShot (alert);
		yield return new WaitForSeconds (alert.length);
		if (annoyanceLevel > annoyanceClips.Length) {
						annoyanceLevel = annoyanceClips.Length;
		}
		audio.clip = annoyanceClips [annoyanceLevel];
		audio.pitch = 1;
		audio.volume = 1;
		audio.Play ();
		yield return new WaitForSeconds( annoyanceClips[annoyanceLevel].length );
	}
}
