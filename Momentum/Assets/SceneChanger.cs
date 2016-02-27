using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {


	public void loadTutorial() {
		SceneManager.LoadScene ("Tutorial");
	}

	public void loadCubedDonut() {
		SceneManager.LoadScene ("CubedDonut");
	}

	public void loadQuit() {
		Application.Quit ();
	}
		
}
