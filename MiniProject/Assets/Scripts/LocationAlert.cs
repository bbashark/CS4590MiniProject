using UnityEngine;
using System.Collections;

public class LocationAlert : Alert {

	public AudioClip locationFindingClip;
	private Vector3 desiredLocation;
	private GameObject positionCollider;

	public void ActivateAlert() {
		//replace with raycast from player's click to geo, record vector 3
		desiredLocation = new Vector3 (0f, 1f, -247f);

		positionCollider = new GameObject ();
		positionCollider.transform.position = desiredLocation;
		positionCollider.AddComponent<BoxCollider> ();


	}

	public void ResolveAlert() {
		GameObject.Destroy (positionCollider);
	}

	// Use this for initialization
	void Start () {
		randomAlert = true;
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject == positionCollider) {
			ResolveAlert();
		}
	}

	void Update() {
		//run location finding clip with pitch changing based on distance
	}

}
