using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultPanelController : MonoBehaviour {
	public static ResultPanelController instance;
	public GameObject star1,star2,star3,rewardShare,Btn_Wrong,Btn_Correct,Btn_NextStage,Panel_MASTER,Panel_TimesUp,Panel_Correct,transition,notification;
	public GameObject BGClear,BGTimeUp,adsObj;
	public GameObject[] punoPrefab = new GameObject[4];

	public Animator Anim_rewardShare,Anim_notification;

	public Text rewardShareText,nextWorldText;
	public Text adsText;

	public Image IMG_gunung_kiri,IMG_gunung_kanan,kamingsun;
	public RawImage adsImg;

	public AudioSource sfxResult,sfxResult2,sfxResult3;
	public AudioClip sfxBenar, sfxWaktuHabis, sfxButton;

	private bool startAds;
	private bool prepareAds=false;

	private string adspot_id="ODU4Ojk5MjQ";
	public string result;

	void Awake(){
		instance = this;
	}

	void Start ()
	{
		startAds = false;
		result = PlayerPrefs.GetString (GameData.Key_Result);
		ShowResultPanel (result);
		GameData.soundCondSetting ();
		//showStarResult (result);

//		if (HikeManager.instance) {
//			HikeManager.instance.GetAds (adspot_id);
//		}

		adsObj.SetActive(false);
		GoogleMobileAdsDemoScript.instance.ShowBannerAd();

	}

	void Update ()
	{
//		if (!prepareAds) {
//			Debug.Log("preparing ads");
//			if (HikeManager.instance.adsReady) {
//				prepareAds = true;
//				showFreakOutNativeAds ();
//			} else {
//				adsText.text = "Ads not ready";
//			}
//		}
	}

	public void ShowResultPanel (string rs)
	{
		//Panel_MASTER.gameObject.SetActive(true);

		GameData._resultType = rs;
		if (rs == "TIMESUP") {
			BGTimeUp.SetActive (true);
			BGClear.SetActive (false);
			IMG_gunung_kiri.color = new Color (1, 1, 1, 0.2f);
			IMG_gunung_kanan.color = new Color (1, 1, 1, 0.2f);
			Panel_TimesUp.gameObject.SetActive (true);
			Btn_Wrong.gameObject.SetActive (true);

			GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxResult, sfxWaktuHabis);
			GameData.soundVolume (GameData.SFX_SOUNDSOURCE, sfxResult, 0.2f);
		} else if (rs == "CORRECT") {

			if (GameData.StageClearedCounter == 4) {
				GameData.StageClearedCounter = 0;
				StartCoroutine(ShowInterstitialAd());
			} else{
				GameData.StageClearedCounter++;
			}

			//set timer to false
			//StarController.instance.isTiming = false;
			BGTimeUp.SetActive (false);
			BGClear.SetActive (true);
			IMG_gunung_kiri.color = new Color (1, 1, 1, 0.2f);
			IMG_gunung_kanan.color = new Color (1, 1, 1, 0.2f);
			Panel_Correct.gameObject.SetActive (true);
			int stageText = PlayerPrefs.GetInt (GameData.Key_StageText);

			//last stage for development mode //untuk sementara
//			if (world != 0 && stage == 1) {
//				Btn_Wrong.gameObject.SetActive (true);
//			} else if (world == 0 && stage == 9) {
//				Btn_Wrong.gameObject.SetActive (true);
//			}else {
//				Btn_Correct.gameObject.SetActive (true);
//			}

			Btn_Correct.gameObject.SetActive (true);

			//last stage

			int world = PlayerPrefs.GetInt (GameData.Key_World);
			int stage = PlayerPrefs.GetInt (GameData.Key_Stage);

			if (world != (GameData.TotalWorld - 1) && stageText == (GameData.TotalStagePerWorld)) {
				nextWorldText.text = "Selamat kamu sudah membuka world selanjutnya";
				notification.SetActive(true);
			} else if (world == (GameData.TotalWorld - 1) && stageText == (GameData.TotalStagePerWorld)) {
				if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0 && GameData.GilaMode.iGilaModeUnlockValue == 0) {
					notification.SetActive(true);
					nextWorldText.text = "Selamat kamu sudah membuka Random Mode";
					GameData.GilaMode.iGilaModeUnlockValue=1;
				} else if (GameData.GilaMode.iGilaMode > 0 && GameData.EnglishMode.iEnglishMode == 0 && GameData.EnglishMode.iEnglishModeUnlockValue == 0) {
					notification.SetActive(true);
					nextWorldText.text = "Selamat kamu sudah membuka English Mode";
					GameData.EnglishMode.iEnglishModeUnlockValue=1;
				}
			}
			GameData.soundSourceAnotherGO(GameData.SFX_SOUNDSOURCE,sfxResult,sfxBenar);
			GameData.soundVolume (GameData.SFX_SOUNDSOURCE,sfxResult,0.2f);
		}
	}

	private void nextWorldController(){
		int world;

		prepareAdmobInterstitial ();
		world = PlayerPrefs.GetInt (GameData.Key_World);
		if (world == 3 && PlayerPrefs.GetInt(GameData.Key_World_Comic) == 3) {
			GameData._sceneState = GameData.Scene_Result;
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_bl_1);
		} else if (world == 6 && PlayerPrefs.GetInt(GameData.Key_World_Comic) == 6) {
			GameData._sceneState = GameData.Scene_Result;
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_pd_1);
		} else if (world == 9 && PlayerPrefs.GetInt(GameData.Key_World_Comic) == 9) {
			GameData._sceneState = GameData.Scene_Result;
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_trj);
		} else if (world == 12 && PlayerPrefs.GetInt(GameData.Key_World_Comic) == 12) {
			GameData._sceneState = GameData.Scene_Result;
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_jog);
		} else {
			PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Game);
		}
	}

	private void prepareAdmobInterstitial(){
//		int chanceShowAdmob;
//
//		chanceShowAdmob = Random.Range (1,10);
//
//		print ("chanceShowAdmob : " + chanceShowAdmob);
//
////		if(chanceShowAdmob > 5){
////			GoogleMobileAdsDemoScript.instance.RequestInterstitial ();
////			startAds = true;
////		}
//		GoogleMobileAdsDemoScript.instance.RequestInterstitial ();
//		startAds = true;
	}

	private void showAdmobInterstitial(){
		if(startAds == true){
			GoogleMobileAdsDemoScript.instance.ShowInterstitial ();
		}
	}

	public void OnMainMenuBtn(){
		GoogleMobileAdsDemoScript.instance.HideBannerAd();

		GameData.resetVariable ();

		GameData._sceneState = GameData.Scene_Menu;
		PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Menu);
		transition.gameObject.SetActive (true);
		GameData.soundSourceAnotherGO(GameData.SFX_SOUNDSOURCE,sfxResult,sfxButton);
		PlayerPrefs.SetInt (GameData.Key_Background,0); //reset background to Jakarta BG
	}

	public void OnRestartBtn(){
		GoogleMobileAdsDemoScript.instance.HideBannerAd();
		GameData.resetVariable ();

		PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Game);
		transition.gameObject.SetActive (true);
		GameData.soundSourceAnotherGO(GameData.SFX_SOUNDSOURCE,sfxResult,sfxButton);
	}

	public void OnNextStageBtn(){
		GoogleMobileAdsDemoScript.instance.HideBannerAd();

		GameData.resetVariable ();

		int world = PlayerPrefs.GetInt (GameData.Key_World);
		int stage = PlayerPrefs.GetInt(GameData.Key_Stage);

		Debug.Log ("current stage:" + stage);
	
		int stageText = PlayerPrefs.GetInt(GameData.Key_StageText);

		if (stage == (GameData.TotalStagePerWorld-1)) { //next world

			if (world == (GameData.TotalWorld-1)) {
				notification.gameObject.SetActive (true);
				kamingsun.gameObject.SetActive (true);
				stage++;

			} else {
				Debug.Log ("next world");
				world++;
				stageText = 1;
				stage = 0;
				PlayerPrefs.SetInt (GameData.Key_World, world);
				PlayerPrefs.SetInt (GameData.Key_Stage, stage);
				PlayerPrefs.SetInt (GameData.Key_StageText, stageText);
				nextWorldController ();
			}

		} else { //next stage
			Debug.Log("next stage");
			stage++;
			stageText++;
			PlayerPrefs.SetInt (GameData.Key_Stage, stage);
			PlayerPrefs.SetInt (GameData.Key_StageText, stageText);

			PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Game);
		}

		Debug.Log ("next stage:" + stage);

		if (world == (GameData.TotalWorld-1) && stage == 10) {
			Debug.Log("do nothing");
		}
		else
		{
			Debug.Log ("scene transition");
			GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxResult, sfxButton);
			transition.gameObject.SetActive (true);
		}
	}

	public void FBShareStar(){
		int world = PlayerPrefs.GetInt (GameData.Key_World);
		int stage = PlayerPrefs.GetInt (GameData.Key_Stage);
		FBController.instance.ShareScore (GameData._tempStar, GameData._worldName, stage);
	}

	public void getRewardShareScore(){
		rewardShare.gameObject.SetActive (true);
		GetReward.instance.getReward();
		rewardShareText.text = "ANDA MENDAPATKAN\n" + GameData._rewardName;
	}

	public void OnBtn_Close(string sceneState){
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxResult,sfxButton);

		GameData._sceneState = sceneState;
		GameData._buttonTag = GameData.Tag_Close;
		if(GameData._sceneState == GameData.REWARDSHARE_MENU){
			GetReward.instance.rewardList [GameData._randomReward].gameObject.SetActive (false);
			Anim_rewardShare.SetTrigger (GameData.Hide);
		}else if (sceneState == GameData.RESULT_MENU) {
			Anim_notification.SetTrigger (GameData.Hide);
		}
	}

	private void showFreakOutNativeAds ()
	{
		Debug.Log(HikeManager.instance.adsReady);
		for (int i = 0; i < 4; i++) {
			punoPrefab [i].SetActive (false);
		}

		int temp = Random.Range (0, 4);

		punoPrefab [temp].SetActive (true);

		adsObj.SetActive(true);

		adsText.text = HikeManager.instance.getAdsTitle () + "\n\nSponsored by "+ HikeManager.instance.getAdsAdvertiser();
		adsImg.texture=HikeManager.instance.getAdsTexture();
		HikeManager.instance.showAds();
	}

	public void openFreakOutNativeAds(){
		HikeManager.instance.clickAds();
	}

	IEnumerator ShowInterstitialAd(){
		while(!GoogleMobileAdsDemoScript.instance.GetInterstitialLoadStatus()){
			GoogleMobileAdsDemoScript.instance.ShowInterstitial();
			yield return null;
		}
	}
}
