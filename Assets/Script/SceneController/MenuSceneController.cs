using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using Facebook.Unity;
using System;
using System.Globalization;

public class MenuSceneController : MonoBehaviour {
	#region variable
	const string Tag_Btn = "Btn_MenuScene";

	public static MenuSceneController instance;

	public GameObject MobilPuno,HelpMenu,Credit,LevelSelect,StageSelect,WorldSelect,TapToPlay,settingBtn,buatSoalBtn,transitionOpen,
	transition,asap,shopMenu,moreGamesMenu,selectModeMenu,exitNotification,rouletteStar,
	rouletteStarNotification,webNotification,txt_RandomMode,loginScreen;
	public Button Button_SettingBtn;
	public Animator Anim_Setting,Anim_World,Anim_Stage,Anim_Help,Anim_LevelSelect,Anim_Credit,Anim_PopUpLogin,Anim_Shop,Anim_UnlockHint,
	Anim_BuyNotification,Anim_SoalNotification,Anim_ExitNotification,Anim_RouletteStar,Anim_GetPrize,Anim_RouletteNotif,
	Anim_WebNotification,Anim_MoreGames,Anim_SelectMode,Anim_ResetStarNotif,Anim_ResetStarConfirm,Anim_SelectModeNotif,Anim_LINEErrorNotif;
	public Text unlockHint,txt_UserName;
	public Image btn_music, btn_sound;
	public Sprite music_on, music_off, sound_on, sound_off;

	public bool settingOpen;

	public DateTime time;

	public AudioSource bgmMenu,sfxMenu;
	public AudioClip musicWorldStage,sfxButton;

	private IResult result;

	void Awake(){
//		CheckingInternetConnection.instance.checkInternetConnection ();
		instance = this;
	}

	void OnLevelWasLoaded ()
	{
		if (GameData._isLoggedIn) {
			loginScreen.SetActive(false);
			MobilPuno.SetActive(true);
		}
	}

	void Start ()
	{	
		if (GameData.loginTypeValue == (int)eLoginType.FACEBOOK) {
			Debug.Log("fb auto login");
			LINEController.lineControllerInstance.OnFbLoginSuccessful();
		}
//		if (!String.IsNullOrEmpty (GameData.loginUserNameValue)) {
//			loginScreen.SetActive (false);
//			punoLogo.SetActive (true);
//			txt_UserName.text = "Hello, \n" + GameData.loginUserNameValue;
//		} 

		//first play achievement
		#if !UNITY_EDITOR
		GPAchievementController.instance._achievement_new_player();
		#endif

		print ("onMenuScreen : " + GameData._onMenuScreen);

		settingOpen = false;
		afterReadComic (); //setelah baca komik langsung ke stage select
		playMenuMusic ();
		enableTransitionOpen ();
		//checkFBIsLoggedIn ();
		//GameData._setPowerUpAmount ();

		//roulette star
//		GameData._rouletteStarReady();

//		GameData.checkTotalTambahSoal ();

		GameData.checkPowerUpInventory ();
		GameData.resetVariable ();
		GameData.soundCondSetting ();

//		GameObject.Find (GameData.gameObject_PluginController).GetComponent<FBController> ().getFBName ();
		
//		EventPosterController.self.Show ();

		NotificationController.instance.SendRepeatingNotification (1, 86400, 0, "Asal Tebak PUNO", "Come play play here!", Color.black, true, true, true, "");

		BackgroundImgController.instance.setBackgroundImage();

		GoogleMobileAdsDemoScript.instance.RequestBanner();
	}

	void Update(){
		btn_image ();
		exitGame ();
	}
	#endregion
//----------------------------------------------------------------------------------------------------
	#region button functions

	public void OnTapToPlay ()
	{
		selectModeMenu.SetActive (true);
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxMenu, sfxButton);
		if(!GameData._newLogin){
//			LINEController.lineControllerInstance.onLineLoginClicked();
		}

		#region test
//		GameData.GilaMode.iGilaModeUnlockValue=1;
		#endregion

//		if (PlayerPrefs.HasKey ("StarWorld11Stage9")) {
//			if (PlayerPrefs.GetInt ("StarWorld11Stage9") > 0) {
//				GameData.GilaMode.iGilaModeUnlockValue = 1; //random mode unlocked
//			}
//		}
//
//		if (GameData.GilaMode.GetStarValue (11, 9) > 0) {
//			GameData.EnglishMode.iEnglishModeUnlockValue = 1; //english mode unlocked
//		}
	}

	public void OnEnterMode(){
		GameData._onMenuScreen = true;

		//BackgroundImgController.instance.setSeasonalBGFG();

		//openRouletteStar ();

		OnBtn_Close(GameData.SELECT_MODE_MENU);
		MobilPuno.transform.localScale = new Vector2 (0,0);
		TapToPlay.gameObject.SetActive (false);
		MobilPuno.gameObject.SetActive (false);
		asap.gameObject.SetActive (false);
		buatSoalBtn.gameObject.SetActive (true);
		ShowWorldMenu ();
		playMenuMusic ();

		BtnGilaMode();
		GoogleMobileAdsDemoScript.instance.ShowBannerAd();
	}

	public void OnBtn_Web(){
		webNotification.gameObject.SetActive (true);
	}

	public void OnBtn_YesWeb(){
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
		Application.OpenURL ("http://www.damniloveindonesia.com/");
	}

	public void OnBtn_FBLogin(string onMenu){
		FBController.instance.CallFBLogin ();
		if(onMenu == GameData.BTN_ONFBPOPUP){
			if(FB.IsLoggedIn){
				PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.Scene_TambahSoal);
				transition.gameObject.SetActive (true);
			}
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void OnBtn_Shop(){
//		EventPosterController.self.ShowIapBanner();
		shopMenu.gameObject.SetActive (true);
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void OnBtn_MoreGames(){
		moreGamesMenu.gameObject.SetActive (true);
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
		AppsFlyerController.instance._trackRichEvent ("access_more_games", "access_more_games", "access_more_games");
	}

	public void OnBtn_Setting(){
		if(!settingOpen){
			Anim_Setting.SetTrigger(GameData.Show);
			settingOpen = true;
		}
		else{ 
			Anim_Setting.SetTrigger(GameData.Hide);
			settingOpen = false;
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void btn_image(){
		//sound button
		if(GameData._soundDisable == false){
			btn_sound.sprite = sound_on;
		}else if(GameData._soundDisable == true){
			btn_sound.sprite = sound_off;
		}
	}

	public void OnBtn_Sound(){
		if(GameData._soundDisable == false){
			GameData._soundDisable = true;
		}else if(GameData._soundDisable == true){
			GameData._soundDisable = false;
		}

		GameData.soundCondSetting ();
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void OnBtn_About(){
		disableSettingBtn ();
		Credit.gameObject.SetActive (true);
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void OnBtn_Achievement(){
		Social.ShowAchievementsUI ();
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void OnBtn_Help(){
		print ("onMenuScreenHelp : " + GameData._onMenuScreen);

		ShowHelpMenu();

		disableSettingBtn ();
		GameData._sceneState = GameData.HELP_MENU;
		GameData._buttonTag = GameData.Tag_Help;
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void OnBtn_LogInOut ()
	{
		//show login screen
		LINEController.lineControllerInstance.showLoginScreen(GameData.loginTypeValue);
		bgmMenu.Stop();
		sfxMenu.Stop();
		loginScreen.SetActive(true);
		MobilPuno.SetActive(false);
		asap.SetActive(false);
		TapToPlay.SetActive(false);
	}

	public void OnBtn_Close (string sceneState)
	{
		string saveTime;

		GameData._sceneState = sceneState;
		GameData._buttonTag = GameData.Tag_Close;

		Debug.Log (sceneState);
		if (sceneState == GameData.WORLD_MENU) {
			Debug.Log (sceneState);
			GameData._onMenuScreen = false;
			buatSoalBtn.gameObject.SetActive (false);
			Anim_World.SetTrigger (GameData.Close);
			playMenuMusic ();
		} else if (sceneState == GameData.STAGE_MENU) {
			Anim_Stage.SetTrigger (GameData.Close);
			//PlayerPrefs.SetInt (GameData.Key_Background,0); //reset background to Jakarta BG
		} else if (sceneState == GameData.HELP_MENU) {
			Button_SettingBtn.interactable = true;
			Anim_Help.SetTrigger (GameData.Close);
		} else if (sceneState == GameData.FBLOGIN_MENU) {
			Anim_PopUpLogin.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.UNLOCKHINT_MENU) {
			Anim_UnlockHint.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.SHOP_MENU) {
			GameData._onMenuScene = GameData.Scene_Menu;
			Anim_Shop.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.CREDIT_MENU) {
			Anim_Credit.SetTrigger (GameData.Close);
		} else if (sceneState == GameData.BUY_NOTIFICATION_MENU) {
			Anim_BuyNotification.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.MORE_GAMES_MENU) {
			Anim_MoreGames.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.SELECT_MODE_MENU) {
			Anim_SelectMode.SetTrigger (GameData.Hide);
		} else if(sceneState == GameData.LINE_ERROR_NOTIF){
			Anim_LINEErrorNotif.SetTrigger(GameData.Hide);
		}else if (sceneState == GameData.SELECT_MODE_NOTIF) {
			Anim_SelectModeNotif.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.NOTIF_SOAL_MENU) {
			Anim_SoalNotification.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.EXIT_NOTIF_MENU) {
			Anim_ExitNotification.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.ROULETTE_STAR_MENU) {
			saveTime = (DateTime.Now).ToString ();
			PlayerPrefs.SetString (GameData.Key_lastRouletteStar, saveTime);
			GameData._NotificationTimer ();
			GameData.rouletteStarReady = false;

			Anim_RouletteStar.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.GETPRIZE_MENU) {
			Anim_GetPrize.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.ROULETTE_NOTIF_MENU) {
			CheckingInternetConnection.instance.checkInternetConnection ();
			Anim_RouletteNotif.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.WEB_NOTIF_MENU) {
			Anim_WebNotification.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.RESET_STAR_NOTIF) {
			Anim_ResetStarNotif.SetTrigger(GameData.Hide);
		}else if (sceneState == GameData.RESET_STAR_CONFIRM) {
			Anim_ResetStarConfirm.SetTrigger(GameData.Hide);
		}

		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void OnBtn_YesExit(){
		//GameData._NotificationTimer ();
		Application.Quit();
	}

	public void OnBtn_Locked(string sceneState){
		if(sceneState == GameData.WORLD_MENU){
			unlockHint.text = "selesaikan 10 soal di world sebelumnya";
			GameData._popUpHintTag = GameData.Tag_WrdPopUpHint;
			GameData._sceneState = GameData.WORLD_MENU;
			GameData._buttonTag = GameData.Tag_LockedWorld;
			Anim_World.SetTrigger (GameData.Hide);
		}

		if(sceneState == GameData.STAGE_MENU){
			unlockHint.text = "selesaikan soal sebelumnya";
			GameData._popUpHintTag = GameData.Tag_StgPopUpHint;
			GameData._sceneState = GameData.STAGE_MENU;
			GameData._buttonTag = GameData.Tag_LockedStage;
			Anim_Stage.SetTrigger (GameData.Hide);
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
		Debug.Log("sceneState : " + GameData._sceneState + " | buttonTag : " + GameData._buttonTag);
	}

	#endregion
//----------------------------------------------------------------------------------------------------
	#region PUBLIC functions
	public void setDisplayName (string name){
		txt_UserName.text = "Hello, \n" + name;
	}

	#endregion
//----------------------------------------------------------------------------------------------------
	#region PRIVATE Functions
	private void BtnGilaMode ()
	{
		//check whether random mode has been unlocked or not
		if (GameData.GilaMode.iGilaModeUnlockValue == 0 && GameData.EnglishMode.iEnglishModeUnlockValue == 0) {
			txt_RandomMode.SetActive (false);
		} else {
			if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
				txt_RandomMode.SetActive (false);
			} else {
				txt_RandomMode.SetActive (true);

				if (GameData.GilaMode.iGilaMode == 1) {
					Debug.Log("randommode");
					txt_RandomMode.GetComponent<Text> ().text = "RANDOM MODE";
				} else if (GameData.EnglishMode.iEnglishMode == 1) {
					Debug.Log("englishmode");
					txt_RandomMode.GetComponent<Text>().text = "ENGLISH MODE";
				}

			}
		}
	}

	void openRouletteStar(){
		if(GameData.rouletteStarReady == true){
			if (GameData._isConnected == true) {
				rouletteStar.gameObject.SetActive (true);
			} else {
				rouletteStarNotification.gameObject.SetActive (true);
			}
		}
	}

	void exitGame(){
		if(Input.GetKeyUp(KeyCode.Escape)){
			exitNotification.gameObject.SetActive (true);
		}
	}

	void disableSettingBtn(){
		Anim_Setting.SetTrigger(GameData.Close);
		//settingOpen = false;
	}

	void playMenuMusic(){
		if(GameData._onMenuScreen){
			GameData.soundSourceAnotherGO (GameData.BGM_SOUNDSOURCE,bgmMenu,musicWorldStage);
		}else if(!GameData._onMenuScreen){
			GameData.stopSoundSourceAnotherGO (GameData.BGM_SOUNDSOURCE,bgmMenu);
		}
	}

	void checkFBIsLoggedIn(){
		if(FB.IsLoggedIn){
			GameData.disableBtnFBLogin ();
		}
	}

	void ShowHelpMenu(){
		HelpMenu.SetActive(true);
	}
	void HideHelpMenu(){
		HelpMenu.SetActive(false);
	}

	void ShowWorldMenu(){
		LevelSelect.SetActive(true);
	}
	#endregion
//----------------------------------------------------------------------------------------------------
	#region GameplayController
	private void enableTransitionOpen(){
		string sceneState;

		sceneState = GameData._sceneState;
		if(sceneState == GameData.Scene_Menu || sceneState == GameData.Scene_Game || sceneState == GameData.Scene_TambahSoal){
			transitionOpen.gameObject.SetActive (true);
		}
	}

	private void afterReadComic(){//langsung ke stage select
		if(GameData._sceneState == GameData.Scene_Comic){
			buatSoalBtn.gameObject.SetActive (false);
			MobilPuno.gameObject.SetActive(false);
			LevelSelect.gameObject.SetActive (true);
			WorldSelect.gameObject.SetActive (false);
			StageSelect.gameObject.SetActive (true);
			loginScreen.SetActive(false);
			WorldSelectController.self.SetWorldNameAndIcon(PlayerPrefs.GetInt(GameData.Key_Background,0));
			BackgroundImgController.instance.setBackgroundImage();
		}
	}
	#endregion
//----------------------------------------------------------------------------------------------------
	#region Coroutines
	IEnumerator ChangeScene(string SceneName){
		yield return new WaitForSeconds(GameData.SceneWaitTime);

		GameData.GoToScene(SceneName);
	}
	#endregion
//----------------------------------------------------------------------------------------------------

	void OnApplicationQuit(){
		GoogleMobileAdsDemoScript.instance.DestroyBannerAd();
		PlayerPrefs.SetInt ("Pref/PosterShown", 0);
		PlayerPrefs.SetInt ("Pref/BannerCount", 0);
		PlayerPrefs.Save();
	}
}
