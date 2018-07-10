using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {
	public static PauseMenuController instance;

	public GameObject UI_Pause,
					  musik_on, musik_off,
					  suara_on, suara_off;
	
	void Awake(){
		instance = this;
	}

	public void ShowPauseMenu(){
		UI_Pause.SetActive(true);
	}

	public void HidePauseMenu(){
		UI_Pause.SetActive(false);
	}

	public void SetMusik(){
		if(musik_on.activeSelf){
			musik_on.SetActive(false);
			musik_off.SetActive(true);
		}
		else if(!musik_on.activeSelf){
			musik_on.SetActive(true);
			musik_off.SetActive(false);
		}
	}

	public void SetSuara(){
		if(suara_on.activeSelf){
			suara_on.SetActive(false);
			suara_off.SetActive(true);
		}
		else if(!suara_on.activeSelf){
			suara_on.SetActive(true);
			suara_off.SetActive(false);
		}
	}


}
