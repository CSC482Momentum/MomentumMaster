using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cooldown_Clock : MonoBehaviour {

	private Image image;
	private bool isCoolingDown = false;
	public float coolDownTime = 10.0f;

	// Initialize myself
	void Awake () {
		image = gameObject.GetComponent<Image> ();
	}

	// Initialize relationship to others
	void Start () {
		//null
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) == true) {
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
