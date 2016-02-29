using UnityEngine;
using System.Collections;

public class mindmillRotate : MonoBehaviour {

	public float rotationSpeed = 14;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
	}
}
