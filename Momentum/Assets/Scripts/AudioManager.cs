using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {



	public List<AudioSource> hitSounds = new List<AudioSource>();
	private int hitSoundMarker = 0;

	public List<AudioSource> pullSounds = new List<AudioSource>();
	private int pullSoundMarker = 0;

	public List<AudioSource> punchSounds = new List<AudioSource>();
	private int punchSoundMarker = 0;

	public List<AudioSource> walkSounds = new List<AudioSource>();
	private int walkSoundMarker = 0;

	public void playSound(string hitType) {
		switch(hitType) {
		case "hit":
			hitSounds [hitSoundMarker].Play ();
			hitSoundMarker++;
			if(hitSoundMarker == hitSounds.Count) {
				hitSoundMarker = 0;
			}
			break;
		case "pull":
			pullSounds [pullSoundMarker].Play ();
			pullSoundMarker++;
			if(pullSoundMarker == pullSounds.Count) {
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
			if(walkSoundMarker == walkSounds.Count) {
				walkSoundMarker = 0;
			}
			break;
		default:
			Debug.Log ("Specificied an invalid sound category");
			break;
		}


			
			

}
}
