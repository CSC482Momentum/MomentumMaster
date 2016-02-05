using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PushScript : MonoBehaviour
{
    public float pushforce;
    public float pushplayer;
    public RigidbodyFirstPersonController fpsc;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward);
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
        if (Input.GetButtonDown("Fire2") && fpsc.Grounded)
        {
            RaycastHit hit;
			Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward);
            fwd = fwd.normalized;
            if (Physics.Raycast(transform.position, fwd, out hit, 100))
            {
                if (hit.collider.tag == "Hook")
                {
                    print("hit4!");
                    fpsc.m_Jump = true;

                    //this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce((hit.transform.position - transform.position).normalized * -pushplayer);

                    //Vector3 dir = (hit.transform.position - transform.position).normalized;
                    //fpsc.GetComponent<Rigidbody>().AddForce(Vector3.Cross(dir, new Vector3(1,0,0)) * -pushplayer); 

					//this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce( -((hit.transform.position - transform.position)).normalized * pushplayer);

					Vector3 v = hit.transform.position - this.transform.position;
					//v = Quaternion.AngleAxis(180, Vector3.up) * v;

					this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce( -(v).normalized * pushplayer);
                    
                }
            }
        }
    }
}

