using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Assets;
using System;
using UnityStandardAssets.CrossPlatformInput;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class PullScript : Weapon
{

    public float pullforce = 40000f;
    public float pullplayergrounded = 2000f;
    public float pullplayeraerial = 5000f;
    public Vector3 yvectgrounded = new Vector3(0, (float).2, 0);
    public Vector3 yvectaerial = new Vector3(0, (float).8, 0);
    public float primaryRange = 30f;
    public float secondaryRange = 30f;
    public float primaryCooldown = 1f;
    public float secondaryCooldown = 1f;

    public override void CmdPrimaryFire()
    {
        RaycastHit hit;
        //        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward); //here we're getting the direction of the camera.
        fwd = fwd.normalized;
        if (Physics.Raycast(transform.position, fwd, out hit, getPrimaryRange()))
        {
            if (hit.rigidbody != null)
            {
                print("hit2!");
                //var distance = Vector3.Distance(hit.transform.position, transform.position);
                //hit.rigidbody.AddForce(((-fwd) * pullforce));
                fpsc.ApplyForceToPlayer(-fwd * pullforce, hit.rigidbody.gameObject.tag);
                //var otherPlayer = hit.transform.gameObject.GetComponent<RigidbodyFirstPersonController>();
                //otherPlayer.CmdAddForce(((-fwd) * pullforce), ForceMode.Impulse);
                //fpsc.ApplyForceToPlayer(((-fwd) * pullforce), Int32.Parse((Regex.Match(hit.rigidbody.gameObject.tag, @"\d+").Value)));
                worldController.audioManager.playSound("pull");
                primaryTimeStamp = Time.time + getPrimaryCooldown();
            }
        }
        rightTriggerUsed = true;
    }
    public override void CmdSecondaryFire()
    {
        if (fpsc.isLocalPlayer)
        {
            RaycastHit hit;
            //        Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 fwd = transform.parent.transform.TransformDirection(Vector3.forward); //here we're getting the direction of the camera.
            //Vector3 fwd = transform.TransformDirection(Vector3.forward); //here we're getting the direction of the camera.
            fwd = fwd.normalized;
            if (Physics.Raycast(transform.position, fwd, out hit, getSecondaryRange()))
            {
                if (hit.collider.tag == "Hook")
                {
                    fpsc.movementSettings.movingWithWeapon = true;
                    secondaryTimeStamp = Time.time + getSecondaryCooldown();
                    if (fpsc.Grounded)
                    {
                        fpsc.m_RigidBody.drag = 5f;
                        fpsc.m_Jump = true;
                        var temp = (((hit.point - transform.position).normalized) + yvectgrounded) * pullplayergrounded;
                        //float step = Time.deltaTime;
                        //var temp = Vector3.MoveTowards(transform.position, hit.point, step);
                        //transform.root.GetComponent<RigidbodyFirstPersonController>().CmdAddForce(temp, ForceMode.Impulse);
                        //transform.root.GetComponent<RigidbodyFirstPersonController>().RpcAddServerForce(temp);
                        fpsc.CmdAddForce(temp, ForceMode.Impulse);
                        //fpsc.ApplyForceToPlayer(temp, fpsc.GetComponent<Rigidbody>().gameObject.tag.ToCharArray()[6] - '0');
                        //fpsc.RpcAddServerForce(temp);
                        //print("hit3!" + "Pulling player with force: " + temp);
                        //transform.root.GetComponent<Rigidbody>().AddForce((((hit.point - transform.position).normalized) + yvectgrounded) * pullplayergrounded); // here, we're adding force to the player object
                    }
                    else
                    {
                        fpsc.m_RigidBody.drag = 0f;
                        //fpsc.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        var temp = (((hit.point - transform.position).normalized) + yvectgrounded) * pullplayeraerial;
                        //float step = Time.deltaTime;
                        //var temp = Vector3.MoveTowards(transform.position, hit.point, step);
                        fpsc.CmdAddForce(temp, ForceMode.Impulse);
                        //transform.root.GetComponent<RigidbodyFirstPersonController>().CmdAddForce(temp, ForceMode.Impulse);
                        //transform.root.GetComponent<RigidbodyFirstPersonController>().RpcAddServerForce(temp);
                        //fpsc.RpcAddServerForce(temp);
                        //fpsc.ApplyForceToPlayer(temp, fpsc.GetComponent<Rigidbody>().gameObject.tag.ToCharArray()[6] - '0');
                        //print("hit4!" + "Pulling player with force: " + temp);
                        // transform.root.GetComponent<Rigidbody>().AddForce((((hit.point - transform.position).normalized) + yvectaerial) * pullplayeraerial); // here, we're adding force to the player object
                    }
                }
            }
            leftTriggerUsed = true;
        }


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
    public override void resetSecondaryFire()
    {
        fpsc.movementSettings.movingWithWeapon = false;
    }

    public override void resetPrimaryFire()
    {
    }
}

