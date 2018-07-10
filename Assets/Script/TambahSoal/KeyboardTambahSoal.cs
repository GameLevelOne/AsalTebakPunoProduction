using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class KeyboardTambahSoal : MonoBehaviour {
	public Text inputField_soal,inputField_jawaban,maxSoalText,maxJawabanText;
	public Object obj_soal, obj_jawaban;
	public AudioSource sfxMenu, sfxMenu2, sfxMenu3;
	public AudioClip sfxButton;
	private Object objectName;
	private string tempGetString,tempCurString,tempNewString;
	private bool shiftOn = false;
	private int totalInputSoal,totalInputJawaban,maxText;

	public static KeyboardTambahSoal instance;

	void Start(){
		maxText = 130;

		//biar tempCurString punya index 
		inputField_soal.text = " ";
		inputField_jawaban.text = " ";
		//------------------------------
		totalInputSoal = 0;
		totalInputJawaban = 0;
		maxSoalText.text = totalInputSoal.ToString () + " / " + maxText;
		maxJawabanText.text = totalInputJawaban.ToString () + " / " + maxText;
	}

	private void objectClickedCheck(){
		if(Input.GetMouseButtonDown(0)){
			if (EventSystem.current.IsPointerOverGameObject ()) {
				Debug.Log (EventSystem.current.currentSelectedGameObject);
				if(EventSystem.current.currentSelectedGameObject == obj_soal || EventSystem.current.currentSelectedGameObject == obj_jawaban){
					objectName = EventSystem.current.currentSelectedGameObject;
					if(objectName == obj_soal){
						EraseCursor (inputField_jawaban);
						GetLastText (inputField_soal);
					}else if(objectName == obj_jawaban){
						EraseCursor (inputField_soal);
						GetLastText (inputField_jawaban);
					}
				}
			}
		}
	}

	private int UpdateMaxText(CursorMoveType type){
		print ("obj : " + objectName);
		if ((tempCurString.Length >= 0 && tempCurString.Length < 130) && objectName == obj_soal) {
			if (type == CursorMoveType.FORWARD) {
				totalInputSoal++;
			} else if (type == CursorMoveType.BACKWARD) {
				totalInputSoal--;
			}
			return totalInputSoal;
		} else if ((tempCurString.Length >= 0 && tempCurString.Length < 130) && objectName == obj_jawaban) {
			if (type == CursorMoveType.FORWARD) {
				totalInputJawaban++;
			} else if (type == CursorMoveType.BACKWARD) {
				totalInputJawaban--;
			}
			return totalInputJawaban;
		} else {
			print ("test");
			return 0;
		}
	}

	private void GetLastText(Text inputField){
		tempCurString = inputField.text;
		print (tempCurString.Length);
		if(tempCurString[tempCurString.Length-1].ToString() != "_"){
			if (tempCurString == " ") {
				inputField.text = "_";
			} else {
				inputField.text = tempCurString + "_";
			}
		}
	}

	public void EraseCursor(Text inputField){
		tempCurString = inputField.text;
		print (inputField.name + " : " + tempCurString.Length);
		if(tempCurString.Length != 0 && tempCurString[tempCurString.Length-1].ToString() == "_"){
			tempCurString = tempCurString.Substring (0, tempCurString.Length - 1);
		}

		if(tempCurString == ""){
			tempCurString = " ";
		}
		inputField.text = tempCurString;
	}

	private void UpdateAddText(string key,Text inputField){
		tempCurString = inputField.text;
		tempCurString = tempCurString.Substring (0, tempCurString.Length - 1);
		tempNewString = tempCurString + key + "_";
		inputField.text = tempNewString;
	}

	private void UpdateDelText(Text inputField){
		if (tempGetString.Length > 0) {
			tempCurString = tempGetString.Substring (0, tempGetString.Length - 2);
			tempCurString = tempCurString + "_";
			inputField.text = tempCurString;
		}
	}

	public void inputText(string key){
		if(shiftOn){
			key = key.ToUpper ();
		}

		if(objectName == obj_soal){
			UpdateAddText (key,inputField_soal);
			maxSoalText.text = UpdateMaxText (CursorMoveType.FORWARD).ToString() + " / " + maxText;
		}else if(objectName == obj_jawaban){
			UpdateAddText (key,inputField_jawaban);
			maxJawabanText.text = UpdateMaxText (CursorMoveType.FORWARD).ToString() + " / " + maxText;
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxMenu, sfxButton);
	}

	public void deleteText(){
		string tempString;

		try{
			if (objectName == obj_soal) {
				tempGetString = inputField_soal.text;
				UpdateDelText (inputField_soal);
				maxSoalText.text = UpdateMaxText (CursorMoveType.BACKWARD).ToString() + " / " + maxText;
			} else if (objectName == obj_jawaban) {
				tempGetString = inputField_jawaban.text;
				UpdateDelText (inputField_jawaban);
				maxJawabanText.text = UpdateMaxText (CursorMoveType.BACKWARD).ToString() + " / " + maxText;
			}
			GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxMenu, sfxButton);
		}catch{
			print ("it's okay");
		}
	}

	public void pressShift(){
		shiftOn = !shiftOn;
	}

	void Update(){
		objectClickedCheck ();
	}
}
