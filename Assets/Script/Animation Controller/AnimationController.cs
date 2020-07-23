using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour {
	#region variableDeclaration
	private GameObject levelSelect,TapToPlay,MobilPuno,stageSelect,worldSelect,helpBox,settingBtn,transitionOpen,glowScroll,efekCahaya,asap,bubbleResult
	,star1,star2,star3,mainMenuBox,popUpShare,credit,popUpLogin,popUpBuatSoal,popUpUnlockHint,mainHintMenu,hintMenu,shopMenu,rouletteMenu,prizeMenu,rewardShare,selamatBermain,pointer,
	buyNotification,soalNotification,nextWorldNotification,exitNotification,rouletteNotification,
	rouletteStar,webNotification,moreGames,selectMode,resetStarNotif,resetStarConfirm,selectModeNotif,lineErrorNotif;

	private StarController timer;

	private Button submitButton,closeButton;
	private Image Img_roulette;
	private Animator Anim_LevelSelect,Anim_WorldSelect;
	private AnimationInitObj anim_obj;
	private string sceneName;
	private int star;
	private AudioClip suaraBan,suaraMembal,suaraKlakson,suaraScroll,musicMenu,musicWorldStage,sfxPrize,sfxLoseStar,sfxGotStar;
	private AudioSource SFXLogo,bgmMenu,sfxMenu,sfxMenu2,sfxMenu3;

	private void initGameObject ()
	{
		try {
			anim_obj = GameObject.Find ("SCENE_CONTROLLER").GetComponent<AnimationInitObj> ();
		} catch {
			Debug.Log ("its okay");
		}

		//global
		transitionOpen = anim_obj.GetTransitionOpen ();
		helpBox = anim_obj.GetHelpBox ();
		efekCahaya = anim_obj.GetEfekCahaya ();
		shopMenu = anim_obj.GetShopMenu ();
		rouletteMenu = anim_obj.GetRouletteMenu ();
		prizeMenu = anim_obj.GetPrizeMenu ();
		pointer = anim_obj.GetPointer ();
		buyNotification = anim_obj.GetBuyNotification ();
		exitNotification = anim_obj.GetExitNotification ();
		rouletteNotification = anim_obj.GetRouletteNotif ();
		closeButton = anim_obj.GetCloseButton ();

		//menuscene
		if (SceneManager.GetActiveScene ().name == "MenuScene") {
			levelSelect = anim_obj.GetLevelSelect ();
			TapToPlay = anim_obj.GetTapToPlay ();
			MobilPuno = anim_obj.GetMobilPuno ();
			stageSelect = anim_obj.GetStageSelect ();
			worldSelect = anim_obj.GetWorldSelect ();
			settingBtn = anim_obj.GetSettingBtn ();
			//buatSoalBtn = anim_obj.GetBuatSoalBtn ();
			asap = anim_obj.GetAsap ();
			credit = anim_obj.GetCredit ();
			popUpLogin = anim_obj.GetPopUpLogin ();
			popUpUnlockHint = anim_obj.GetPopUpUnlockHint ();
			soalNotification = anim_obj.GetSoalNotification ();
			rouletteStar = anim_obj.GetRouleteStar ();
			webNotification = anim_obj.GetWebNotification ();
			moreGames = anim_obj.GetMoreGamesObj ();
			selectMode = anim_obj.GetSelectModeObj();
			Anim_LevelSelect = anim_obj.GetAnim_LevelSelect ();
			Anim_WorldSelect = anim_obj.GetAnim_WorldSelect ();
			resetStarNotif = anim_obj.GetResetStarNotifObj();
			resetStarConfirm = anim_obj.GetResetStarConfirmObj();
			selectModeNotif = anim_obj.GetSelectModeNotif();
			lineErrorNotif = anim_obj.GetLineErrorNotif();
		}

		//gamescene
		else if (SceneManager.GetActiveScene ().name == "GameScene") {
			bubbleResult = anim_obj.GetBubbleResult ();
			mainMenuBox = anim_obj.GetMainMenuBox ();
			mainHintMenu = anim_obj.GetMainHintMenu ();
			hintMenu = anim_obj.GetHintMenu ();
			selamatBermain = anim_obj.GetSelamatBermain ();
			timer = anim_obj.GetTimer ();
			submitButton = anim_obj.GetSubmitButton ();
			Img_roulette = anim_obj.GetImg_roulette ();
		}

		//resultscene
		else if (SceneManager.GetActiveScene ().name == "ResultScene") {
			glowScroll = anim_obj.GetGlowScroll ();
			star1 = anim_obj.GetStar1 ();
			star2 = anim_obj.GetStar2 ();
			star3 = anim_obj.GetStar3 ();
			rewardShare = anim_obj.GetRewardShare ();
			nextWorldNotification = anim_obj.GetNextWorldNotification ();
		}

		//tambahsoalscene
		else if (SceneManager.GetActiveScene ().name == "TambahSoalScene") {
			popUpBuatSoal = anim_obj.GetPopUpBuatSoal ();
		}

		//popUpShare = anim_obj.GetPopUpShare ();

		//sound
		bgmMenu = anim_obj.GetBGMMenu ();
		sfxMenu = anim_obj.GetSFXMenu ();
		suaraScroll = anim_obj.GetSuaraScroll ();
		sfxPrize = anim_obj.GetSfxPrize ();

		//gamescene
		if (SceneManager.GetActiveScene ().name == "GameScene") {
			sfxMenu2 = anim_obj.GetSFXMenu2 ();
			sfxLoseStar = anim_obj.GetSfxLoseStar ();
		}

		//menuscene
		else if (SceneManager.GetActiveScene ().name == "MenuScene") {
			suaraBan = anim_obj.GetSuaraBan ();
			suaraMembal = anim_obj.GetSuaraMembal ();
			suaraKlakson = anim_obj.GetSuaraKlakson ();
			musicMenu = anim_obj.GetMusicMenu ();
		}

		//resultscene
		else if (SceneManager.GetActiveScene ().name == "ResultScene") {
			sfxGotStar = anim_obj.GetSfxGotStar ();
		}

		//sfxMenu3 = anim_obj.GetSFXMenu3 ();


	}
	#endregion

	#region sound
	private void soundSource(AudioSource source, AudioClip clip){
		source = GetComponent<AudioSource> ();
		source.clip = clip;
		source.Play ();
	}

	public void _playBgmMenu(){
		GameData.soundSourceAnotherGO (GameData.BGM_SOUNDSOURCE,bgmMenu,musicMenu);
	}

	public void _suaraScroll(){
		GameData.soundSourceAnotherGO (GameData.SFX2_SOUNDSOURCE,sfxMenu2,suaraScroll);
	}

	public void _sfxPrize(){
		GameData.soundSourceAnotherGO (GameData.SFX3_SOUNDSOURCE,sfxMenu,sfxPrize);
	}

	public void _sfxLoseStar(){
		GameData.soundSourceAnotherGO (GameData.SFX3_SOUNDSOURCE,sfxMenu,sfxLoseStar);
	}

	public void _sfxGotStar(){
		GameData.soundSourceAnotherGO (GameData.SFX3_SOUNDSOURCE,sfxMenu3 ,sfxGotStar);
		GameData.soundVolume (GameData.SFX3_SOUNDSOURCE,sfxMenu3,0.4f);
	}

	public void _suaraBan(){
		GameData.soundSourceAnotherGO (GameData.SFX2_SOUNDSOURCE,sfxMenu2 ,suaraBan);
	}

	public void _suaraMembal(){
		GameData.soundSourceAnotherGO (GameData.SFX2_SOUNDSOURCE,sfxMenu2 ,suaraMembal);
	}

	public void _suaraKlakson(){
		GameData.soundSourceAnotherGO (GameData.SFX2_SOUNDSOURCE,sfxMenu2 ,suaraKlakson);
	}
	#endregion

	#region publicFunction
	public void ChangeMenu ()
	{
		Debug.Log ("sceneState : " + GameData._sceneState + " | buttonTag : " + GameData._buttonTag);
		if (GameData._sceneState == GameData.WORLD_MENU && GameData._buttonTag == GameData.Tag_Close) {// in menu scene
			levelSelect.gameObject.SetActive (false);
			MobilPuno.gameObject.SetActive (true);
		} else if (GameData._sceneState == GameData.WORLD_MENU && GameData._buttonTag == GameData.Tag_World) {
			worldSelect.gameObject.SetActive (false);
			//buatSoalBtn.gameObject.SetActive (false);
			stageSelect.gameObject.SetActive (true);
		} else if (GameData._sceneState == GameData.WORLD_MENU && GameData._buttonTag == GameData.Tag_LockedWorld) {
			popUpUnlockHint.gameObject.SetActive (true);
			worldSelect.gameObject.SetActive (false);
		} else if (GameData._sceneState == GameData.WORLD_MENU && GameData._buttonTag == GameData.Tag_BuatSoal) {
			popUpLogin.gameObject.SetActive (true);
			worldSelect.gameObject.SetActive (false);
		} else if (GameData._sceneState == GameData.STAGE_MENU && GameData._buttonTag == GameData.Tag_Close) {
			//buatSoalBtn.gameObject.SetActive (true);
			worldSelect.gameObject.SetActive (true);
			stageSelect.gameObject.SetActive (false);
		} else if (GameData._sceneState == GameData.STAGE_MENU && GameData._buttonTag == GameData.Tag_LockedStage) {
			popUpUnlockHint.gameObject.SetActive (true);
			stageSelect.gameObject.SetActive (false);
		} else if (GameData._sceneState == GameData.HELP_MENU && GameData._buttonTag == GameData.Tag_Help) {
			helpBox.gameObject.SetActive (true);
		} else if (GameData._sceneState == GameData.HELP_MENU && GameData._buttonTag == GameData.Tag_Close) {
			if (GameData._sceneState == GameData.WORLD_MENU) {
				//buatSoalBtn.gameObject.SetActive (true);
			}				

			if (SceneManager.GetActiveScene().name == "GameScene" && GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
				selamatBermain.gameObject.SetActive (true);
			}

			try{
				helpBox.gameObject.SetActive (false);

			}catch{
				Debug.Log ("it's okay");
			}
		}else if(GameData._sceneState == GameData.GAME_MENU && GameData._buttonTag == GameData.Tag_Close){
			selamatBermain.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.FBLOGIN_MENU && GameData._buttonTag == GameData.Tag_Close){
			worldSelect.gameObject.SetActive (true);
			popUpLogin.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.FBSHARE_MENU && GameData._buttonTag == GameData.Tag_Close){
			rewardShare.gameObject.SetActive (true);
			popUpShare.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.REWARDSHARE_MENU && GameData._buttonTag == GameData.Tag_Close){
			//timer.enabled = true;
			rewardShare.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.UNLOCKHINT_MENU && GameData._buttonTag == GameData.Tag_Close){
			if(GameData._popUpHintTag == GameData.Tag_WrdPopUpHint){
				worldSelect.gameObject.SetActive (true);
				popUpUnlockHint.gameObject.SetActive (false);
			}else if(GameData._popUpHintTag == GameData.Tag_StgPopUpHint){
				stageSelect.gameObject.SetActive (true);
				popUpUnlockHint.gameObject.SetActive (false);
			}
		}else if(GameData._sceneState == GameData.HINT_MENU && GameData._buttonTag == GameData.Tag_Shop){// in game scene
			shopMenu.gameObject.SetActive(true);
			hintMenu.gameObject.SetActive(false);
		}else if(GameData._sceneState == GameData.HINT_MENU && GameData._buttonTag == GameData.Tag_Roulette){
			rouletteMenu.gameObject.SetActive(true);
			hintMenu.gameObject.SetActive(false);
		}else if(GameData._sceneState == GameData.HINT_MENU && GameData._buttonTag == GameData.Tag_Close){
			mainHintMenu.gameObject.SetActive(false);
			GameData._onMenuScene = GameData.GAME_MENU;
		}else if(GameData._sceneState == GameData.SHOP_MENU && GameData._buttonTag == GameData.Tag_Close){
			if(GameData._onMenuScene == GameData.Scene_Menu){
				shopMenu.gameObject.SetActive (false);
			}else if(GameData._onMenuScene == GameData.Scene_Game){
				hintMenu.gameObject.SetActive (true);
				shopMenu.gameObject.SetActive (false);
			}
			GameData._onMenuScene = "";
		}else if(GameData._sceneState == GameData.MORE_GAMES_MENU && GameData._buttonTag == GameData.Tag_Close){
			moreGames.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.SELECT_MODE_MENU && GameData._buttonTag == GameData.Tag_Close){
			selectMode.gameObject.SetActive(false);
		}else if(GameData._sceneState == GameData.SELECT_MODE_NOTIF && GameData._buttonTag == GameData.Tag_Close){
			selectModeNotif.gameObject.SetActive(false);
		}else if(GameData._sceneState == GameData.LINE_ERROR_NOTIF && GameData._buttonTag == GameData.Tag_Close){
			lineErrorNotif.gameObject.SetActive(false);
		}else if(GameData._sceneState == GameData.RESET_STAR_NOTIF && GameData._buttonTag == GameData.Tag_Close){
			resetStarNotif.gameObject.SetActive(false);
		}else if(GameData._sceneState == GameData.RESET_STAR_CONFIRM && GameData._buttonTag == GameData.Tag_Close){
			resetStarConfirm.gameObject.SetActive(false);
		}else if(GameData._sceneState == GameData.HINT_MENU && GameData._buttonTag == GameData.Tag_Used){
			//close after using power up in hint
			mainHintMenu.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.ROULETTE_MENU && GameData._buttonTag == GameData.Tag_Close){
				rouletteMenu.gameObject.SetActive (false);
				timer.enabled = true;
		}else if(GameData._sceneState == GameData.ROULETTE_MENU && GameData._buttonTag == GameData.Tag_Used){
			closeButton.enabled = true;
			efekCahaya.gameObject.SetActive (false);
			pointer.gameObject.SetActive (true);
			prizeMenu.gameObject.SetActive (true);
			Img_roulette.gameObject.transform.eulerAngles = new Vector3 (0,0,0);
		}else if(GameData._sceneState == GameData.ROULETTE_STAR_MENU && GameData._buttonTag == GameData.Tag_Close){
			rouletteMenu.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.ROULETTE_STAR_MENU && GameData._buttonTag == GameData.Tag_Used){
			closeButton.enabled = true;
			efekCahaya.gameObject.SetActive (false);
			pointer.gameObject.SetActive (true);
			rouletteStar.gameObject.SetActive (false);
			prizeMenu.gameObject.SetActive (true);
		}else if(GameData._sceneState == GameData.ROULETTE_MENU && GameData._buttonTag == GameData.Tag_Roulette){
			rouletteMenu.gameObject.SetActive (false);
			rouletteNotification.gameObject.SetActive (true);
		}else if(GameData._sceneState == GameData.GETPRIZE_MENU && GameData._buttonTag == GameData.Tag_Close){
			if (GameData._onMenuScene == GameData.WORLD_MENU) {
				GameData._onMenuScene = string.Empty;
				RouletteController.instance._disableSpin ();
				rouletteStar.gameObject.SetActive (true);
				prizeMenu.gameObject.SetActive (false);
				rouletteMenu.gameObject.SetActive (false);
			} else {
				prizeMenu.gameObject.SetActive (false);
				rouletteMenu.gameObject.SetActive (false);
			}
		}else if(GameData._sceneState == GameData.BACKTOMAINMENU_MENU && GameData._buttonTag == GameData.Tag_BackToMainMenu){
			mainMenuBox.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.BUATSOAL_MENU && GameData._buttonTag == GameData.Tag_Close){
			GetReward.instance.rewardList [GameData._randomReward].gameObject.SetActive (false);
			popUpBuatSoal.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.CREDIT_MENU && GameData._buttonTag == GameData.Tag_Close){
			credit.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.BUY_NOTIFICATION_MENU && GameData._buttonTag == GameData.Tag_Close){
			buyNotification.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.NOTIF_SOAL_MENU && GameData._buttonTag == GameData.Tag_Close){
			soalNotification.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.RESULT_MENU && GameData._buttonTag == GameData.Tag_Close){
			nextWorldNotification.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.EXIT_NOTIF_MENU && GameData._buttonTag == GameData.Tag_Close){
			exitNotification.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.ROULETTE_NOTIF_MENU && GameData._buttonTag == GameData.Tag_Close){
			rouletteNotification.gameObject.SetActive (false);
		}else if(GameData._sceneState == GameData.WEB_NOTIF_MENU && GameData._buttonTag == GameData.Tag_Close){
			webNotification.gameObject.SetActive (false);
		}

		GameData._sceneState = "";
		GameData._buttonTag = "";
	} 

	public void frameAnim(float frame){
		GameData._frameAnim = frame;
	}

	public void correctEffect(){
		string result;
		result = PlayerPrefs.GetString (GameData.Key_Result);

		if(result == "CORRECT"){
			glowScroll.gameObject.SetActive (true);
			efekCahaya.gameObject.SetActive (true);
		}
	}

	public void ChangeScene(){
		sceneName = PlayerPrefs.GetString (GameData.Key_SceneToGo);
		GameData.GoToScene (sceneName);

		GameTools.destroyGameObject (GameData.BGM_SOUNDSOURCE);
		GameTools.destroyGameObject (GameData.SFX_SOUNDSOURCE);
		GameTools.destroyGameObject (GameData.SFX2_SOUNDSOURCE);
		GameTools.destroyGameObject (GameData.SFX3_SOUNDSOURCE);
		//Application.LoadLevel(GameData.Scene_Loading);
	}
	#endregion

	#region show_hide_disable gameObject
	public void hideBubbleResult(){
		//submitButton.enabled = true;
		CursorController.instance.waitingAnimation = false;
		submitButton.gameObject.SetActive(true);
		bubbleResult.gameObject.SetActive (false);
	}

	public void disableTransitionOpen(){
		transitionOpen.gameObject.SetActive (false);
	}

	public void disableMainMenuBox(){
		timer.enabled = true;
		mainMenuBox.gameObject.SetActive (false);
	}

	public void showTapToPlay(){		
		TapToPlay.gameObject.SetActive (true);
		asap.gameObject.SetActive (true);
	}

	public void hidelevelSelect(){
		Anim_LevelSelect.SetTrigger (GameData.Hide);
	}

	public void showlevelSelect(){
		try{
			Anim_LevelSelect.SetTrigger (GameData.Show);
		}catch{
			Debug.Log ("it's okay");
		}
	}

	public void showMenuSelect(){
		if(GameData._buttonTag == GameData.Tag_BuatSoal){
			Anim_WorldSelect.SetTrigger (GameData.Show);
		}
	}

	public void showStar1(){
		star = GameData._tempStar;

		if (GameData._resultType == GameData.typeCorrect) {
			if(star >= 1){
				star1.gameObject.SetActive (true);
			}
		}
	}

	public void showStar2(){
		star = GameData._tempStar;

		if (GameData._resultType == GameData.typeCorrect) {
			if(star >= 2){
				star2.gameObject.SetActive (true);
			}
		}
	}

	public void showStar3(){
		star = GameData._tempStar;

		if (GameData._resultType == GameData.typeCorrect) {
			if(star >= 3){
				star3.gameObject.SetActive (true);
			}
		}
	}
	#endregion

	#region button animation

	#endregion

	void Start(){
		initGameObject ();
	}
}
