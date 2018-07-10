using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class TestBannerAds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RequestBanner();
	}
	
	void RequestBanner(){
//		#if UNITY_EDITOR
//	        string adUnitId = "unused";
//	   	#elif UNITY_ANDROID
//		string adUnitId = "ca-app-pub-3940256099942544/2934735716";
//	    #elif UNITY_IPHONE
//	        string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
//	    #else
//	        string adUnitId = "unexpected_platform";
//	    #endif

		string adUnitId = "ca-app-pub-3940256099942544/2934735716"; //test id

	    BannerView bannerAd = new BannerView(adUnitId,AdSize.Banner,AdPosition.Bottom);
	    AdRequest adRequest = new AdRequest.Builder().Build();
	    bannerAd.LoadAd(adRequest);
	}
}
