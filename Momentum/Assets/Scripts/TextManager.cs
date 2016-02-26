using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public Text[] textFields;

	public string[] englishText;

	public string[] spanishText;

	public string[] frenchText;

	public string[] germanText;

	public string[] italianText;

	public string[] japaneseText;

	public string[] russianText;

	// Use this for initialization
	void Start () {
		setLanguage ("English");
	}

	public void setLanguage(string targetLang) {
		switch (targetLang) {
		case "English":
			for(int i = 0; i < textFields.Length; i++) {
				textFields[i].text = englishText[i];
			}
			break;
		case "Spanish":
			for(int i = 0; i < textFields.Length; i++) {
				textFields[i].text = spanishText[i];
			}
			break;
		case "French":
			for(int i = 0; i < textFields.Length; i++) {
				textFields[i].text = frenchText[i];
			}
			break;
		case "German":
			for(int i = 0; i < textFields.Length; i++) {
				textFields[i].text = germanText[i];
			}
			break;
		case "Italian":
			for(int i = 0; i < textFields.Length; i++) {
				textFields[i].text = italianText[i];
			}
			break;
		case "Japanese":
			for(int i = 0; i < textFields.Length; i++) {
				textFields[i].text = japaneseText[i];
			}
			break;
		case "Russian":
			for(int i = 0; i < textFields.Length; i++) {
				textFields[i].text = russianText[i];
			}
			break;
		}
	}
}
