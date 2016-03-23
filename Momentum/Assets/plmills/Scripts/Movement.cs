using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour {

	public float walkingSpeed;

	Vector3 movement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/**
		float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * walkingSpeed * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		transform.position = transform.position + movement;
		*/

		if (Input.GetKey(KeyCode.W)){
			transform.position = transform.position + Camera.main.transform.forward * walkingSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.S)){
			transform.position = transform.position + Camera.main.transform.forward * -1 * walkingSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.D)){
			transform.position = transform.position + Camera.main.transform.right * walkingSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.A)){
			transform.position = transform.position + Camera.main.transform.right * -1 * walkingSpeed * Time.deltaTime;
		}

		//transform.Translate(Vector3.forward * Time.deltaTime);

	}
}
