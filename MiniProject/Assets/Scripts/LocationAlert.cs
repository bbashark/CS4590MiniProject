using UnityEngine;
using System.Collections;

public class LocationAlert : Alert {

	public AudioClip locationFindingClip;
	private Vector3 desiredLocation;
	private GameObject positionCollider;
	private GameObject player;
	private bool active = false;

	public override void ActivateAlert() {
		active = true;
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
		active = false;
		base.ResolveAlert ();
		Debug.Log ("child of alert resolved");
		GameObject.Destroy (positionCollider);
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		randomAlert = true;
	}

	void Update() {
		if(active) {
			//run location finding clip with pitch changing based on distance
			float xDist = Mathf.Abs(positionCollider.transform.position.x - player.transform.position.x);
			xDist *= xDist;
			float zDist = Mathf.Abs(positionCollider.transform.position.z - player.transform.position.z);
			zDist *= zDist;
			float dist = Mathf.Sqrt(xDist + zDist);

			Debug.Log ("distance: "+dist);
		}
	}

}
