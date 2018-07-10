using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventPosterController : MonoBehaviour {
	public static EventPosterController self;

	public RawImage[] ImgPoster = new RawImage[2];
	public GameObject[] Btn_Shop = new GameObject[2];

	public bool bWaitTexture1 = false;
	public bool bWaitTexture2 = false;

	void Awake(){
		self = this;
	}

	public void ValidateBannerCount(){

		if (string.IsNullOrEmpty (EventBannerChecker.self.bannerURL [0]) == true) {
			bWaitTexture1 = true;
		} else {
			//nothing
		}

		if (string.IsNullOrEmpty (EventBannerChecker.self.bannerURL [1]) == true) {
			bWaitTexture2 = true;
		} else {
			//nothing
		}
	}

	void Update(){
		if(bWaitTexture1 == false){
			if(GameData.BannerTexture[0] != null){
				bWaitTexture1 = true;
				ImgPoster[0].texture = GameData.BannerTexture[0];
			}
		}
		if(bWaitTexture2 == false){
			if(GameData.BannerTexture[1] != null){
				bWaitTexture2 = true;
				ImgPoster[1].texture = GameData.BannerTexture[1];
			}
		}
	}

	public void Show(){
		ValidateBannerCount ();
		Debug.Log ("poster = "+GameData.iPosterHasShown);
		if(GameData.iPosterHasShown == 0){
			GameData.iPosterHasShown = 1;

			if (GameData.iBannerCount != 0) {
				if(!ImgPoster[0].gameObject.activeSelf) ImgPoster[0].gameObject.SetActive(true); //show poster IMG if exist
				if (string.IsNullOrEmpty (EventBannerChecker.self.bannerLink [0]) == true) { //shop
//					if (ImgPoster [0].GetComponent<Button> ().enabled == true) ImgPoster [0].GetComponent<Button> ().enabled = false;
//					if (!Btn_Shop [0].activeSelf) Btn_Shop [0].SetActive (true);
					if(ImgPoster[0].gameObject.activeSelf) ImgPoster[0].gameObject.SetActive(false);
				} else { //link
					if (ImgPoster [0].GetComponent<Button> ().enabled == false) ImgPoster [0].GetComponent<Button> ().enabled = true;
					if (Btn_Shop [0].activeSelf) Btn_Shop [0].SetActive (false);
				}

				if (GameData.iBannerCount == 2) {
					if(!ImgPoster[1].gameObject.activeSelf) ImgPoster[1].gameObject.SetActive(true); //show poster IMG if exist
					if (string.IsNullOrEmpty (EventBannerChecker.self.bannerLink [1]) == true) { //shop
//						if (ImgPoster [1].GetComponent<Button> ().enabled == true) ImgPoster [1].GetComponent<Button> ().enabled = false;
//						if (!Btn_Shop [1].activeSelf) Btn_Shop [1].SetActive (true);
						if(ImgPoster[1].gameObject.activeSelf) ImgPoster[1].gameObject.SetActive(false);
					} else { //link
						if (ImgPoster [1].GetComponent<Button> ().enabled == false) ImgPoster [1].GetComponent<Button> ().enabled = true;
						if (Btn_Shop [1].activeSelf) Btn_Shop [1].SetActive (false);
					}
				}

			} else {
				for(int i = 0;i<2;i++) if(ImgPoster[i].gameObject.activeSelf) ImgPoster[i].gameObject.SetActive(false);
			}
		}else{
			for(int i = 0;i<2;i++) if(ImgPoster[i].gameObject.activeSelf) ImgPoster[i].gameObject.SetActive(false);
		}
	}

	public void ShowIapBanner (){
		if(GameData.BannerCode[0] == 2){ //shop
			if(!ImgPoster[0].gameObject.activeSelf) ImgPoster[0].gameObject.SetActive(true);
			if(Btn_Shop[0].activeSelf) Btn_Shop[0].SetActive(false);
		}
		if(GameData.BannerCode[1] == 2){ //shop
			if(!ImgPoster[1].gameObject.activeSelf) ImgPoster[1].gameObject.SetActive(true);
			if(Btn_Shop[1].activeSelf) Btn_Shop[1].SetActive(false);
		}
	}

	#region button functions
	public void OnBannerClick(int idx){
		Application.OpenURL(EventBannerChecker.self.bannerLink[idx]);
	}
	public void OnShopButton(){
		HideAllPosters();
		MenuSceneController.instance.OnBtn_Shop();
	}
	public void OnCloseButton(int idx){
		ImgPoster[idx].gameObject.SetActive(false);
	}
	#endregion

	public void HideAllPosters(){
		for(int i = 0;i<2;i++){
			if(ImgPoster[i].gameObject.activeSelf) ImgPoster[i].gameObject.SetActive(false);
		}
	}

}
