using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Charge_Slider_Ghost_Script : MonoBehaviour {

	private Slider slider;
	public Slider target;

	// Initialize myself
	void Awake () {
		slider = gameObject.GetComponent<Slider> ();
	}

	// Initialize relationship to others
	void Start () {
		slider.wholeNumbers = false;
		slider.minValue = target.minValue;
		slider.maxValue = target.maxValue;
		// Start empty
		slider.value = target.minValue;
	}

	// Update is called once per frame
	void Update () {
		UpdateValue ();
	}

	void UpdateValue () {
		if (Input.GetMouseButtonUp (0) == true) {
			slider.value = target.value;
		}
	}
}
