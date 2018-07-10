using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LINEErrorNotif : MonoBehaviour {
	public static LINEErrorNotif instance;

	public Text txtNotif;

	void Awake(){
		instance=this;
	}

	public void setNotifText(string txt){
		txtNotif.text=txt;
	}

}
