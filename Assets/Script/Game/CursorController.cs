using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CursorController : MonoBehaviour {
	public static CursorController instance; 

	public RectTransform CursorObj;

	//answer char components
	public RectTransform[] CharPos;
	public string[] CharValues;
	
	public int CursorPositionIndex;
	public bool waitingAnimation;
	int Ans_Length;

	public Button ButtonSubmit;


	void Awake(){
		instance = this;

		CursorPositionIndex = 0;
	}

	#region PUBLIC TOOLS
	public void ResetPosition(){
		CursorPositionIndex = 0;
		CursorObj.anchoredPosition = new Vector2(-300f,20f);
		CharPos = null;
		CharValues = null;
	}

	public void SetCharValues(GameObject[] TextObjs){
		Ans_Length = TextObjs.Length;

		CharPos 	= new RectTransform[Ans_Length];
		CharValues 	= new string[Ans_Length];

		for(int i = 0; i < Ans_Length; i++){
			CharPos[i] = TextObjs[i].GetComponent<RectTransform>();
			CharValues[i] = TextObjs[i].GetComponent<Text>().text;
		}
	}

	public void UpdateCursorPosition(CursorMoveType type){

		//cursor is in the last index AND the letter in that index is empty -> nothing
		//cursor is in the last index -> nothing
		//otherwise do function
		//same goes with first index

		if(type == CursorMoveType.FORWARD){
			if (CursorPositionIndex == (Ans_Length - 1) && CharValues [Ans_Length - 1] == "_") {
			} //do nothing
			else if (CursorPositionIndex == (Ans_Length - 1)) {
			} //do nothing
			else {
				//CursorPositionIndex = IndexForward (CursorPositionIndex, CharValues [CursorPositionIndex + 1]);
				CursorPositionIndex = IndexForward(CursorPositionIndex);
			}
		}
		else if(type == CursorMoveType.BACKWARD){
			if (CursorPositionIndex == 0 && CharValues [0] != "_") {
			} //do nothing
			else if (CursorPositionIndex == 0) {
			} //do nothing
			else {
				//CursorPositionIndex = IndexBackward (CursorPositionIndex, CharValues [CursorPositionIndex - 1]);
				CursorPositionIndex = IndexBackward(CursorPositionIndex);
			}
		}else if(type == CursorMoveType.SELECTED){
			CursorPositionIndex = GameData._charIndex;
		}

		MoveCursor(CharPos[CursorPositionIndex]);
	} 

	public void ShowHideSubmitButton(){//int answerLength, string charValue,
		if((GameData._letterChecked == GameData._totalLetters) && waitingAnimation == false){ //(CursorPositionIndex == answerLength - 1 && charValue != "_") && 
			if(!ButtonSubmit.gameObject.activeSelf) ButtonSubmit.gameObject.SetActive(true);
		}else{
			if(ButtonSubmit.gameObject.activeSelf) ButtonSubmit.gameObject.SetActive(false);
		}
	}
	#endregion

	#region PRIVATE TOOLS
//	private int IndexForward(int currentIndex, string value){
//
//		//IF cursor is in the last index -> return the index
//		//IF character next to cursor is 'space' return the index + 2
//		//Otherwise, return the next index
//
//		if (currentIndex == (Ans_Length - 1)) {
//			return currentIndex;
//		}else{
//			if (string.IsNullOrEmpty (value) || value == " ") {
//				return currentIndex + 2;
//			}else{
//				return currentIndex + 1;
//			}
//		}
//	}

	private int IndexForward(int currentIndex){
		if (currentIndex == (Ans_Length - 1)) {
			return currentIndex;
		} else {
			while (string.IsNullOrEmpty (CharValues [currentIndex + 1]) || CharValues [currentIndex + 1] == " ") {
				currentIndex += 1;
			}

			if (CharValues[currentIndex] != " ") {
				currentIndex += 1;
			}
		}

		return currentIndex;
	}

//	private int IndexBackward(int currentIndex, string value){
//
//		//IF cursor is in the first index -> return the index
//		//IF character before to cursor is 'space' return the index - 2
//		//Otherwise, return the previous index
//
//		if (currentIndex == 0) {
//			return currentIndex;
//		}
//		else{
//			if (string.IsNullOrEmpty (value) || value == " ") {
//				return currentIndex - 2;
//			} else {
//				return currentIndex - 1;
//			}
//		}
//	}

	private int IndexBackward(int currentIndex){
		if (currentIndex == 0) {
			return currentIndex;
		} else {
			while (string.IsNullOrEmpty (CharValues [currentIndex - 1]) || CharValues [currentIndex - 1] == " ") {
				currentIndex -= 1;
			}

			if (CharValues[currentIndex] != " ") {
				currentIndex -= 1;
			}
		}

		return currentIndex;
	}

	private void MoveCursor(RectTransform Destination){
		try{
			CursorObj.anchoredPosition = Destination.anchoredPosition;
		}catch{
			Debug.Log ("it's okay");
		}
	}
	#endregion

}
