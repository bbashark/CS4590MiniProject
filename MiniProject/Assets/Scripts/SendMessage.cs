using UnityEngine;
using System.Collections;

public class SendMessage : MonoBehaviour {

	public AudioClip listen;
	public AudioClip success;
	public AudioClip cancel;

	private bool listening = false; // Is Glass now listening for inputs
	private bool spoke = false;
	private bool chimed = false;

	private float timer = 0;
	private float coolStart = .45F;
	private float coolStop = .3F;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		glassCommSounds ();
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		if (timer < 0) {
			timer = 0;
		}


	}

	private void glassCommSounds() {
		if (timer == 0 && listening && Input.GetKeyDown (KeyCode.F)) {
			spoke = true;
		}
		if (timer == 0 && chimed && !listening) {
			if (spoke){
				audio.PlayOneShot(success);
				spoke = false;
			}
			else{
				audio.PlayOneShot(cancel);
			}
			timer = coolStop;
			chimed = false;
		}
		if (Input.GetKeyDown(KeyCode.G)){
			listening = true;
			if (timer == 0){
				audio.PlayOneShot(listen);
				timer = coolStart;
				chimed = true;
			}
		}
		if (listening && Input.GetKeyUp (KeyCode.G)) {
			listening = false;
		}
	}
}
