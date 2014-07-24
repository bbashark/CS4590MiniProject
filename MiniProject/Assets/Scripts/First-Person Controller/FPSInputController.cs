using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//adaptation of included Unity JavaScript component in C# by xadhoom
//reference: http://forum.unity3d.com/threads/64378-CharacterMotor-FPSInputController-PlatformInputController-in-C

// Require a character controller to be attached to the same game object
[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/FPS Input Controller")]

public class FPSInputController : MonoBehaviour
{
	private bool parented = false;
    private CharacterMotor motor;

    // Use this for initialization
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input vector from kayboard or analog stick
        Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (directionVector != Vector3.zero)
        {
            // Get the length of the directon vector and then normalize it
            // Dividing by the length is cheaper than normalizing when we already have the length anyway
            float directionLength = directionVector.magnitude;
            directionVector = directionVector / directionLength;

            // Make sure the length is no bigger than 1
            directionLength = Mathf.Min(1.0f, directionLength);

            // Make the input vector more sensitive towards the extremes and less sensitive in the middle
            // This makes it easier to control slow speeds when using analog sticks
            directionLength = directionLength * directionLength;

            // Multiply the normalized direction vector by the modified length
            directionVector = directionVector * directionLength;
        }

        // Apply the direction to the CharacterMotor
        motor.inputMoveDirection = transform.rotation * directionVector;
        motor.inputJump = Input.GetButton("Jump");
    }

	void OnControllerColliderHit(ControllerColliderHit collider){
		//Debug.Log ("controller collider hit" + collider.gameObject.name);
	}

	void OnTriggerStay(Collider collider){
		if(collider.gameObject.tag.Equals("Grabbable")) {
			if(Input.GetKeyDown("q")) {
				Debug.Log("grabbed camera");
				if(parented) {
					collider.transform.parent = transform.parent.parent;
					collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y - 1.0f, collider.transform.position.z);
					parented = false;
				} else {
					collider.transform.parent = transform;
					collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y + 1.0f, collider.transform.position.z);
					parented = true;
					collider.collider.isTrigger = true;
				}
			}
		}
	}
}