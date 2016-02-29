using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
    private static WorldController instance;
    public AudioManager audioManager;
	public AnimationManager animationManager;

	public GameObject pauseMenu;
	public GameObject pauseOptionsMenu;

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
		}
	}

	public void setCurrentLanguage(string newLang) {
		currentLang = newLang;
	}
	void Update() {
		if (Input.GetKeyDown (KeyCode.P)) {
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
