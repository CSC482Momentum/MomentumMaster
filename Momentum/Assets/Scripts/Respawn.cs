using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public GameObject prefabPe;
    public Vector3 Spawn1;
    public Vector3 Spawn2;
    public Vector3 Spawn3;
    public Vector3 Spawn4;
    public Vector3 Spawn5;
    public Quaternion Rotation;
    

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Contains("Player1"))
        {

            //Instantiate(prefabPe, col.transform.position, Rotation);
            col.transform.position = Spawn1;

//          col.transform.localEulerAngles = Rotation;

			col.attachedRigidbody.velocity = Vector3.zero;
			col.attachedRigidbody.rotation = Quaternion.Euler(0, 0, 0);

 //           Destroy(prefabPe, 2);

 //           col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
 //           new WaitForSeconds(1);
//            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        } else if (col.tag.Contains("Player2"))
        {
            col.transform.position = Spawn2;

            col.attachedRigidbody.velocity = Vector3.zero;
            col.attachedRigidbody.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (col.tag.Contains("Player3"))
        {
            col.transform.position = Spawn3;

            col.attachedRigidbody.velocity = Vector3.zero;
            col.attachedRigidbody.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (col.tag.Contains("Player4"))
        {
            col.transform.position = Spawn4;

            col.attachedRigidbody.velocity = Vector3.zero;
            col.attachedRigidbody.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (col.tag.Contains("Player5"))
        {
            col.transform.position = Spawn5;

            col.attachedRigidbody.velocity = Vector3.zero;
            col.attachedRigidbody.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}