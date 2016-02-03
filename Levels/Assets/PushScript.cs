using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PushScript : MonoBehaviour {


   public float pushforce;
    public float pushplayer;
    public RigidbodyFirstPersonController fpsc;
 
    // Update is called once per frame
    void Update()  {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward);
            fwd = fwd.normalized;
            if (Physics.Raycast(transform.position, fwd, out hit, 10))
            {

                if (hit.collider.tag == "Hook")
                {
                    print("hit4!");
                    fpsc.m_Jump = true;
//                    this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce((hit.transform.position - transform.position).normalized * -pushplayer);
                    fpsc.GetComponent<Rigidbody>().AddForce((hit.transform.position - transform.position).normalized * -pushplayer);
                }
                else
                {

                    if (hit.rigidbody != null)
                    {
                        print("hit!");
                        hit.rigidbody.AddForce(fwd * pushforce);
                    }
                }
            }
        }
    }

	
}

