using UnityEngine;
using System.Collections;

public class HandleForces : MonoBehaviour {

	[PunRPC]
	public void applyForce(Vector3 force){
		print("Network");
		print(force);

		this.transform.GetComponent<Rigidbody>().AddForce(force);
        print("Force:" + force);
	}


}
