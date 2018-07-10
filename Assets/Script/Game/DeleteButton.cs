using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DeleteButton : MonoBehaviour {
	public KeyboardController keyboardController;
	public Object obj_deleteBtn;
	private float timer;
	private bool holdBtn;

	void Start(){
		timer = 0.5f;
		holdBtn = false;
	}

	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if (EventSystem.current.IsPointerOverGameObject ()) {
				if (EventSystem.current.currentSelectedGameObject == obj_deleteBtn) {
					keyboardController.OnEraseButton ();
				}
			}
		}else if(holdBtn == true){
			keyboardController.OnEraseButton ();
		}

		if(Input.GetMouseButton(0)){
			if (EventSystem.current.IsPointerOverGameObject ()) {
				if (EventSystem.current.currentSelectedGameObject == obj_deleteBtn) {
					StartCoroutine ("IsHoldBtn");
				}
			}
		}else if(Input.GetMouseButtonUp(0)){
			if (EventSystem.current.IsPointerOverGameObject ()) {
				if (EventSystem.current.currentSelectedGameObject == obj_deleteBtn) {
					StopCoroutine ("IsHoldBtn");
					holdBtn = false;
				}
			}
		}
	}

	IEnumerator IsHoldBtn(){
		yield return new WaitForSeconds (timer);
		holdBtn = true;
	}
}
