using UnityEngine;
using System.Collections;

public class NetworkSmooth : Photon.MonoBehaviour {

	public Vector3 forceApplied = Vector3.zero;

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	Vector3 lastPosition = Vector3.zero;

	public double realTime = 0.0;
	public double realPacketTime = 0.0;
	public double lastPacketTime = 0.0;
	public double goalTime = 0.0;




	void Start () {
		realPosition = transform.position;
		realRotation = transform.rotation;


	}
    
	// Update is called once per frame
	void Update () {
		//If the photonView is another character's then just use current characters controller
		if( !photonView.isMine ) {

			goalTime = realPacketTime - lastPacketTime;
			realTime += Time.deltaTime;

			transform.position = Vector3.Lerp(lastPosition, realPosition, (float) (realTime/goalTime) );
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);

		}
		/**else{

			transform.GetComponent<Rigidbody>().isKinematic = false;

			transform.GetComponent<Rigidbody>().AddForce(forceApplied);

			if(forceApplied != Vector3.zero){
				print("Testing");
				print(forceApplied);
			}

			forceApplied = Vector3.zero;

			//transform.GetComponent<Rigidbody>().isKinematic = true;
		}
		*/
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		//If writing to the stream then this character's information needs to be sent to the network
		if(stream.isWriting) {
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else {

			realTime = 0.0;
			lastPosition = transform.position;

			//If not writing to stream then the information is another character's and needs to be updated on our end
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();

			lastPacketTime = realPacketTime;
			realPacketTime = info.timestamp;
		}

	}



}
