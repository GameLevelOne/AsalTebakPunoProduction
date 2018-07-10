using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class EventBannerChecker : MonoBehaviour {
	public static EventBannerChecker self;


	public string[] bannerURL = new string[2];
	public string[] bannerLink = new string[2];

	void Awake(){
		self = this;
		GameData.iBannerCount = 0;
		GameData.iPosterHasShown = 0;
		GameData.BannerCode[0] = 0;
		GameData.BannerCode[1] = 0;
		Debug.Log("eventbannerchecker");
	}

	public void RequestBanner(){
		string url = "http://api.gemugemu.com/api/banner/getbanner.php?idgame="+GameData.GAMEID;
		WWW www = new WWW(url);

		StartCoroutine(ApiResponse(www));
	}

	IEnumerator ApiResponse (WWW www)
	{
		yield return www;
		GameData.iBannerCount = 0;

		if (www.error == null) {
			Debug.Log (www.text);
			JSONNode data = JSON.Parse (www.text);

			string tempBannerTextureURL1 = "";
			string tempBannerTextureURL2 = "";

			//Get link from API (Image Link) 1
			tempBannerTextureURL1 = data ["banner1"];
			if (tempBannerTextureURL1 == "null") {
				//nothing
				Debug.Log("banner 1 null");
				yield return null;
			} else {
				GameData.iBannerCount++;

				bannerURL [0] = tempBannerTextureURL1;
				bannerLink [0] = data ["banner1_url"];

				if (string.IsNullOrEmpty (bannerLink [0]) == true) {
					GameData.BannerCode [0] = 2;
				} else {
					GameData.BannerCode[0] = 1;
				}

				WWW imgurlreq = new WWW(bannerURL[0]);
				StartCoroutine(ApiImgResponse(imgurlreq, 0));
			}

			//Get link from API (Image Link) 2
			tempBannerTextureURL2 = data["banner2"];
			if(tempBannerTextureURL2 == "null"){
				//nothing
				Debug.Log("banner 2 null");
				yield return null;
			}else{
				GameData.iBannerCount ++;
				bannerURL[1] = tempBannerTextureURL2;
				bannerLink[1] = data["banner2_url"];

				if (string.IsNullOrEmpty (bannerLink [1]) == true) {
					GameData.BannerCode [1] = 2;
				} else {
					GameData.BannerCode[1] = 1;
				}

				WWW imgurlreq = new WWW(bannerURL[0]);
				StartCoroutine(ApiImgResponse(imgurlreq, 1));
			}

		}else{
			GameData.iBannerCount = 0;
			Debug.Log("error: "+www.error);
		}
		GameData.bhasBannerResponse = true;
		//StartCoroutine(GameData.waitingToChangeScene(GameData.Scene_Menu));
		Debug.Log("done checking banner");
	}

	IEnumerator ApiImgResponse(WWW ImgUrl, int idx){
		yield return ImgUrl;
		print(ImgUrl.error);
		if(ImgUrl.error == null){
			GameData.BannerTexture[idx] = ImgUrl.texture;
			Debug.Log (GameData.BannerTexture [idx].ToString ());
		}else{
			Debug.Log("error: "+ImgUrl.error);
		}
	}

}

/*
 * {
 * banner1:"http://api.gemugemu.com/api/banner/banner1.jpg",
 * banner1_url:"",
 * banner2:"http://api.gemugemu.com/api/banner/banner2.jpg",
 * banner2_url:"http://www.damniloveindonesia.com"
 * }
 */ 
