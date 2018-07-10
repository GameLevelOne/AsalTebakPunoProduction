using UnityEngine;
using System.Collections;
using System.Net;

public class CheckingInternetConnection : MonoBehaviour {
	public static CheckingInternetConnection instance;

	void Awake(){
		instance = this;
		Debug.Log("checkinginternetconnection");
	}

	public void checkInternetConnection(){
		//print ("CHECKING CONNECTION");
		WWW www = new WWW ("https://www.google.co.id/");

		StartCoroutine (netResponse(www));
	}

	IEnumerator netResponse(WWW www){
		//print ("DO CHECKING");
		yield return www;

		if (www.error == null) {
			GameData._isConnected = true;
			//GoogleMobileAdsDemoScript.instance.RequestInterstitial ();
			//print ("IS CONNECTED");
		} else {
			GameData._isConnected = false;
			//print ("IS NOT CONNECTED");
		}
	}
}
