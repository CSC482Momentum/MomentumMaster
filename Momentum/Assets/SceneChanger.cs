using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {


	public int loadedScene = 0;

	public void loadTutorial() {
		SceneManager.LoadScene ("Tutorial");
		loadedScene = 2;
	}

	public void loadCubedDonut() {
		SceneManager.LoadScene ("CubedDonut");
		loadedScene = 1;
	}

	public void loadQuit() {
		Application.Quit ();
	}
		
}
