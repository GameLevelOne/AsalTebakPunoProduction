using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AnswerLetterController : MonoBehaviour {
	public int charIndex;

	void Start(){
		//print ("charIndex : " + charIndex);
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			if (EventSystem.current.IsPointerOverGameObject ()) {
				if(EventSystem.current.currentSelectedGameObject == this.gameObject){
					if(AnswerGenerator.instance.AnswerChars[charIndex].GetComponent<Text>().text != ""){
						GameData._charIndex = charIndex;
						CursorController.instance.UpdateCursorPosition (CursorMoveType.SELECTED);
					}
				}
			}
		}
	}
}
