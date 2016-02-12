using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class Clickable : MonoBehaviour {


   public float pushforce;
    public float pullforce;
    public float pullplayer;
    public RigidbodyFirstPersonController fpsc;
 
    // Update is called once per frame
    void Update()  {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            fwd = fwd.normalized;
            if (Physics.Raycast(transform.position, fwd, out hit, 10))
            {

                if (hit.rigidbody != null)
                {
                    print("hit!");
                    hit.rigidbody.AddForce(fwd * pushforce);
                }
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            fwd = fwd.normalized;
            if (Physics.Raycast(transform.position, fwd, out hit, 20))
            {
                if (hit.collider.tag == "Hook")
                {
                    print("hit3!");
                    fpsc.m_Jump = true;
                    this.transform.parent.GetComponent<Rigidbody>().AddForce((hit.transform.position - transform.position).normalized * pullplayer);

                }
                else
                {
                    if (hit.rigidbody != null)
                    {
                        print("hit2!");
                        hit.rigidbody.AddForce((-fwd) * pullforce);
                    }
                }
            }
        }
    }

	
}

