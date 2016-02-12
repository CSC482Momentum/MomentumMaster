using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets;

public class Cooldown_Clock : MonoBehaviour {

	private Image image;
	private bool isCoolingDown = false;
    private float coolDownTime;
    public Weapon weap;

	// Initialize myself
	void Awake () {
		image = gameObject.GetComponent<Image> ();
        this.coolDownTime = weap.getCooldown();
       // print(coolDownTime);
	}

	// Initialize relationship to others
	void Start () {
		//null
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			isCoolingDown = true;
		}
		UpdateValue ();
	}

	void UpdateValue () {
		if (isCoolingDown == true) {
			image.fillAmount -= 1.0f/coolDownTime * Time.deltaTime;
		}
		if (image.fillAmount == 0) {
			isCoolingDown = false;
			image.fillAmount = 1;
		}
	}
}
