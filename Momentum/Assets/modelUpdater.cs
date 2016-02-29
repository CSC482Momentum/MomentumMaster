using UnityEngine;
using System.Collections;

public class modelUpdater : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(this.transform.parent.position);
        this.transform.Rotate(this.transform.parent.rotation.eulerAngles);
	}
}
