using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectModeController : MonoBehaviour {
	public Sprite randomModeLocked, randomModeUnlocked;
	public Sprite englishModeLocked, englishModeUnlocked;
	public Button Btn_RandomMode,Btn_EnglishMode;
	public GameObject selectModeNotif;
	public Text selectModeText;

	void Start ()
	{
		if (GameData.GilaMode.iGilaModeUnlockValue == 0 && GameData.EnglishMode.iEnglishModeUnlockValue == 0) {
			Btn_RandomMode.GetComponent<Image> ().sprite = randomModeLocked;
			Btn_EnglishMode.GetComponent<Image> ().sprite = englishModeLocked;
//			Btn_RandomMode.interactable=false;
//			Btn_EnglishMode.interactable=false;
		} else {
			if (GameData.GilaMode.iGilaModeUnlockValue > 0) {
				Btn_RandomMode.GetComponent<Image> ().sprite = randomModeUnlocked;
			}

			if (GameData.EnglishMode.iEnglishModeUnlockValue > 0) {
				Btn_EnglishMode.GetComponent<Image> ().sprite = englishModeUnlocked;
			}
		}
	}

	public void OnBtnNormalMode(){
		GameData.GilaMode.iGilaMode=0;
		GameData.EnglishMode.iEnglishMode=0;
		MenuSceneController.instance.OnEnterMode();
	}

	public void OnBtnRandomMode ()
	{
		if (GameData.GilaMode.iGilaModeUnlockValue == 0) {
			selectModeNotif.SetActive (true);
			selectModeText.text = "Selesaikan Normal Mode dahulu";
		} else {
			GameData.GilaMode.iGilaMode=1;
			GameData.EnglishMode.iEnglishMode=0;
			MenuSceneController.instance.OnEnterMode();
		}
	}

	public void OnBtnEnglishMode ()
	{
		if (GameData.EnglishMode.iEnglishModeUnlockValue == 0) {
			selectModeNotif.SetActive(true);
			selectModeText.text = "Selesaikan Normal Mode dan Random Mode dahulu";
		} else {
			GameData.GilaMode.iGilaMode=0;
			GameData.EnglishMode.iEnglishMode=1;
			MenuSceneController.instance.OnEnterMode();
		}
	}
}
