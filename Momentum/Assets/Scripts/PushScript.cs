using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Assets;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class PushScript : Weapon
{
    public float pushforce = 1600f;
    public float pushplayergrounded = 2100f;
    public Vector3 yvectaerial = new Vector3(0, (float).25, 0);
    public float pushplayeraerial = 4000f;
    public float charge = 0f;
    public float primaryRange = 30f;
    public float primaryCooldown = 1f;
    public float secondaryRange = 30f;
    public float secondaryCooldown = 1f;
    // Update is called once per frame
    public override void primaryFire()
    {
        RaycastHit hit;
        Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward);
        fwd = fwd.normalized;
        if (Physics.Raycast(transform.position, fwd, out hit, getPrimaryRange()))
        {
            if (hit.rigidbody != null)
            {
                print("hit! " + fpsc.tag);
                hit.transform.GetComponent<PhotonView>().RPC("applyForce", PhotonTargets.All, (fwd * pushforce));
                //fpsc.ApplyForceToPlayer(((-fwd) * pullforce), hit.rigidbody.gameObject.tag);d

                //                hit.rigidbody.AddForce(fwd * pushforce);

                primaryTimeStamp = Time.time + getPrimaryCooldown();
                worldController.audioManager.playSound("push");
            }
        }
        rightTriggerUsed = true;

    }
    public override void secondaryFire()
    {
        RaycastHit hit;
        Vector3 fwd = fpsc.cam.transform.TransformDirection(Vector3.forward);

        if (fpsc.m_IsGrounded)
        {
            secondaryCoolingDown = true;
            Vector3 v = Vector3.up;
            fpsc.m_Jump = true;
            var temp = v.normalized * pushplayergrounded;
            //fpsc.AddForceToLocal(temp, ForceMode.Impulse);
            secondaryTimeStamp = Time.time + getSecondaryCooldown();
            //transform.root.GetComponent<Rigidbody>().AddForce((v).normalized * pushplayergrounded); // here, we're adding force to the player object.
            this.transform.GetComponent<Rigidbody>().AddForce(temp);
        }
        else
        {
            //fpsc.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (Physics.Raycast(transform.position, fwd, out hit, getSecondaryRange()))
            {
                if (hit.collider.tag == "Hook")
                {
                    fwd = fwd.normalized;
                    var temp = -((((hit.point - transform.position).normalized) + yvectaerial) * pushplayeraerial);
                   //fpsc.AddForceToLocal(temp, ForceMode.Impulse);
                    this.transform.GetComponent<Rigidbody>().AddForce(temp);
                    //transform.root.GetComponent<Rigidbody>().AddForce((-((hit.point - transform.position).normalized) + yvectaerial) * pushplayeraerial); // here, we're adding force to the player object.
                    secondaryCoolingDown = true;
                    secondaryTimeStamp = Time.time + getSecondaryCooldown();
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

        //                    Vector3 v = hit.transform.position - this.transform.position;
        //v = Quaternion.AngleAxis(180, Vector3.up) * v;
        leftTriggerUsed = true;

    }

    public override float getPrimaryRange()
    {
        return primaryRange;
    }

    public override float getSecondaryRange()
    {
        return secondaryRange;
    }

    public override float getPrimaryCooldown()
    {
        return primaryCooldown;
    }

    public override float getSecondaryCooldown()
    {
        return secondaryCooldown;
    }
}

