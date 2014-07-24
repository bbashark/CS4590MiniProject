using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Collider nonTriggercollider;

	void OnTriggerEnter(Collider collision){
		Debug.Log ("entered desired position");
		if(collision.transform.parent.GetComponent<Alert>() != null) {
			collision.transform.parent.GetComponent<Alert>().ResolveAlert();
		}
	}

	void OnCollisionEnter(Collision collision){

	}

}
