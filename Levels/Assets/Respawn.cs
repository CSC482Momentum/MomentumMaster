﻿using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public GameObject prefabPe;
    public Vector3 Spawn;
    public Quaternion Rotation;
    

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {

            Instantiate(prefabPe, col.transform.position, Rotation);
            col.transform.position = Spawn;
//          col.transform.localEulerAngles = Rotation;


 //           Destroy(prefabPe, 2);

 //           col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
 //           new WaitForSeconds(1);
//            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}