using UnityEngine;
using System.Collections;

public class ObjectRespawn : MonoBehaviour {

	private Vector3 startLocation;
	private Quaternion startRotation;

	public double time = 0.0;
	public double respawnTime;

	// Use this for initialization
	void Start () {
		startLocation = transform.position;
		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if(time > respawnTime){
			transform.position = startLocation;
			transform.rotation = startRotation;

			transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

			time = 0.0;
		}
	}
}
