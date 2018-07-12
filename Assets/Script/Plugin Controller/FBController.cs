using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Facebook.Unity;
using Facebook.MiniJSON;

public class FBController : MonoBehaviour {
	private class actionDictionary
	{
		public const int INIT = 1;
		public const int LOGIN = 2;
	}

	public delegate void LoginSuccessful();
	public delegate void GetName(string name);
	public event LoginSuccessful OnLoginSuccessful;
	public event GetName OnGetName;

	//share
	private string shareLink = "https://fb.me/1087491038010125";
	private string shareTitle = "Asal Tebak PUNO";
	private string shareDescription = "Ada yang bisa bantu ?";
	private string shareImage = "http://i.imgur.com/traBpjQ.png";

	private int lastAction;
	private string getNameData,getEmailData,getUserIdData;

	public static FBController instance;

	protected void HandleResult (IResult result)
	{
		print ("show handle result");
		if (result == null) {
			Debug.Log (result);
		}

		// Some platforms return the empty string instead of null.
		if (!string.IsNullOrEmpty (result.Error)) {
			print ("login failed 01 : " + result);
		} else if (result.Cancelled) {
			print ("login failed 02 : " + result);
		} else if (!string.IsNullOrEmpty (result.RawResult)) { //success result
			if (Application.loadedLevelName == "MenuScene") {
				// LINEController.lineControllerInstance.OnFbLoginSuccessful();
			} 
			print("FB login success");
			GameData._isLoggedIn = true;
			PlayerPrefs.SetInt("GuestLogin", 0);
			getFBName ();
			getFBEmail ();
			getFBUserID();
			if(OnLoginSuccessful != null) OnLoginSuccessful();
		}
		else {
			print("login failed 03 : " + result);
		}

		lastAction = 0;
	}

	//test
	protected void HandleShare(IResult result) {
		if (result == null) {
			Debug.Log(result);
		}

		// Some platforms return the empty string instead of null.
		if (!string.IsNullOrEmpty(result.Error)) {
			print("publish login failed 01 : " + result);
		}
		else if (result.Cancelled) {
			print("publish login failed 02 : " + result);
		}
		else if (!string.IsNullOrEmpty(result.RawResult)) { //success result
			GameObject.Find("SCENE_CONTROLLER").GetComponent<ResultPanelController>().getRewardShareScore();
			print("publish login success : " + result);
		}
		else {
			print("publish login failed 03 : " + result);
		}

		lastAction = 0;
	}
	//--------------

	void Awake ()
	{
		instance = this;

		if (!FB.IsInitialized) {
			FB.Init (InitCallback,OnHideUnity,null);
		} else {
			FB.ActivateApp ();
			if(FB.IsLoggedIn) GameData._isLoggedIn = true;
		}

		// Debug.Log("fbcontroller");
	}

	private void InitCallback ()
{
    if (FB.IsInitialized) {
        // Signal an app activation App Event
        FB.ActivateApp();
		// getFBName ();
		// getFBEmail ();
		// getFBUserID();
        // Continue with Facebook SDK
        // ...
    } else {
        Debug.Log("Failed to Initialize the Facebook SDK");
    }
}

private void OnHideUnity (bool isGameShown)
{
    // if (!isGameShown) {
    //     // Pause the game - we will need to hide
    //     Time.timeScale = 0;
    // } else {
    //     // Resume the game - we're getting focus again
    //     Time.timeScale = 1;
    // }
}

	// private void onFBInitComplete (){
	// 	// Debug.Log("fb finished init");
	// 	getFBName ();
	// 	getFBEmail ();
	// 	getFBUserID();
	// }

	public void CallFBLogin(){
		// Debug.Log("call fb login");
		if(!FB.IsInitialized){
			FB.Init (InitCallback,OnHideUnity,null);
		}

		lastAction = actionDictionary.LOGIN;
		FB.LogInWithReadPermissions (new List<string> () {
			"public_profile","email","user_friends"
		},this.HandleResult);
	}

	public void CallFBLogout(){
		// Debug.Log("call fb logout");
		FB.LogOut();
		GameData._isLoggedIn = false;
	}

	//share screenshoot baru
	public void ShareScore(int star, string world, int stage) {
		if (!FB.IsLoggedIn) {
			CallFBLogin();
		}

		if (FB.IsLoggedIn) {
			FB.ShareLink(
				new Uri(this.shareLink),
				"Saya baru saja mendapatkan " + star + " bintang di kota " + world + " soal " + (stage+1),
				"Mainkan dan ikuti petualangan PUNO",
				new Uri(this.shareImage),
				this.HandleShare
			);
		}
	}
	//---------------------------

	public void getFBName(){
		if(!FB.IsInitialized){
			FB.Init ();
		}

		if (FB.IsLoggedIn) {
			FB.API ("/me?fields=name", HttpMethod.GET, userCallBack);
		} else if(!FB.IsLoggedIn) {
			// print ("fb not login");
		}
	}
 
	public void getFBEmail(){
		if(!FB.IsInitialized){
			FB.Init ();
		}

		if (FB.IsLoggedIn) {
			FB.API ("/me?fields=email", HttpMethod.GET, emailCallBack);
		} else if(!FB.IsLoggedIn) {
			// print ("fb not login");
		}
	}

	public void getFBUserID (){
		if(!FB.IsInitialized){
			FB.Init ();
		}

		if (FB.IsLoggedIn) {
			FB.API ("/me?fields=id", HttpMethod.GET, userIdCallback);
		} else if(!FB.IsLoggedIn) {
			// print ("fb not login");
		}
	}

	private void userCallBack(IGraphResult result){
		if (result.Error != null) {
			print ("userCallBack (failed) : " + result);
			getNameData = result.RawResult;
		} else {
			print ("userCallBack (success) : " + result);
			getNameData = result.RawResult;
		}
		var dict = Json.Deserialize (getNameData) as IDictionary;
		PlayerPrefs.SetString (GameData.Key_fbname,dict ["name"].ToString ());
		GameData.loginUserNameValue=dict ["name"].ToString ();
		if(OnGetName != null) OnGetName(GameData.loginUserNameValue);
		//GameData._fbName = dict ["name"].ToString ();
	}

	private void emailCallBack(IGraphResult result){
		if (result.Error != null) {
			print ("emailCallBack (failed) : " + result);
			getEmailData = result.RawResult;
		} else {
			print ("emailCallBack (success) : " + result);
			getEmailData = result.RawResult;
		}
		var dict = Json.Deserialize (getEmailData) as IDictionary;
		PlayerPrefs.SetString (GameData.Key_fbemail,dict ["email"].ToString ());
	}

	private void userIdCallback(IGraphResult result){
		if (result.Error != null) {
			print ("userIdCallback (failed) : " + result);
			getUserIdData = result.RawResult;
		} else {
			print ("userIdCallback (success) : " + result);
			getUserIdData = result.RawResult;
		}
		var dict = Json.Deserialize (getUserIdData) as IDictionary;
		PlayerPrefs.SetString (GameData.Key_fbuserid,dict ["id"].ToString ());
		Debug.Log("FB user-id: "+dict["id"]);
	}
}
