using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public class ServerTimeManager : MonoBehaviour {
	public static ServerTimeManager self;

	DateTime timeNow;
	DayOfWeek day;

	void Awake(){
		self = this;
//		DontDestroyOnLoad(this.gameObject);
		GameData._bHasGetRecentServerTime = false;
		Debug.Log("servertimemanager");
	}

	public void RequestServerTime(){
		WWW www = new WWW("http://api.timezonedb.com/?zone=Europe/London&format=json&key=A9DTMGUDV74U");
		StartCoroutine(Response(www));

//		FORTEST_RequestTime(); //FOR TEST ONLY
	}

	IEnumerator Response(WWW www){
		yield return www;
		
		if(www.error == null){
			//parse timestamp (unix timestamp)
			//			print (www.text);
			JSONNode data = JSON.Parse(www.text);
			double timeStamp = data["timestamp"].AsDouble;
			
			DateTime epochTime = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc);
			timeNow = epochTime.AddSeconds(timeStamp).ToLocalTime();
			
			
			GameData.ServerTime = timeNow;
			GameData._bHasGetRecentServerTime = true;
		}else{
			//no connection / internet error
			GameData._isConnected = false;
		}
	}

	//local time
	void FORTEST_RequestTime(){
//		Debug.Log("today is "+DateTime.Now.DayOfWeek);
//		if(DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday){
//			GL1Data.iOccuringEventPromo = 2;
//		}else{
//			if(GL1Data.iHasTopUp == 1){
//				GL1Data.iOccuringEventPromo = 0;
//			}else{
//				GL1Data.iOccuringEventPromo = 1;
//			}
//		}
		GameData.ServerTime = DateTime.Now;
	}
}
