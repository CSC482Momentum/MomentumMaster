using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Assets;
using System;

public class PushScript : Weapon
{
    public float pushforce;
    public float pushplayergrounded;
    public Vector3 yvectaerial;
    public float pushplayeraerial;
    private float timeStamp;
    public float charge;
    public float range;
    public float cooldown;


    public RigidbodyFirstPersonController fpsc;

    // Update is called once per frame
    void Update()
    {
        //        timeStamp = Time.time + cooldown;
        if (timeStamp <= Time.time)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward);
                fwd = fwd.normalized;
                if (Physics.Raycast(transform.position, fwd, out hit, range))
                {
                    if (hit.rigidbody != null)
                    {
                        print("hit!");
                        hit.rigidbody.AddForce(fwd * pushforce);
                        timeStamp = Time.time + cooldown;
                    }
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                RaycastHit hit;
                Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward);
                if (fpsc.Grounded)
                {
                    Vector3 v = Vector3.up;
                    fpsc.m_Jump = true;
                    this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce((v).normalized * pushplayergrounded);
                }
                else
                {
                    fpsc.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    if (Physics.Raycast(transform.position, fwd, out hit, range))
                    {
                        if (hit.collider.tag == "Hook")
                        {
                            fwd = fwd.normalized;
                            this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce((-((hit.point - transform.position).normalized) + yvectaerial) * pushplayeraerial);
                            timeStamp = Time.time + cooldown;
                        }
                    }

                }

                //                }
                //            }



                //            if (Physics.Raycast(transform.position, fwd, out hit, 100))
                //           {
                //                if (hit.collider.tag == "Hook")
                //                {
                //this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce((hit.transform.position - transform.position).normalized * -pushplayer);

                //Vector3 dir = (hit.transform.position - transform.position).normalized;
                //fpsc.GetComponent<Rigidbody>().AddForce(Vector3.Cross(dir, new Vector3(1,0,0)) * -pushplayer); 

                //this.transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce( -((hit.transform.position - transform.position)).normalized * pushplayer);

                //					Vector3 v = hit.transform.position - this.transform.position;
                //v = Quaternion.AngleAxis(180, Vector3.up) * v;
            }
        }
    }

    public override float getRange()
    {
        return range;
    }

    public override float getCooldown()
    {
        return cooldown;
    }
}

