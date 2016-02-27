using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	public AudioManager audioManager;
	public AnimationManager animationManager;
	public SceneChanger sceneChanger;

	private string currentLang = "English";

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
