using UnityEngine;
using System.Collections;

public abstract class Alert: MonoBehaviour {

	public bool randomAlert;
	public AudioClip activateClip, resolveClip;
	private bool destroyObject = false;
	public AlertManager aManager;
	
	public virtual void ActivateAlert(){
		audio.PlayOneShot (activateClip);
		Debug.Log ("play one shot");
	}

	public virtual void ResolveAlert(){
		audio.PlayOneShot (resolveClip);
		aManager.setOffRandomAlert = true;
		Debug.Log ("alert resolved");
	}

}
