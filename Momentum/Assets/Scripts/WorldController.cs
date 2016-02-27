using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
    private static WorldController instance;
    public AudioManager audioManager;
	public AnimationManager animationManager;
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

}
