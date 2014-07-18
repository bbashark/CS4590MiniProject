using UnityEngine;
using System.Collections;

public class SendMessage : MonoBehaviour {

	public AudioClip listen;
	public AudioClip success;
	public AudioClip cancel;

	private bool listening = false; // Is Glass now listening for inputs
	private bool spoke = false;

	private float timer = 0;
	private float cool = .3F;

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
		if (timer == 0){
			if (!listening && Input.GetKeyDown(KeyCode.G)){
				listening = true;
				audio.PlayOneShot(listen);
				timer = cool;
			}
			else if (listening && Input.GetKeyUp(KeyCode.G)){
				listening = false;
				if (spoke){
					audio.PlayOneShot(success);
					timer = cool;
				}
				else{
					audio.PlayOneShot(cancel);
					timer = cool;
				}
				spoke = false;
			}
		}
		if (listening && Input.GetKeyDown (KeyCode.F)) {
			spoke = true;
		}
	}
}
