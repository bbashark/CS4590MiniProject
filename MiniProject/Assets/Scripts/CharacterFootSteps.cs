using UnityEngine;
using System.Collections;

public class CharacterFootSteps : MonoBehaviour {

//	Sound info
	float timer = 0;
	float cool = .5F;

//	Tile info
	private bool onTile = true;
	public AudioClip[] stepSounds;

	private CharacterMotor chMotor;
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		chMotor = GameObject.FindWithTag("Player").GetComponent<CharacterMotor>();
		controller = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.isGrounded){
//			if (controller.velocity.magnitude > 0 && Input.GetKey(KeyCode.LeftShift)) {
//				chMotor.movement.maxForwardSpeed = 10;
//				chMotor.movement.maxSidewaysSpeed = 10;
//			} else {
//				chMotor.movement.maxForwardSpeed = 5;
//				chMotor.movement.maxSidewaysSpeed = 5;
//			}
			if (controller.velocity.magnitude > 0) {
//				if (Input.GetKey (KeyCode.LeftShift)){
//					cool = .25F;
//				}
//				else{
				cool = .5F;
//				}
				if (onTile){
					tileSound();
				}
			}
		}
		
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		if (timer < 0) {
			timer = 0;
		}
	}
	
	void tileSound(){
		if (timer == 0) {
			audio.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
			timer = cool;
		}
	}
}
