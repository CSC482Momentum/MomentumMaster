using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Direction_Slider_Script : MonoBehaviour {

	private Slider slider;
	public Transform target;

	// Initialize myself
	void Awake () {
		slider = gameObject.GetComponent<Slider> ();
	}

	// Initialize relationship to others
	void Start () {
		slider.wholeNumbers = false;
		slider.minValue = 0;
		slider.maxValue = 360;
		// Start empty
		slider.value = 0;
	}

	// Update is called once per frame
	void Update () {
		UpdateValue ();
	}

	void UpdateValue () {
		slider.value = target.rotation.eulerAngles.y;
	}
}
