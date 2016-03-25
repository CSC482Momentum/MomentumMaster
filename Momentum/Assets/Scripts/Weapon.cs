using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

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
        public Movement fpsc;
        public GameObject model;
        public bool primaryCoolingDown = false;
        public bool secondaryCoolingDown = false;
        void FixedUpdate()
        { 
            //        timeStamp = Time.time + cooldown;
            if (primaryTimeStamp <= Time.time)
            {
                primaryCoolingDown = false;
                if (CrossPlatformInputManager.GetButtonDown("Fire1") || Input.GetAxisRaw("Xbox Right Trigger") != 0)
                {
                    if (!rightTriggerUsed)
                    {
                        primaryFire();
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
                secondaryCoolingDown = false;
                if (CrossPlatformInputManager.GetButtonDown("Fire2") || Input.GetAxisRaw("Xbox Left Trigger") != 0)
                {
                    if (!leftTriggerUsed)
                    {
                        secondaryFire();
                        leftTriggerUsed = true;
                        //secondaryTimeStamp = Time.time + getSecondaryCooldown();
                    }
                }
                if (Input.GetAxisRaw("Xbox Left Trigger") == 0)
                {
                    leftTriggerUsed = false;
                }
            }

        }
        public bool isPrimaryCoolingDown()
        {
            return primaryCoolingDown;
        }
        public bool isSecondaryCoolingDown()
        {
            return secondaryCoolingDown;
        }
        public abstract float getPrimaryRange();
        public abstract float getSecondaryRange();
        public abstract float getPrimaryCooldown();
        public abstract float getSecondaryCooldown();
        public abstract void secondaryFire();
        public abstract void primaryFire();
    }
}