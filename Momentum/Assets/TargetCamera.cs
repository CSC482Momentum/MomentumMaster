using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TargetCamera : NetworkBehaviour {

    void Awake()
    {
        if (GetComponent<NetworkView>().isMine)
        {
//            GetComponent<Camera>().enable = true;
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
