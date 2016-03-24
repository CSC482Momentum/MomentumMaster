using UnityEngine;
using System.Collections;
using Assets;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class PullScript : Weapon
{

    public float pullforce = 40000f;
    public float pullplayergrounded = 2000f;
    public float pullplayeraerial = 5000f;
    public Vector3 yvectgrounded = new Vector3(0, (float).2, 0);
    public Vector3 yvectaerial = new Vector3(0, (float)1, 0);
    public float primaryRange = 30f;
    public float secondaryRange = 30f;
    public float primaryCooldown = 1f;
    public float secondaryCooldown = 1f;


    // Update is called once per frame
    public override void primaryFire()
    {
        RaycastHit hit;
        //        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 fwd = fpsc.cam.transform.TransformDirection(Vector3.forward); //here we're getting the direction of the camera.

        fwd = fwd.normalized;
        if (Physics.Raycast(transform.position, fwd, out hit, getPrimaryRange()))
        {
            if (hit.rigidbody != null)
            {
                primaryCoolingDown = true;
                print("hit2!");
                //var distance = Vector3.Distance(hit.transform.position, transform.position);
                //hit.rigidbody.AddForce(((-fwd) * pullforce));
                //fpsc.transform.GetComponent<PhotonView>().RPC("applyForce", PhotonTargets.All, value * pushForce);

                hit.transform.GetComponent<PhotonView>().RPC("applyForce", PhotonTargets.All, ((-fwd) * pullforce));
                //fpsc.ApplyForceToPlayer(((-fwd) * pullforce), hit.rigidbody.gameObject.tag);

                worldController.audioManager.playSound("pull");
                primaryTimeStamp = Time.time + getPrimaryCooldown();
            }
        }
        rightTriggerUsed = true;
    }
    public override void secondaryFire()
    {
        RaycastHit hit;
        //        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 fwd = fpsc.cam.transform.TransformDirection(Vector3.forward); //here we're getting the direction of the camera.
        fwd = fwd.normalized;
        if (Physics.Raycast(transform.position, fwd, out hit, getSecondaryRange()))
        {
            if (hit.collider.tag == "Hook")
            {
                secondaryCoolingDown = true;
                print("hit3!");
                secondaryTimeStamp = Time.time + getSecondaryCooldown();
                if (fpsc.m_IsGrounded)
                {
                    var temp = (((hit.point - transform.position).normalized) + yvectgrounded) * pullplayergrounded;
                    print("hit4! "+ temp);
                    fpsc.m_Jump = true;
                    //fpsc.ApplyForceToPlayer(temp, fpsc.gameObject.tag.ToCharArray()[6] - '0'); // here, we're adding force to the player object
                    //fpsc.transform.GetComponent<PhotonView>().RPC("applyForce", PhotonTargets.All, temp);
                    //fpsc.AddForceToLocal(temp, ForceMode.Impulse);
                    //transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce(temp);
                    //fpsc.m_RigidBody.AddForce(temp);
                    this.transform.GetComponent<Rigidbody>().AddForce(temp);
                }
                else
                {
                    this.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    var temp = (((hit.point - transform.position).normalized) + yvectaerial) * pullplayeraerial;
                    //fpsc.ApplyForceToPlayer((((hit.point - transform.position).normalized) + yvectaerial) * pullplayeraerial, fpsc.gameObject.tag.ToCharArray()[6] - '0'); // here, we're adding force to the player object
                    //fpsc.AddForceToLocal(temp, ForceMode.Impulse);
                    //fpsc.transform.GetComponent<PhotonView>().RPC("applyForce", PhotonTargets.All, temp);
                    fpsc.m_RigidBody.AddForce(temp);
                    //transform.parent.transform.parent.GetComponent<Rigidbody>().AddForce(temp);
                    this.transform.GetComponent<Rigidbody>().AddForce(temp);
                }
            }
        }
        leftTriggerUsed = true;


    }

    public override float getPrimaryRange()
    {
        return primaryRange;
    }

    public override float getPrimaryCooldown()
    {
        return primaryCooldown;
    }

    public override float getSecondaryRange()
    {
        return secondaryRange;
    }

    public override float getSecondaryCooldown()
    {
        return secondaryCooldown;
    }
}

