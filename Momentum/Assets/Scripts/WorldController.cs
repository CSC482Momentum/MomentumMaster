using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
    private static WorldController instance;
    public AudioManager audioManager;
	public AnimationManager animationManager;

	public GameObject pauseMenu;
	public GameObject pauseOptionsMenu;
	public GameObject hudCanvas;

	//Pause handler
	private bool showingOptions;
	private bool isPaused = false;
	private bool pDown = false;
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

	public string currentLang = "English";

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	void OnLevelWasLoaded(int level) {
		if(level == 1) {
			GameObject g = Instantiate (pauseMenu);
			g.SetActive(false);
			pauseMenu = g;
			pauseMenu.transform.GetChild (0).GetComponent<TextManager> ().worldController = this;
			pauseMenu.transform.GetChild (0).GetComponent<TextManager> ().setLanguage (currentLang);
			hudCanvas = GameObject.Find ("HUD_Canvas");
		}
	}

	public void setCurrentLanguage(string newLang) {
		currentLang = newLang;
	}
	void Update() {
		if (hudCanvas == null) {
			hudCanvas = GameObject.Find ("HUD_Canvas");
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			hudCanvas.SetActive (false);
			if (!isPaused) {
				pauseMenu.SetActive (true);
				if (sceneChanger.loadedScene == 1) {
					if (!pDown) {
						for (int i = 0; i < pauseMenu.transform.childCount; i++) {
							bool isPauseMenu = false;
							if (pauseMenu.transform.GetChild (i).name == "InGameMenu") {
								isPauseMenu = true;
							}
							pauseMenu.transform.GetChild (i).gameObject.SetActive (isPauseMenu);
						}
					} else {
						pDown = true;
					}
				}
				isPaused = true;
			} else if (isPaused /*&& !pauseOptionsMenu.activeInHierarchy*/) {
				pauseMenu.SetActive (false);
				hudCanvas.SetActive (true);
				if (sceneChanger.loadedScene == 1) {
					if (!pDown) {
						for (int i = 0; i < pauseMenu.transform.childCount; i++) {
							bool isPauseMenu = false;
							if (pauseMenu.transform.GetChild (i).name == "InGameMenu") {
								isPauseMenu = true;
							}
							pauseMenu.transform.GetChild (i).gameObject.SetActive (!isPauseMenu);
						}
					} else {
						pDown = true;
					}
				}
				//pauseOptionsMenu.SetActive(false);
				isPaused = false;
			}
			if (Input.GetKeyUp (KeyCode.P)) {
				pDown = false;
			}
		}
	}

}
