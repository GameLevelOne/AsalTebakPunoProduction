using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppsFlyerController : MonoBehaviour {
	public static AppsFlyerController instance;

	void Awake(){
		instance = this;
		Debug.Log("appsflyercontroller");
	}

	void Start(){
		//startUp ();
	}

	public void startUp(){

		#if UNITY_IOS 

//		AppsFlyer.setAppsFlyerKey ("YOUR_APPSFLYER_DEV_KEY_HERE");
//		AppsFlyer.setAppID ("YOUR_APPLE_APP_ID_HERE");
//		AppsFlyer.setIsDebug (true);
//		AppsFlyer.getConversionData ();
//		AppsFlyer.trackAppLaunch ();

		#elif UNITY_ANDROID

		// if you are working without the manifest, you can initialize the SDK programatically.
		AppsFlyer.init ("o67gJkTD8G2qTWdLXxKb77");
		AppsFlyer.setIsDebug(true);

		// un-comment this in case you are not working with the android manifest file
		AppsFlyer.setAppID ("com.gemugemu.puno"); 

		// for getting the conversion data
		AppsFlyer.loadConversionData("StartUp","didReceiveConversionData", "didReceiveConversionDataWithError");

		#endif
	}

	public void _trackRichEvent(string keyEvent, string valueEvent, string eventName){
//		Dictionary<string,string> theEvent = new Dictionary<string,string>();
//		theEvent.Add(keyEvent,valueEvent);
//		AppsFlyer.trackRichEvent(eventName,theEvent);
	}

	public void didReceiveConversionData(string conversionData) {
		print ("AppsFlyerTrackerCallbacks:: got conversion data = " + conversionData);
		if (conversionData.Contains ("Non")) {
			print("Non-Organic Install");
		} else {
			print("Organic Install");
		}	
	}
}
