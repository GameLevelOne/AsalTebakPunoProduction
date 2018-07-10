using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CheckedClickedObj : MonoBehaviour {
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (EventSystem.current.IsPointerOverGameObject ()) {
				Debug.Log (EventSystem.current.currentSelectedGameObject);
			}
		}
	}
}
