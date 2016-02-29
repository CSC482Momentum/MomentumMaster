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

	public WorldController worldController;

	// Use this for initialization
	void Start () {
		
		//worldController = WorldController.getInstance ();
	}

	public void setLanguage(string targetLang) {
		switch (targetLang) {
		case "English":
			for(int i = 0; i <= textFields.Length - 2; i+=2) {
				textFields[i].text = englishText[i/2];
				textFields[i+1].text = englishText[i/2];
				worldController.setCurrentLanguage ("English");

			}
			break;
		case "Spanish":
			for(int i = 0; i < textFields.Length; i+=2) {
				textFields[i].text = spanishText[i/2];
				textFields[i+1].text = spanishText[i/2];
				worldController.setCurrentLanguage ("Spanish");
			}
			break;
		case "French":
			for(int i = 0; i < textFields.Length; i+=2) {
				textFields[i].text = frenchText[i/2];
				textFields[i+1].text = frenchText[i/2];
				worldController.setCurrentLanguage ("French");
			}
			break;
		case "German":
			for(int i = 0; i < textFields.Length; i+=2) {
				textFields[i].text = germanText[i/2];
				textFields[i+1].text = germanText[i/2];
				worldController.setCurrentLanguage ("German");
			}
			break;
		case "Italian":
			for(int i = 0; i < textFields.Length; i+=2) {
				textFields[i].text = italianText[i/2];
				textFields[i+1].text = italianText[i/2];
				worldController.setCurrentLanguage ("Italian");
			}
			break;
		case "Japanese":
			for(int i = 0; i < textFields.Length; i+=2) {
				textFields[i].text = japaneseText[i/2];
				textFields[i+1].text = japaneseText[i/2];
				worldController.setCurrentLanguage ("Japanese");
			}
			break;
		case "Russian":
			for(int i = 0; i < textFields.Length; i+=2) {
				textFields[i].text = russianText[i/2];
				textFields[i+1].text = russianText[i/2];
				worldController.setCurrentLanguage ("Russian");
			}
			break;
		}
	}
}
