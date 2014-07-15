using UnityEngine;
using System.Collections;

public class LocationAlert : Alert {

	public AudioClip locationFindingClip;
	private Vector3 desiredLocation;
	private GameObject positionCollider;

	public override void ActivateAlert() {
		Debug.Log ("also do new activate stuff");
		base.ActivateAlert ();
		//replace with raycast from player's click to geo, record vector 3
		desiredLocation = new Vector3 (0f, 1f, -247f);

		positionCollider = new GameObject ();
		positionCollider.transform.position = desiredLocation;
		positionCollider.transform.parent = this.gameObject.transform;
		positionCollider.AddComponent<BoxCollider> ();
		positionCollider.GetComponent<BoxCollider> ().isTrigger = true;


	}

	public override void ResolveAlert() {
		base.ResolveAlert ();
		Debug.Log ("child of alert resolved");
		GameObject.Destroy (positionCollider);
	}

	// Use this for initialization
	void Start () {
		randomAlert = true;
	}

	void Update() {
		//run location finding clip with pitch changing based on distance
	}

}
