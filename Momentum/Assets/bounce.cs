using UnityEngine;
using System.Collections;

public class bounce : MonoBehaviour {

    public Vector3 direction;
    public int strength;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Rigidbody>().AddForce(direction * strength);
        }
    }

    // Update is called once per frame
    void Update () {
        
	}
 
}
