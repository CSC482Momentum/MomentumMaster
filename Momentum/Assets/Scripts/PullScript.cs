using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Assets;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class PullScript : Weapon
{

	public float pullforce = 400f;
	public float pullplayergrounded = 2000f;
	public float pullplayeraerial = 5000f;
	public Vector3 yvectgrounded = new Vector3 (0, (float) .2, 0);
	public Vector3 yvectaerial = new Vector3 (0, (float) .8, 0);
	public float range= 15f;
	public float cooldown = 1f;

    private float timeStamp;
    private bool rightTriggerUsed = false;
    private bool leftTriggerUsed = false;
    public RigidbodyFirstPersonController fpsc;
    public WorldController worldController;

    // Update is called once per frame
    void Update()
    {
        if (timeStamp <= Time.time)
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1") || Input.GetAxisRaw("Xbox Right Trigger") != 0)
            {
                if (!rightTriggerUsed) {
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
                            worldController.audioManager.playSound ("pull");
                        }
                    }
                    rightTriggerUsed = true;
                }
            }
            if (Input.GetAxisRaw("Xbox Right Trigger") == 0) {
                rightTriggerUsed = false;
            }

            if (CrossPlatformInputManager.GetButtonDown("Fire2") || Input.GetAxisRaw("Xbox Left Trigger") != 0)
            {
                if (!leftTriggerUsed) {
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
                    leftTriggerUsed = true;
                }
            }
            if (Input.GetAxisRaw("Xbox Left Trigger") == 0) {
                leftTriggerUsed = false;
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

