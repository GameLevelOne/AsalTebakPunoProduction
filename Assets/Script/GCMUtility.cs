using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GCMUtility : MonoBehaviour {
	public static GCMUtility self;

	private string[] SENDER_IDS = {"698552919952"};
	const string GAMEID = "PUNO";

	bool bRegistered = false;
	float f = 0;
	void Awake(){
		self = this;
		//DontDestroyOnLoad(this.gameObject);
	}

	void Start () {
		f = 0;
		// Create receiver game object
		GCM.Initialize ();

		// Set callbacks
		GCM.SetErrorCallback ((string errorId) => {
			Debug.Log ("Error!!! " + errorId);
		});

		GCM.SetMessageCallback ((Dictionary<string, object> table) => {
			Debug.Log ("Message!!!");
		});

		GCM.SetRegisteredCallback ((string registrationId) => {
			Debug.Log ("Registered successfully with registration id: " + registrationId);
		});

		GCM.SetUnregisteredCallback ((string registrationId) => {
			Debug.Log ("Unregistered!!! " + registrationId);
		});

		GCM.SetDeleteMessagesCallback ((int total) => {
			Debug.Log ("DeleteMessages!!! " + total);
		});

		RegisterGCM();
	}

	void Update(){
		if(bRegistered == false){
			if(GCM.IsRegistered() == true){
				RegisterDeviceID(GCM.GetRegistrationId());
				bRegistered = true;
			}
		}

	}

	public void RegisterGCM(){
		Debug.Log("Registering GCM");
		GCM.Register (SENDER_IDS);
	}

	public void RegisterDeviceID(string RegistrationID){
		Debug.Log ("Sending Registration ID to Database");
		string url = "http://api.gemugemu.com/apinotif/register.php?idgame="+GAMEID+"&deviceid="+RegistrationID;
		print (url);
		WWW www = new WWW(url);

		StartCoroutine(response(www));
	}

	IEnumerator response(WWW www){
		yield return www;
		print ("request sent!");
		Debug.Log(www.text);
	}
}
