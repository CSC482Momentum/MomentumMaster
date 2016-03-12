using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

namespace Assets
{
    public abstract class Weapon : MonoBehaviour
    {
        [HideInInspector]
        public float primaryTimeStamp;
        [HideInInspector]
        public float secondaryTimeStamp;
        [HideInInspector]
        public bool rightTriggerUsed = false;
        [HideInInspector]
        public bool leftTriggerUsed = false;
        [HideInInspector]
        public WorldController worldController = WorldController.getInstance();
        public RigidbodyFirstPersonController fpsc;

        void Start()
        {
            fpsc = gameObject.transform.root.GetComponent<RigidbodyFirstPersonController>();
            /*var thing = fpsc.connectionToClient;
            gameObject.transform.GetComponent<NetworkIdentity>().AssignClientAuthority(thing);
            thing = fpsc.connectionToServer;
            gameObject.transform.GetComponent<NetworkIdentity>().AssignClientAuthority(thing);*/
        }
        public void FixedUpdate()
        {
            //print("is local player? " + fpsc.isLocalPlayer);
            if (fpsc.isLocalPlayer)
            {
                //        timeStamp = Time.time + cooldown;
                if (primaryTimeStamp <= Time.time)
                {
                    resetPrimaryFire();
                    if (CrossPlatformInputManager.GetButtonDown("Fire1") || Input.GetAxisRaw("Xbox Right Trigger") != 0)
                    {
                        if (!rightTriggerUsed)
                        {
                            CmdPrimaryFire();
                            //rightTriggerUsed = true;
                            //primaryTimeStamp = Time.time + getPrimaryCooldown();
                        }
                    }
                    if (Input.GetAxisRaw("Xbox Right Trigger") == 0)
                    {
                        rightTriggerUsed = false;
                    }
                }
                if (secondaryTimeStamp <= Time.time)
                {

                    resetSecondaryFire();
                    if (CrossPlatformInputManager.GetButtonDown("Fire2") || Input.GetAxisRaw("Xbox Left Trigger") != 0)
                    {
                        if (!leftTriggerUsed)
                        {
                            CmdSecondaryFire();
                            leftTriggerUsed = true;
                            secondaryTimeStamp = Time.time + getSecondaryCooldown();
                        }
                    }
                    if (Input.GetAxisRaw("Xbox Left Trigger") == 0)
                    {
                        leftTriggerUsed = false;
                    }
                }
            }
        }

        public abstract float getPrimaryRange();
        public abstract float getSecondaryRange();
        public abstract float getPrimaryCooldown();
        public abstract float getSecondaryCooldown();

        public abstract void CmdSecondaryFire();
        public abstract void CmdPrimaryFire();

        public abstract void resetSecondaryFire();
        public abstract void resetPrimaryFire();
    }
}