using UnityEngine;
using System.Collections;
//using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

namespace Assets
{
    public abstract class Weapon : NetworkBehaviour
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
            var Allfpsc = GameObject.FindObjectsOfType<RigidbodyFirstPersonController>();
            fpsc = gameObject.transform.root.GetComponent<RigidbodyFirstPersonController>();
            print("fpsc from the root:\n" + fpsc.ToString());
            
            fpsc = fpsc.connectionToServer.playerControllers[0].unetView.GetComponent<RigidbodyFirstPersonController>();
            print("fpsc from the server:\n" + fpsc.ToString());

            var thing = fpsc.playerControllerId;
            var thing2 = fpsc.connectionToServer.playerControllers[thing];
            var thing3 = thing2.gameObject.GetComponent<RigidbodyFirstPersonController>();
            print("fpsc from server player controllers? " + thing3.ToString());
            /*var thing4 = fpsc.netId;
            var thing5 = NetworkServer.objects[thing4];
            var thing6 = thing5.GetComponent<RigidbodyFirstPersonController>();
            print("fpsc from going through NetworkServer" + thing6.ToString());*/
            //fpsc = GameObject.FindGameObjectWithTag("Player_HOST").GetComponent<RigidbodyFirstPersonController>();
            //print("fpsc searching for Player_HOST\n" + fpsc.ToString());
            /*var temp1 = fpsc.connectionToClient;
            var temp2 = temp1.playerControllers;
            var temp3 = temp2[0];
            var temp4 = temp3.unetView;
            var temp = temp4.GetComponent<RigidbodyFirstPersonController>();
            print("fpsc from the client connection:\n" + temp.ToString());*/
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