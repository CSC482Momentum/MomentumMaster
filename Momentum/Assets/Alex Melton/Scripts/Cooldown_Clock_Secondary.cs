using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets;
using UnityStandardAssets.CrossPlatformInput;

public class Cooldown_Clock_Secondary : MonoBehaviour
{

    private Image image;
    private bool isCoolingDown = false;
    private float coolDownTime;
    public Weapon weap;

    // Initialize myself
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        //this.coolDownTime = weap.getPrimaryCooldown();
        // print(coolDownTime);
    }

    // Initialize relationship to others
    void Start()
    {
        //null
    }

    // Update is called once per frame
    void Update()
    {
        /*if (weap.isActiveAndEnabled && !isCoolingDown && CrossPlatformInputManager.GetButtonDown("Fire2") || Input.GetAxisRaw("Xbox Left Trigger") != 0)
        {
            isCoolingDown = true;
        }
        UpdateValue ();*/
        if (weap.secondaryCoolingDown)
        {
            image.fillAmount -= 1.0f / weap.getSecondaryCooldown() * Time.deltaTime;
        }
        else
        {
            image.fillAmount = 1;
        }
        if (image.fillAmount <= 0)
        {
            image.fillAmount = 1;
        }
    }

    /*void UpdateValue () {
		if (isCoolingDown == true) {
			image.fillAmount -= 1.0f/coolDownTime * Time.deltaTime;
		}
		if (image.fillAmount == 0) { // this means that we JUST finshed cooling down.
			isCoolingDown = false;
			image.fillAmount = 1;
		}
	}*/
}
