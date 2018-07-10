using UnityEngine;
using System.Collections;

public class KeyboardSoundBtn : MonoBehaviour {
	public AudioSource sfxGame3;
	public AudioClip sfxButton;

	void OnMouseDown(){
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame3,sfxButton);
	}
}
