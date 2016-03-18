using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Assets;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class PullScript : Weapon
{

	public float pullforce = 40000f;
	public float pullplayergrounded = 2000f;
	public float pullplayeraerial = 5000f;
	public Vector3 yvectgrounded = new Vector3 (0, (float) .2, 0);
	public Vector3 yvectaerial = new Vector3 (0, (float) .8, 0);
	public float primaryRange= 30f;
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
                print("hit2!");
                //var distance = Vector3.Distance(hit.transform.position, transform.position);
                //hit.rigidbody.AddForce(((-fwd) * pullforce));

                fpsc.ApplyForceToPlayer(((-fwd) * pullforce), hit.rigidbody.gameObject.tag.ToCharArray()[6] - '0');

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
                print("hit3!");
                secondaryTimeStamp = Time.time + getSecondaryCooldown();
                if (fpsc.Grounded)
                {
                    fpsc.m_Jump = true;
                    fpsc.ApplyForceToPlayer((((hit.point - transform.position).normalized) + yvectgrounded) * pullplayergrounded, fpsc.gameObject.tag.ToCharArray()[6] - '0'); // here, we're adding force to the player object
                }
                else
                {
                    fpsc.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    fpsc.ApplyForceToPlayer((((hit.point - transform.position).normalized) + yvectaerial) * pullplayeraerial, fpsc.gameObject.tag.ToCharArray()[6] - '0'); // here, we're adding force to the player object
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

