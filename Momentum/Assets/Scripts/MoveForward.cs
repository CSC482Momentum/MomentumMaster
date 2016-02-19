using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

    public Vector3 movement;
    public Vector3 direction;
    public int strength;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(movement * Time.deltaTime);
	}

    void OnTriggerEnter(Collider col)
    {
        col.GetComponent<Rigidbody>().AddForce(direction * strength);
    }
}
