using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	void OnTriggerEnter(Collider collision){
		Debug.Log ("entered desired positoin");
		if(collision.transform.parent.GetComponent<Alert>() != null) {
			collision.transform.parent.GetComponent<Alert>().ResolveAlert();
		}
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("character controller collided with me");
	}
}
