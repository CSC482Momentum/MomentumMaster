using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PullScript : MonoBehaviour
{

    public float pullforce;
    public float pullplayergrounded;
    public float pullplayeraerial;
    public Vector3 yvectgrounded;
    public Vector3 yvectaerial;
    public float range;
    public float cooldown;
    private float timeStamp;
    public RigidbodyFirstPersonController fpsc;

    // Update is called once per frame
    void Update()
    {
        if (timeStamp <= Time.time)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                //        Vector3 fwd = transform.TransformDirection(Vector3.forward);
                Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward);
                fwd = fwd.normalized;
                if (Physics.Raycast(transform.position, fwd, out hit, 20))
                {
                    if (hit.rigidbody != null)
                    {
                        print("hit2!");
                        hit.rigidbody.AddForce((-fwd) * pullforce);
                        timeStamp = Time.time + cooldown;
                    }
                }
            }

            if (Input.GetButtonDown("Fire2"))
            {
                RaycastHit hit;
                //        Vector3 fwd = transform.TransformDirection(Vector3.forward);
                Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward);
                fwd = fwd.normalized;
                if (Physics.Raycast(transform.position, fwd, out hit, range))
                {
                    if (hit.collider.tag == "Hook")
                    {
                        print("hit3!");
                        if (fpsc.Grounded)
                        {
                            fpsc.m_Jump = true;
                            this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce((((hit.point - transform.position).normalized) + yvectgrounded) * pullplayergrounded);

                        }
                        else
                        {
                            fpsc.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce((((hit.point - transform.position).normalized) + yvectaerial) * pullplayeraerial);
                        }
                        timeStamp = Time.time + cooldown;
                    }

                }
            }
        }
    }
}

