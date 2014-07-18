using UnityEngine;
using System.Collections;

public class ReceiveNotice : MonoBehaviour {

	public AudioClip superiorSound;
	public AudioClip subordinateSound;
	public bool play = false;
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
		if (superior) {
			audio.PlayOneShot(superiorSound);
		}
		else{
			audio.PlayOneShot(subordinateSound);
		}
		audio.pitch = 1;
	}
}
