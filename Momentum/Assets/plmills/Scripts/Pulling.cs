using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

//Does more than pulling, test script
public class Pulling : MonoBehaviour {

	public float pushForce;

	public float pullForce;

	public float jumpForce;

	public float jumpRayLength;

	public void Update()
	{
		//this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 20, 0));

		if(CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			Vector3 down = transform.TransformDirection(Vector3.down);

			if (Physics.Raycast(transform.position, down, jumpRayLength))
			{
				this.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}

		if (CrossPlatformInputManager.GetButtonDown("Fire1"))
		{
			
			RaycastHit hit;
			Vector3 end = GetComponentInChildren<Camera>().transform.TransformDirection(Vector3.forward);
			//Vector3 start = (transform.GetComponentInChildren<Transform>().position - transform.position) + transform.position;
			//Vector3 start = transform.position;

			Vector3 start = GetComponentInChildren<Camera>().transform.position;

			//end = end.normalized; //This one
			//start = start.normalized;

			//start.y = end.y;

			print(start);
			print(end);

			//print("Player position: " + transform.position);
			//print("Test position: " + start);

			if (Physics.Raycast(start, end, out hit))
			{

				//Debug.DrawRay(transform.position, MoveForward, Color.green);

				//Debug.DrawLine(start, end, Color.red);


				if(hit.collider.tag == "RigidbodyObject" || hit.collider.tag == "Player"){
					Vector3 value = (hit.transform.position - this.transform.position);

					hit.transform.GetComponent<PhotonView>().RPC("applyForce", PhotonTargets.All, value * pushForce);
				}

			}

		}

		if(CrossPlatformInputManager.GetButtonDown("Fire2"))
		{


			RaycastHit hit;
			Vector3 end = GetComponentInChildren<Camera>().transform.TransformDirection(Vector3.forward);
			//Vector3 start = (transform.GetComponentInChildren<Transform>().position - transform.position) + transform.position;
			//Vector3 start = transform.position;
			end = end.normalized;
			//start = start.normalized;

			Vector3 start = GetComponentInChildren<Camera>().transform.position;

			//print("Player position: " + transform.position);
			//print("Test position: " + start);

			if (Physics.Raycast(start, end, out hit))
			{

				//Debug.DrawRay(transform.position, MoveForward, Color.green);

				//Debug.DrawLine(start, end, Color.red);

				Vector3 value = (hit.transform.position - this.transform.position);

				//print(value);

				this.transform.GetComponent<Rigidbody>().AddForce(value * pullForce); //ForceMode.Impulse);

			}


		}
	}
}
