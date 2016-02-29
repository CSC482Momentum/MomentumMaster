using UnityEngine;
using System.Collections;

using UnityEngine;


public class Billboard : MonoBehaviour
{
	public Camera c;

	void Update() 
	{
		transform.LookAt(Camera.main.transform.position, Vector3.up);
	}
}
