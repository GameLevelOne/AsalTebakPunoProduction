using UnityEngine;
using System.Collections;
using SimpleJSON;
using System;

public class DateTimeController : MonoBehaviour {
	DateTime timeNow;

	public void GetTime(){
		//StartCoroutine(DoTheThing());
	}
	
	private IEnumerator DoTheThing(){
		//post request
		string postURL = "http://api.timezonedb.com/?zone=Europe/London&format=json&key=A9DTMGUDV74U";
		WWW www = new WWW(postURL);
		
		//catch result
		yield return www;
		
		if(www.error == null){
			//parse timestamp (unix timestamp)
			//print (www.text);
			JSONNode data = JSON.Parse(www.text);
			double timeStamp = data["timestamp"].AsDouble;
			
			DateTime epochTime = new DateTime(1971,1,1,0,0,0,DateTimeKind.Utc);
			timeNow = epochTime.AddSeconds(timeStamp).ToLocalTime();

		}else{
			//no connection / internet error
//			PlayerPrefs.SetInt(GameData.Key_InternetConnection,0);
			//hide prize
		}
	}
}
