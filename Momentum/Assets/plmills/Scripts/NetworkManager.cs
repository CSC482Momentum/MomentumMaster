using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private GameObject[] spawnPoints;

	// Use this for initialization
	void Start () {
		//spawnPoints = GameObject.FindObjectsOfType<Spawn>();
		spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
		Connect();
	}

	void Connect() {
		PhotonNetwork.ConnectUsingSettings("Test Network");
	}

	void OnGUI() {
		GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString() );

	}
		
	void OnConnectedToMaster() {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom( null );
	}

	void OnJoinedRoom() {
		Debug.Log ("OnJoinedRoom");
		SpawnPlayer();
	}

	void SpawnPlayer() {
		
		GameObject mySpawn = spawnPoints[ Random.Range (0, spawnPoints.Length) ];

		GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("PlayerTest", mySpawn.transform.position, mySpawn.transform.rotation, 0);

		myPlayer.GetComponentInChildren<Camera>().enabled = true;
		//myPlayer.GetComponentInChildren<Pulling>().enabled = true;
		myPlayer.GetComponentInChildren<Movement>().enabled = true;
        myPlayer.GetComponentInChildren<PullScript>().enabled = true;
        myPlayer.GetComponentInChildren<WeaponsManager>().enabled = true;
        //myPlayer.GetComponentInChildren<MouseLook>().enabled = true;
    }

}
