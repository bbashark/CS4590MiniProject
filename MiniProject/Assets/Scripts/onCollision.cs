using UnityEngine;
using System.Collections;

public class onCollision : MonoBehaviour {

	//	Sound info
	float timer = 0;
	public float cool = 0F;

	public AudioClip[] sounds;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		if (timer < 0) {
			timer = 0;
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		if (timer == 0) {
			audio.PlayOneShot (sounds [Random.Range (0, sounds.Length)]);
			timer = cool;
		}
	}

	void OnCharacterCollision(Vector3 velocity) {
		if (timer == 0 && !audio.isPlaying) {
			audio.PlayOneShot (sounds [Random.Range (0, sounds.Length)]);
			timer = cool;
		}
	}
}
