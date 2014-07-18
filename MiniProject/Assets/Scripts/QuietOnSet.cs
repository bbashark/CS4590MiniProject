using UnityEngine;
using System.Collections;

public class QuietOnSet : MonoBehaviour {
	public bool activate;
	private ArrayList objectsWithSound = new ArrayList();
	private bool rec = false;
	public float pauseTime;
	private float startTime;

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(activate){
			objectsWithSound.Add( GameObject.FindWithTag("PlaysAudio") );

			foreach(GameObject i in objectsWithSound) {
				StartCoroutine(FadeAudio(i));
			}
			activate = false;
			rec = true;
			startTime = Time.time;
		}

		if(rec){
			if(Time.time - startTime > pauseTime) {
				renderer.enabled = !renderer.enabled;
				startTime = Time.time;
			}

		}
	
	}

	IEnumerator FadeAudio(GameObject i){
		if(i.audio.volume > 0) {
			i.audio.volume -= 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
