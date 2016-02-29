using UnityEngine;
using UnityEngine.Networking;

public class Multiplayer : NetworkBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (isLocalPlayer) return; //Do not execute on client
        GameObject pl = GameObject.FindWithTag("Player");

        if (pl != null) {
            for (int i = 1; i < 5; i++) {
                bool filled = GameObject.FindWithTag("Player" + i) != null;
                if (!filled) {
                    pl.tag = "Player" + i;
                    print("Player's NetworkID: " + pl.GetComponent<NetworkIdentity>().netId);
                    print("Assigned Player " + i);
                    break;
                }
            }
        }
    }
}