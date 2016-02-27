﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets;
using UnityStandardAssets.CrossPlatformInput;

public class Cooldown_Clock_Primary : MonoBehaviour {

	private Image image;
	private bool isCoolingDown = false;
    private float coolDownTime;
    public Weapon weap;

	// Initialize myself
	void Awake () {
		image = gameObject.GetComponent<Image> ();
        this.coolDownTime = weap.getSecondaryCooldown();
       // print(coolDownTime);
	}

	// Initialize relationship to others
	void Start () {
		//null
	}

	// Update is called once per frame
	void Update () {
        if (weap.isActiveAndEnabled && !isCoolingDown && CrossPlatformInputManager.GetButtonDown("Fire1") || Input.GetAxisRaw("Xbox Right Trigger") != 0)
        {
            isCoolingDown = true;
        }
        UpdateValue ();
	}

	void UpdateValue () {
		if (isCoolingDown == true) {
			image.fillAmount -= 1.0f/coolDownTime * Time.deltaTime;
		}
		if (image.fillAmount == 0) { // this means that we JUST finshed cooling down.
			isCoolingDown = false;
			image.fillAmount = 1;
		}
	}
}
