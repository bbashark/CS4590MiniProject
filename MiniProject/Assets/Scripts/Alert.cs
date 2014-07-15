using UnityEngine;
using System.Collections;

public abstract class Alert: MonoBehaviour {

	public bool randomAlert;
	public AudioClip activateClip, resolveClip;
	private bool destroyObject = false;
	
	public void ActivateAlert(){
		audio.PlayOneShot (activateClip);
	}

	public void ResolveAlert(){
		audio.PlayOneShot (resolveClip);
		destroyObject = true;
	}

	void Update() {
		if (destroyObject && !audio.isPlaying){
			GameObject.Destroy(this);
		}
	}

}
