using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardController : MonoBehaviour {
	public static KeyboardController instance;
	public Button submitButton;
	public AudioSource sfxGame3;
	public AudioClip sfxButton;

	void Awake(){
		instance = this;
	}

	public void OnAlphabetButton(string alphabet){
		//GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame3,sfxButton);
		if(CursorController.instance.waitingAnimation == false){
			AnswerContainer.instance.UpdateAnswer(alphabet, CursorMoveType.FORWARD);
		}
	}

	public void OnEraseButton(){
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame3,sfxButton);
		if (CursorController.instance.waitingAnimation == false) {
			AnswerContainer.instance.UpdateAnswer ("_", CursorMoveType.BACKWARD);
		}
	}

	public void OnSubmitButton(){
		//submit answer

		//combine chars to string
		//check answer

		//0 - 79% = Wrong
		//80 - 99% = Almost
		//100% = true
		GameData.soundSourceAnotherGO (GameData.SFX3_SOUNDSOURCE,sfxGame3,sfxButton);
		AnswerContainer.instance.CheckAnswer();
		submitButton.gameObject.SetActive (false);
		//submitButton.enabled = false;
	}

}
