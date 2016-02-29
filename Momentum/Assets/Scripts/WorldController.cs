using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
    private static WorldController instance;
    public AudioManager audioManager;
	public AnimationManager animationManager;

	public GameObject hudCanvas;
	public GameObject optionsMenu;

	//Pause handler
	public bool showingOptions;
	public bool isPaused = false;
	public bool pDown = false;
	private bool pUp = false;

    public static WorldController getInstance()
    {
        if (instance == null)
        {
            instance = new WorldController();
        }
        return instance;
    }

	public SceneChanger sceneChanger;

	private string currentLang = "English";

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	void OnLevelWasLoaded(int level) {
		if(level == 1) {
			//optionsMenu.GetComponent<TextManager> ().setLanguage (currentLang);
		}
	}

	public void setCurrentLanguage(string newLang) {
		currentLang = newLang;
	}
//	void Update() {
//		if (Input.GetKeyDown (KeyCode.P)) {
//			if (!isPaused) {
//				if (hudCanvas != null) {
//					if (!pDown) {
//						for (int i = 0; i < hudCanvas.transform.childCount; i++) {
//							bool isPauseMenu = false;
//							if (hudCanvas.transform.GetChild (i).name == "InGameMenu") {
//								isPauseMenu = true;
//							}
//							hudCanvas.transform.GetChild (i).gameObject.SetActive (isPauseMenu);
//						}
//					} else {
//						pDown = true;
//					}
//				}
//				isPaused = true;
//			} else if (isPaused && !optionsMenu.activeInHierarchy) {
//				if (hudCanvas != null) {
//					if (!pDown) {
//						for (int i = 0; i < hudCanvas.transform.childCount; i++) {
//							bool isPauseMenu = false;
//							if (hudCanvas.transform.GetChild (i).name == "InGameMenu") {
//								isPauseMenu = true;
//							}
//							hudCanvas.transform.GetChild (i).gameObject.SetActive (!isPauseMenu);
//						}
//					} else {
//						pDown = true;
//					}
//				}
//				optionsMenu.SetActive(false);
//				isPaused = false;
//			}
//			if (Input.GetKeyUp (KeyCode.P)) {
//				pDown = false;
//			}
//		}
//	}

}
