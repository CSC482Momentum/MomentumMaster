using UnityEngine;
using System.Collections;

public class Respawning : MonoBehaviour {

	private GameObject[] spawnPoints;

	// Use this for initialization
	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag.Contains("Player"))
		{

			GameObject mySpawn = spawnPoints[ Random.Range (0, spawnPoints.Length) ];

			col.transform.position = mySpawn.transform.position;
			col.transform.rotation = mySpawn.transform.rotation;

			col.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
			col.GetComponentInParent<Rigidbody>().rotation = mySpawn.transform.rotation;
		}
	}
}
