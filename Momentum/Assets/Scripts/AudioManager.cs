using UnityEngine;
using System.Collections;
using System.IO;

public class AudioManager : MonoBehaviour {

	public AudioSource[] hitSounds = new AudioSource[3];
	private int hitSoundMarker = 0;

	public AudioSource[] pullSounds = new AudioSource[4];
	private int pullSoundMarker = 0;

	public AudioSource[] punchSounds = new AudioSource[5];
	private int punchSoundMarker = 0;

	public AudioSource[] walkSounds = new AudioSource[2];
	private int walkSoundMarker = 0;

	public void playSound(string hitType) {
		switch(hitType) {
		case "hit":
			hitSounds [hitSoundMarker].Play ();
			hitSoundMarker++;
			if(hitSoundMarker == hitSounds.Length) {
				hitSoundMarker = 0;
			}
			break;
		case "pull":
			pullSounds [pullSoundMarker].Play ();
			pullSoundMarker++;
			if(pullSoundMarker == pullSounds.Length) {
				pullSoundMarker = 0;
			}
			break;
		case "punch":
			punchSounds [punchSoundMarker].Play ();
			punchSoundMarker++;
			if(punchSoundMarker == 3) {
				punchSoundMarker = 0;
			}
			break;
		case "walk":
			walkSounds [walkSoundMarker].Play ();
			walkSoundMarker++;
			if(walkSoundMarker == walkSounds.Length) {
				walkSoundMarker = 0;
			}
			break;
		default:
			Debug.Log ("Specificied an invalid sound category");
			break;
		}


			
			

}
}
