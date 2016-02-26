using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Charge_Slider_Script : MonoBehaviour {

	private Slider slider;

	public int minValue = 0;
	public int maxValue = 600;
	public Image background;
	public Image fill;
	public Color minValueColor = Color.red;
	public Color maxValueColor = Color.green;
	public int rate = 20;

	// Initialize myself
	void Awake () {
		slider = gameObject.GetComponent<Slider> ();
	}

	// Initialize relationship to others
	void Start () {
		slider.wholeNumbers = false;
		slider.minValue = minValue;
		slider.maxValue = maxValue;
		// Start empty
		slider.value = minValue;
	}

	// Update is called once per frame
	void Update () {
		UpdateValue ();
	}

	void UpdateValue () {
		if (Input.GetMouseButton(0) == true) {
			slider.value += rate;
		}
		else {
			slider.value = minValue;
		}
		if (slider.value > maxValue) {
			slider.value = maxValue;
		}
		background.color = Color.Lerp (minValueColor, maxValueColor, slider.value / maxValue);
		fill.color = Color.Lerp (minValueColor, maxValueColor, slider.value / maxValue);
	}
}
