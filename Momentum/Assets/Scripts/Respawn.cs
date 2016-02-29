using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public GameObject prefabPe;
    public Vector3 Spawn;
    public Quaternion Rotation;
    

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Player"))
        {

            //Instantiate(prefabPe, col.transform.position, Rotation);
            col.transform.position = Spawn;

//          col.transform.localEulerAngles = Rotation;

			col.attachedRigidbody.velocity = Vector3.zero;
			col.attachedRigidbody.rotation = Quaternion.Euler(0, 0, 0);

 //           Destroy(prefabPe, 2);

 //           col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
 //           new WaitForSeconds(1);
//            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}