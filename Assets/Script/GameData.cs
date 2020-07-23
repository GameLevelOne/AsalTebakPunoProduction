using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Net;
using System;
using System.Globalization;

public enum eLoginType{
	LINE = 0,
	FACEBOOK,
	GUEST,
    Google
}

public static class GameData{
	#region TempVariable
	private static string sceneState;
	private static string buttonTag;
	private static string onMenuScene;
	private static string popUpHintTag;
	private static string worldName;
	private static string animTrigger;
	private static string resultType;
	private static string rewardName;
	private static Sprite worldSprite;
	private static int worldNumber;
	private static int stageNumber;
	private static int tempStar;
	private static int charIndex;
	private static int letterChecked;
	private static int totalLetters;
	private static int usedRoulette;
	private static int randomReward;
	private static float frameAnim;
	private static bool checkUnlockedStg;
	private static bool onMenuScreen;
	private static bool soundDisable;
	private static bool isConnected;
	private static bool isLoggedIn = false;
	
	private static bool lineInit = false;
//	private static bool lineLogin = false;
	private static bool newLogin = true;
	#endregion

	#region DynamicVar
	public static int ownPowerUp;
	public static int timeNotif;
	public static float idleWaitTime;
	public static bool rouletteStarReady;
	private static int usingPowerUp;
	#endregion

	#region Declarations
	//constant value for GameObject
	public const string GAMEID = "PUNO";
	public const string gameObject_PluginController = "PluginController";

	//constant value for power up name
	public const string pu_ext1 		= "Extended Time 5s";
	public const string pu_ext2 		= "Extended Time 10s";
	public const string pu_show1word 	= "Show One Word";
	public const string pu_show2words 	= "Show Two Words";
	public const string pu_stopTime1 	= "Freeze Time 5s";
	public const string pu_stopTime2 	= "Freeze Time 10s";
	public const string pu_resetStar	= "Reset Star";

	//constant value for tag
	public const string Tag_Close			= "close";
	public const string Tag_World			= "world";
	public const string Tag_Help			= "help";
	public const string Tag_MainMenu		= "mainmenu";
	public const string Tag_Restart			= "restart";
	public const string Tag_Shop 			= "shop";
	public const string Tag_Roulette		= "roulette";
	public const string Tag_Used			= "used";
	public const string Tag_LockedWorld		= "lockedWorld";
	public const string Tag_LockedStage		= "lockedStage";
	public const string Tag_BuatSoal 		= "buatSoal";
	public const string Tag_StgPopUpHint	= "unlockStageHint";
	public const string Tag_WrdPopUpHint	= "unlockWorldHint";
	public const string Tag_BackToMainMenu	= "backToMainMenu";

	//constant value (string)
	public const string CHARACTER_BAGONG 	= "BAG";
	public const string CHARACTER_GARENG 	= "GAR";
	public const string CHARACTER_PETRUK 	= "PET";
	public const string CHARACTER_SEMAR  	= "SEM";
	public const string BGM_SOUNDSOURCE  	= "BGM";
	public const string SFX_SOUNDSOURCE  	= "SFX";
	public const string SFX2_SOUNDSOURCE 	= "SFX2";
	public const string SFX3_SOUNDSOURCE 	= "SFX3";
	public const string SFXLOGO_SOUNDSOURCE = "LogoGroup";

	//constant value (int)
	public const int TotalWorld			= 15;
	public const int TotalStagePerWorld = 10;
	public const int TotalPowerUp 		= 7;
	public const int TotalTambahSoal 	= 5;

	//constant value (float)
	public const float SceneWaitTime = 1f;
	public const float idleWaitShortTime = 1f;
	public const float idleWaitLongTime = 3f;

	//Animation Trigger Name
	public const string Open		= "Open";
	public const string Close		= "Close";
	public const string Show		= "Show";
	public const string Hide		= "Hide";
	//opening scroll in result scene
	public const string BadOpening	= "BadOpening";
	public const string GoodOpening = "GoodOpening";
	//trigger character animation
	public const string isAlmost = "isAlmost";
	public const string isWrong = "isWrong";
	public const string isCorrect = "isCorrect";
	//string result type
	public const string typeTimesUp = "TIMESUP";
	public const string typeCorrect = "CORRECT";
	
	//constant scene names
	public const string Scene_Menu			= "MenuScene";
	public const string Scene_LevelSelect	= "LevelSelectScene";
	public const string Scene_Game			= "GameScene";
	public const string Scene_Loading		= "LoadingScene";
	public const string Scene_Result		= "ResultScene";
	public const string Scene_Comic 		= "ComicScene";
	public const string Scene_TambahSoal	= "TambahSoalScene";

	//constant menu names
	public const string REWARDSHARE_MENU			= "RewardShare";
	public const string FBLOGIN_MENU 				= "FBLogin";
	public const string FBSHARE_MENU 				= "FBShare";
	public const string BUATSOAL_MENU 				= "BuatSoal";
	public const string UNLOCKHINT_MENU 			= "UnlockHint";
	public const string WORLD_MENU					= "WorldMenu";
	public const string STAGE_MENU					= "StageMenu";
	public const string GAME_MENU 					= "GameMenu";
	public const string HELP_MENU					= "HelpMenu";
	public const string HINT_MENU 					= "HintMenu";
	public const string SHOP_MENU 					= "ShopMenu";
	public const string ROULETTE_MENU 				= "RouletteMenu";
	public const string ROULETTE_STAR_MENU			= "RouletteStarMenu";
	public const string GETPRIZE_MENU 				= "GetPrizeMenu";
	public const string BACKTOMAINMENU_MENU 		= "BackToMainMenu";
	public const string CREDIT_MENU 				= "Credit";
	public const string BUY_NOTIFICATION_MENU 		= "BuyNotification";
	public const string NOTIF_SOAL_MENU 			= "NotifSubmitSoal";
	public const string EXIT_NOTIF_MENU 			= "ExitNotification";
	public const string RESULT_MENU 				= "ResultMenu";
	public const string ROULETTE_NOTIF_MENU 		= "RouletteNotification";
	public const string WEB_NOTIF_MENU 				= "WebNotification";
	public const string MORE_GAMES_MENU				= "MoreGames";
	public const string SELECT_MODE_MENU			= "SelectMode";
	public const string RESET_STAR_CONFIRM			= "ResetStarConfirm";
	public const string RESET_STAR_NOTIF			= "ResetStarNotif";
	public const string SELECT_MODE_NOTIF			= "SelectModeNotif";
	public const string LINE_ERROR_NOTIF			= "LINEErrorNotif";

	//constant button location
	public const string BTN_ONFBPOPUP 		= "FBPopup"; 

	//constant comic page
	public const string comic_jkt_1 = "jakarta1";
	public const string comic_jkt_2 = "jakarta2";
	public const string comic_jkt_3 = "jakarta3";
	public const string comic_bl_1 = "bali1";
	public const string comic_bl_2 = "bali2";
	public const string comic_pd_1 = "padang1";
	public const string comic_pd_2 = "padang2";
	public const string comic_trj = "Toraja";
	public const string comic_jog = "Jogja";

	//constant world name
	public static string[] worldNameList = new string[]{"Jakarta","Jakarta 2","Jakarta 3","Bali","Bali 2","Bali 3",
	"Padang","Padang 2","Padang 3","Toraja","Toraja 2","Toraja 3","Jogja","Jogja 2","Jogja 3"};

	//constant PlayerPref Keys
	public const string Key_prizeCode 			= "prizeCode";
	public const string Key_SceneToGo			= "TempScene";
	public const string Key_World				= "SelectedWorld";
	public const string Key_Stage				= "SelectedStage";
	public const string Key_StageText 			= "StageText";
	public const string Key_NetCon				= "NetCon";
	public const string Key_Result				= "Result";
	public const string Key_tempStar			= "Tempstar";
	public const string Key_World_Comic			= "WorldComic";
	public const string Key_UnlockedWorld		= "unlockWorld";
	public const string Key_PowerUp 			= "powerUp"; 
	public const string Key_Background 			= "background";
	public const string Key_setDefaultBg 		= "setDefaultBg";
	public const string Key_lastTimer 			= "lastTimer";
	public const string Key_usingPowerUp 		= "UsingPowerUp";
	public const string Key_fbname 				= "fbname";
	public const string Key_fbemail				= "fbemail";
	public const string Key_fbuserid			= "fbuserid";
	public const string Key_starCurrency		= "starCurrency";
	public const string Key_firstPlay			= "firstPlay";
	public const string Key_firstPlayRandomMode = "firstPlayRandomMode";
	public const string Key_totalTambahSoal 	= "totalTambahSoal";
	public const string key_lastTimePlay 		= "lastTimePlay";
	public const string Key_lastRouletteStar	= "lastUseRouletteStar";
	public const string Key_lastStageWorldJKT 	= "StarWorld0Stage14";
	public const string Key_lastStageWorldBL 	= "StarWorld3Stage14";
	public const string Key_lastStageWorldPD 	= "StarWorld6Stage14";
	public const string Key_ServerTime			= "ServerTime";

	//constant JSON resource data
	public const string LevelDataResource = "LevelData";
	public const string LevelDataResource_GilaMode = "LevelData_GilaMode";
	public const string LevelDataResource_EnglishMode = "LevelData_EnglishMode";

	//IAP product sku
	#if UNITY_ANDROID
	public const string sku_paket1 = "paket1";
	public const string sku_paket2 = "paket2";
	public const string sku_paket3 = "paket3";
	public const string sku_paket4 = "paket4";
	public const string sku_paket5 = "paket5";
	#elif UNITY_IOS
	public const string sku_paket1 = "gemu.puno.paket1";
	public const string sku_paket2 = "gemu.puno.paket2";
	public const string sku_paket3 = "gemu.puno.paket3";
	public const string sku_paket4 = "gemu.puno.paket4";
	public const string sku_paket5 = "gemu.puno.paket5";
	#endif

	//game data
	private static int stageClearedCount = 0;

	private static int Star_Total = 0;

	public static bool[] temp_checkLockedStg = new bool[TotalStagePerWorld-1];

	public static int[] Star_World1 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};
	public static int[] Star_World2 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};
	public static int[] Star_World3 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	public static int[] Star_World4 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};
	public static int[] Star_World5 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	public static int[] Star_World6 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	public static int[] Star_World7 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	public static int[] Star_World8 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	public static int[] Star_World9 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	public static int[] Star_World10 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	public static int[] Star_World11 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	public static int[] Star_World12 = new int[TotalStagePerWorld]{
		0,0,0,0,0,0,0,0,0,0
	};

	/*public static LevelStatus[] WorldStatus = new LevelStatus[3]{
		LevelStatus.UNLOCKED,
		LevelStatus.LOCKED,
		LevelStatus.LOCKED
	};*/


	#endregion

	public static DateTime ServerTime{
		get{ return Convert.ToDateTime( PlayerPrefs.GetString(Key_ServerTime) ); }
		set{ PlayerPrefs.SetString(Key_ServerTime, value.ToString()); }
	}

//----------------------------------------------------------------------------------------------------
	#region Public Functions
	public static void _rouletteStarReady(){
		DateTime lastTime;
		TimeSpan compareTime;
		Debug.Log (PlayerPrefs.HasKey (Key_lastRouletteStar));
		if (!PlayerPrefs.HasKey (Key_lastRouletteStar)) { //first time play
//			PlayerPrefs.SetString (Key_lastRouletteStar, string.Empty);
			Debug.Log("asdasdasd");
			rouletteStarReady = true;
		} else {
			lastTime = Convert.ToDateTime (PlayerPrefs.GetString (GameData.Key_lastRouletteStar));
			compareTime = DateTime.Now - lastTime;

			Debug.Log ("compareTime : " + compareTime);

			if (compareTime >= new TimeSpan(2,0,0)) {
				rouletteStarReady = true;
			} else {
				rouletteStarReady = false;
			}
		}
	}

	/*
	 * else if(string.IsNullOrEmpty(PlayerPrefs.GetString(Key_lastRouletteStar))){
			rouletteStarReady = true;
		} 
	 */ 

	public static TimeSpan _setTimeNotif(){
		DateTime lastTime,nextTimeNotif;
		TimeSpan compareTime;

		lastTime = Convert.ToDateTime (PlayerPrefs.GetString (GameData.Key_lastRouletteStar));
		nextTimeNotif = lastTime.AddHours (2);
		//nextTimeNotif = new DateTime (lastTime.Year,lastTime.Month,lastTime.Day,(lastTime.Hour+2),lastTime.Minute,lastTime.Second);

		compareTime = nextTimeNotif - DateTime.Now;

		return compareTime;
	}

	public static void _NotificationTimer(){
		TimeSpan compareTime;

		if(!string.IsNullOrEmpty(PlayerPrefs.GetString(Key_lastRouletteStar))){
			compareTime = GameData._setTimeNotif();
			Debug.Log ("COMPARE TIME : " + compareTime);
			NotificationController.instance.SendNotification (1, compareTime, "Asal Tebak PUNO", "Come play play, Don’t shy shy");
		}
	}

	public static void checkTotalTambahSoal ()
	{
		if (!PlayerPrefs.HasKey (Key_totalTambahSoal)) {
			PlayerPrefs.SetInt (Key_totalTambahSoal, 5);
			Debug.Log ("totalTambahSoal : " + PlayerPrefs.GetInt (Key_totalTambahSoal));
		} else {
			DateTime timeNow = new DateTime ();
			if (PlayerPrefs.HasKey (GameData.key_lastTimePlay) == false) {
				timeNow = DateTime.Today;
			} else if (PlayerPrefs.HasKey (GameData.key_lastTimePlay) == true && PlayerPrefs.HasKey (GameData.Key_ServerTime) == true) {
				timeNow = GameData.ServerTime;
			} else {
				timeNow = DateTime.Today;
			}


			if(PlayerPrefs.HasKey(GameData.key_lastTimePlay)){
				Debug.Log ("timeNow : " + timeNow);
				Debug.Log("day : " + DateTime.Today);
				Debug.Log("lastDay : " + PlayerPrefs.GetString(GameData.key_lastTimePlay));
//				if(timeNow.ToString() != PlayerPrefs.GetString(GameData.key_lastTimePlay)){
//					PlayerPrefs.SetInt (Key_totalTambahSoal, 5);
//					Debug.Log ("BERHASIL TAMBAHIN TAMBAH SOAL");
//				} 
			}
		}
	}

	public static string getPowerUpName(int powerUpCode){
		string[] powerUpName = new string[TotalPowerUp]{
			pu_ext1,pu_ext2,pu_show1word,pu_show2words,pu_stopTime1,pu_stopTime2,pu_resetStar
		};

		return powerUpName[powerUpCode];
	}

	public static bool isInternetConnect(){
		try{
			using(var client = new WebClient()){
				using (var stream  = client.OpenRead("https://www.google.co.id/")){
					Debug.Log ("INTERNET CONNECTION IS AVAILABLE");
					return true;
				}
			}
		}catch{
			Debug.Log ("INTERNET CONNECTION IS NOT AVAILABLE");
			return false;
		}
	}

	public static void soundCondSetting(){
		AudioSource source,source2,source3,source4;

		if(_soundDisable == false){//turn on
			source = GameObject.Find (GameData.SFX_SOUNDSOURCE).GetComponent<AudioSource> ();
			source2 = GameObject.Find (GameData.SFX2_SOUNDSOURCE).GetComponent<AudioSource> ();
			source3 = GameObject.Find (GameData.SFX3_SOUNDSOURCE).GetComponent<AudioSource> ();
			source4 = GameObject.Find (GameData.BGM_SOUNDSOURCE).GetComponent<AudioSource> ();
			source.enabled = true;
			source2.enabled = true;
			source3.enabled = true;
			source4.enabled = true;
			source.clip = source.clip;
			source2.clip = source2.clip;
			source3.clip = source3.clip;
			source4.clip = source4.clip;
			source.Play ();
			source2.Play ();
			source3.Play ();
			source4.Play ();
		}else if(_soundDisable == true){//turn off
			Debug.Log ("testDua");
			source = GameObject.Find (GameData.SFX_SOUNDSOURCE).GetComponent<AudioSource> ();
			source2 = GameObject.Find (GameData.SFX2_SOUNDSOURCE).GetComponent<AudioSource> ();
			source3 = GameObject.Find (GameData.SFX3_SOUNDSOURCE).GetComponent<AudioSource> ();
			source4 = GameObject.Find (GameData.BGM_SOUNDSOURCE).GetComponent<AudioSource> ();
			source.enabled = false;
			source2.enabled = false;
			source3.enabled = false;
			source4.enabled = false;
		}
	}

	public static void soundSourceAnotherGO(string sourceObject, AudioSource source, AudioClip clip){ //play sound on another game object (GO)
		source = GameObject.Find(sourceObject).GetComponent<AudioSource> ();
		source.clip = clip;
		source.Play ();
	}

	public static void disableBtnFBLogin(){
		GameObject.Find ("FBButton").gameObject.SetActive(false);
	}

	public static void stopSoundSourceAnotherGO(string sourceObject, AudioSource source){ //stop sound on another game object (GO)
		source = GameObject.Find(sourceObject).GetComponent<AudioSource> ();
		source.Stop();
	}

	public static void soundVolume(string sourceObject,AudioSource source,float volume){
		source = GameObject.Find (sourceObject).GetComponent<AudioSource> ();
		source.volume = volume;
	}

	public static void maxCountLetter(){
		if(GameData._letterChecked > GameData._totalLetters){
			GameData._letterChecked = GameData._totalLetters;
		}
	}

	public static void minCountLetter(){
		if(GameData._letterChecked <= 0){
			GameData._letterChecked = 0;
		}
	}

	public static void resetVariable(){
		Debug.Log ("RESET");
		_usedRoulette = 3;
		_onMenuScreen = false;
		_totalLetters = 0;
		_letterChecked = 0;
	}

	public static int loadWorldComicUnlocked(){
		int worldComic;
		worldComic = PlayerPrefs.GetInt (Key_World_Comic);

		return worldComic;
	}

	public static void SetStarTotal(int value){
		Star_Total = value;
	}

	public static int GetStarTotal(){
		Star_Total = CalculateStar();
		return Star_Total;
	}

	public static void GoToScene(string SceneName){
		//Application.LoadLevel (SceneName);
		Application.LoadLevelAsync(SceneName);
		//SceneManager.LoadSceneAsync(SceneName,LoadSceneMode.Single);
		//PlayerPrefs.SetString(GameData.Key_SceneToGo,SceneName);
		//Application.LoadLevel(GameData.Scene_Loading);
	}

	public static void Save(){
		int[] tempWorldStages = new int[90];
		for(int World = 1; World <= GameData.TotalWorld ; World++){			
			switch(World){
				case 1: tempWorldStages = Star_World1; break;
				case 2: tempWorldStages = Star_World2; break;
				case 3: tempWorldStages = Star_World3; break;
				case 4: tempWorldStages = Star_World4; break;
				case 5: tempWorldStages = Star_World5; break;
				case 6: tempWorldStages = Star_World6; break;
				case 7: tempWorldStages = Star_World7; break;
				case 8: tempWorldStages = Star_World8; break;
				case 9: tempWorldStages = Star_World9; break;
				case 10: tempWorldStages = Star_World10; break;
				case 11: tempWorldStages = Star_World11; break;
				case 12: tempWorldStages = Star_World12; break;
			}
			
			for(int Stage = 0;Stage < GameData.TotalStagePerWorld; Stage++){
				string StarDataPrefKey = "StarWorld"+World.ToString()+"Stage"+Stage.ToString();
				PlayerPrefs.SetInt(StarDataPrefKey,tempWorldStages[Stage]);
			}
		}

		PlayerPrefs.Save();
	}

	public static void Load(){
		//LOAD STARS
		// format key => StarW1S1
		for(int World = 0; World <= GameData.TotalWorld ; World++){
			for(int Stage = 0;Stage < GameData.TotalStagePerWorld; Stage++){
				string StarDataPrefKey = "StarWorld"+World.ToString()+"Stage"+Stage.ToString();

				if (World == 0) {
					Star_World1 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}else if (World == 1) {
					Star_World2 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}else if (World == 2) {
					Star_World3 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}else if (World == 3) {
					Star_World4 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}else if (World == 4) {
					Star_World5 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}else if (World == 5) {
					Star_World6 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}else if (World == 6) {
					Star_World7 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}else if (World == 7) {
					Star_World8 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}else if (World == 8) {
					Star_World9 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}
				else if (World == 9) {
					Star_World10 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}
				else if (World == 10) {
					Star_World11 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}
				else if (World == 11) {
					Star_World12 [Stage] = PlayerPrefs.GetInt (StarDataPrefKey);
				}
			}
		}
	}

	public static int totalStarWorld (int world)
	{
		int totalStar = 0;

		if (world == 0) {
			for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
						Debug.Log("normalmode clearedvalue:"+totalStar);
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World1[stage];
			}
		}else if(world == 1){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World2[stage];
			}
		}else if(world == 2){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World3[stage];
			}
		}else if(world == 3){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World4[stage];
			}
		}else if(world == 4){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World5[stage];
			}
		}else if(world == 5){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World6[stage];
			}
		}else if(world == 6){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World6[stage];
			}
		}else if(world == 7){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World6[stage];
			}
		}else if(world == 8){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
				//totalStar += Star_World6[stage];
			}
		}else if(world == 9){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
			}
		}else if(world == 10){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
			}
		}else if(world == 11){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
			}
		}else if(world == 12){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
			}
		}else if(world == 13){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
			}
		}else if(world == 14){
			for(int stage=0;stage<GameData.TotalStagePerWorld;stage++){
				if (GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
					if (PlayerPrefs.HasKey ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) && PlayerPrefs.GetInt ("StarWorld" + world.ToString () + "Stage" + stage.ToString ()) > 0) {
						totalStar++;
					}
				} else if(GameData.GilaMode.iGilaMode == 1){
					if (GilaMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("gilamode clearedvalue:"+totalStar);
					}
				} else if(GameData.EnglishMode.iEnglishMode == 1){
					if (EnglishMode.GetStarValue(world,stage) > 0) {
						totalStar++;
						Debug.Log("englishmode clearedvalue:"+totalStar);
					}
				}
			}
		}

		return totalStar;
	}

	public static int totalStarAllWorld(){
		int world;
		int totalStar=0;

		for (world = 0; world < GameData.TotalWorld; world++) {
			if (world == 0) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World1 [stage];
				}
			} else if (world == 1) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World2 [stage];
				}
			} else if (world == 2) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World3 [stage];
				}
			} else if (world == 3) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World4 [stage];
				}
			} else if (world == 4) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World5 [stage];
				}
			} else if (world == 5) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World6 [stage];
				}
			} else if (world == 6) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World7 [stage];
				}
			} else if (world == 7) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World8 [stage];
				}
			} else if (world == 8) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World9 [stage];
				}
			} else if (world == 9) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World10 [stage];
				}
			}else if (world == 10) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World11 [stage];
				}
			}else if (world == 11) {
				for (int stage = 0; stage < GameData.TotalStagePerWorld; stage++) {
					totalStar += Star_World12 [stage];
				}
			}

		}

		return totalStar;
	}

	public static void _setPowerUpAmount(){
		//powerUpDataPrefKey format : powerUp1 = extTime1 | powerUp2 : extTime2 | powerUp3 : show1Word | powerUp4 : show2Word | powerUp5 : stopTime1 | powerUp6 : stopTime2
		string powerUpPrefKey = "";

		for(int powerUp = 1 ; powerUp <= TotalPowerUp ; powerUp++){
			powerUpPrefKey = GameData.Key_PowerUp + powerUp.ToString();
			PlayerPrefs.SetInt (powerUpPrefKey,0);
		}
	}

	public static string _getPowerUpAmount(Text textAmount,int indexPrefKey){
		//powerUpDataPrefKey format : powerUp1 = extTime1 | powerUp2 : extTime2 | powerUp3 : show1Word | powerUp4 : show2Word | powerUp5 : stopTime1 | powerUp6 : stopTime2
		string powerUpPrefKey;
		int powerUpAmount;

		powerUpPrefKey = GameData.Key_PowerUp + indexPrefKey;
		powerUpAmount = PlayerPrefs.GetInt (powerUpPrefKey);
		textAmount.text = powerUpAmount.ToString();

		return textAmount.text;
	}

	public static void checkPowerUpInventory(){
		for(int checking=1;checking<=GameData.TotalPowerUp;checking++){
			if(PlayerPrefs.HasKey(GameData.Key_PowerUp + checking.ToString())){
				ownPowerUp += 1;
			}
		}

		if (ownPowerUp == 0) { //first time play make new power up data, 0 means don't have power up
			_setPowerUpAmount ();
		}
	}

	public static void _savePowerUpAmount(Text textAmount,int indexPrefKey){
		//powerUpDataPrefKey format : powerUp1 = extTime1 | powerUp2 : extTime2 | powerUp3 : show1Word | powerUp4 : show2Word | powerUp5 : stopTime1 | powerUp6 : stopTime2
		string powerUpPrefKey;
		int powerUpAmount;

		powerUpPrefKey = GameData.Key_PowerUp + indexPrefKey;
		powerUpAmount = int.Parse (textAmount.text);
		PlayerPrefs.SetInt (powerUpPrefKey,powerUpAmount);
	}

	public static void loadUsingPowerUp(){
		usingPowerUp = PlayerPrefs.GetInt (Key_usingPowerUp);
	}

	public static void updateUsingPowerUp(){
		usingPowerUp++;
		PlayerPrefs.SetInt (Key_usingPowerUp,usingPowerUp);
	}

	#endregion
	//----------------------------------------------------------------------------------------------------
	#region PRIVATE functions
	public static void gotOnePowerUp(int powerUpCode,int totalItem){
		Debug.Log ("GOT ONE POWER UP");
		int tempAmount = PlayerPrefs.GetInt(Key_PowerUp+powerUpCode.ToString(),0);
		tempAmount += totalItem;

		PlayerPrefs.SetInt(Key_PowerUp+powerUpCode.ToString(),tempAmount);
	}

	public static void gotTwoPowerUp(int powerUpCode1,int totalItem1,int powerUpCode2,int totalItem2){
		Debug.Log ("GOT TWO POWER UP");
		int tempAmount;

		tempAmount = PlayerPrefs.GetInt(Key_PowerUp+powerUpCode1.ToString(),0);
		tempAmount += totalItem1;

		PlayerPrefs.SetInt(GameData.Key_PowerUp+powerUpCode1.ToString(),tempAmount);

		tempAmount = PlayerPrefs.GetInt(Key_PowerUp+powerUpCode2.ToString(),0);
		tempAmount += totalItem2;

		PlayerPrefs.SetInt(GameData.Key_PowerUp+powerUpCode2.ToString(),tempAmount);
	}

	private static int CalculateStar(){
		int[] tempWorldStages = new int[60];
		int tempStar = 0;
		for(int World = 1;World <= GameData.TotalWorld;World++){

			switch(World){
			case 1: tempWorldStages = Star_World1; break;
			case 2: tempWorldStages = Star_World2; break;
			case 3: tempWorldStages = Star_World3; break;
			}

			for(int Stage = 0;Stage < GameData.TotalStagePerWorld; Stage++){
				tempStar += tempWorldStages[Stage];

			}
		}
		return tempStar;
	}
	#endregion

	#region numerator
	public static IEnumerator waitingToChangeScene(string SceneName){
		yield return new WaitForSeconds (GameData.idleWaitTime);
		Application.LoadLevel (SceneName);
	}
	#endregion

	#region SetGetData
	public static int StageClearedCounter {
		set{ stageClearedCount = value; }
		get{ return stageClearedCount;}
	}

	//get star every stage
	public static int getStarWorld(int stage){
		string StarDataPrefKey = "StarWorld"+worldNumber.ToString()+"Stage"+stage.ToString();

		tempStar = PlayerPrefs.GetInt (StarDataPrefKey);

		StarDataPrefKey = "";

		return tempStar;
	}

	public static int[] _Star_World1{
		set{
			Star_World1 = value;
		}
		get{
			return Star_World1;
		}
	}

	public static int[] _Star_World2{
		set{
			Star_World2 = value;
		}
		get{
			return Star_World2;
		}
	}

	public static int[] _Star_World3{
		set{
			Star_World3 = value;
		}
		get{
			return Star_World3;
		}
	}
	 
	public static string _sceneState{
		set{
			sceneState = value;
		}
		get{
			return sceneState;
		}
	}

	public static string _buttonTag{
		set{
			buttonTag = value;
		}
		get{
			return buttonTag;
		}
	}

	public static string _onMenuScene{
		set{
			onMenuScene = value;
		}
		get{
			return onMenuScene;
		}
	}


	public static string _popUpHintTag{
		set{
			popUpHintTag = value;
		}
		get{
			return popUpHintTag;
		}
	}

	public static string _worldName{
		set{
			worldName = value;
		}
		get{
			return worldName;
		}
	}

	public static string _animTrigger{
		set{
			animTrigger = value;
		}
		get{
			return animTrigger;
		}
	}

	public static string _resultType{
		set{
			resultType = value;
		}
		get{
			return resultType;
		}
	}

	public static string _rewardName{
		set{
			rewardName = value;
		}
		get{
			return rewardName;
		}
	}

	public static Sprite _worldSprite{
		set{
			worldSprite = value;
		}
		get{
			return worldSprite;
		}
	}

	public static int _worldNumber{
		set{
			worldNumber = value;
		}
		get{
			return worldNumber;
		}
	}

	public static int _stageNumber{
		set{
			stageNumber = value;
		}
		get{
			return stageNumber;
		}
	}

	public static int _tempStar{
		set{
			tempStar = value;
		}
		get{
			return tempStar;
		}
	}

	public static int _letterChecked{
		set{
			letterChecked = value;
		}
		get{
			return letterChecked;
		}
	}

	public static int _charIndex{
		set{
			charIndex = value;
		}
		get{
			return charIndex;
		}
	}

	public static int _totalLetters{
		set{
			totalLetters = value;
		}
		get{
			return totalLetters;
		}
	}

	public static float _frameAnim{
		set{
			frameAnim = value;
		}
		get{
			return frameAnim;
		}
	}

	public static bool _checkUnlockedStg{
		set{
			checkUnlockedStg = value;
		}
		get{
			return checkUnlockedStg;
		}
	}

	public static int _usedRoulette{
		set{
			usedRoulette = value;
		}
		get{
			return usedRoulette;
		}
	}

	public static int _randomReward{
		set{
			randomReward = value;
		}
		get{
			return randomReward;
		}
	}

	public static bool _onMenuScreen{
		set{
			onMenuScreen = value;
		}
		get{
			return onMenuScreen;
		}
	}

	public static bool _soundDisable{
		set{
			soundDisable = value;
		}
		get{
			return soundDisable;
		}
	}

	public static bool _isConnected{
		set{
			isConnected = value;
		}
		get{
			return isConnected;
		}
	}

	public static bool _isLoggedIn{
		set{
			isLoggedIn = value;
		}
		get{
			return isLoggedIn;
		}
	}

	public static bool _lineInit {
		set { 
			lineInit = value;
		}
		get{ 
			return lineInit;
		}
	}

//	public static bool _lineLogin{
//		set{
//			lineLogin = value;
//		}
//		get{
//			return lineLogin;
//		}
//	}

	public static bool _newLogin {
		set { 
			newLogin = value;
		}
		get{ 
			return newLogin;
		}
	}

	public static int iPosterHasShown{
		set{PlayerPrefs.SetInt ("Pref/PosterShown", value);}
		get{return PlayerPrefs.GetInt ("Pref/PosterShown", 0);}
	}

	public static int iBannerCount{
		set{PlayerPrefs.SetInt ("Pref/BannerCount", value);}
		get{return PlayerPrefs.GetInt ("Pref/BannerCount", 0);}
	}

	private static bool bHasGetRecentServerTime;
	public static bool _bHasGetRecentServerTime{
		get{ return bHasGetRecentServerTime; }
		set{ bHasGetRecentServerTime = value; }
	}

	public static string loginUserNameValue{
		set{PlayerPrefs.SetString ("Pref/LoginUserName", value);}
		get{return PlayerPrefs.GetString ("Pref/LoginUserName", "");}
	}

	public static string loginAccessTokenValue{
		set{PlayerPrefs.SetString ("Pref/AccessTokenValue", value);}
		get{return PlayerPrefs.GetString ("Pref/AccessTokenValue", "");}
	}

	public static int loginTypeValue {
		set{PlayerPrefs.SetInt ("Pref/loginTypeValue", value);}
		get{return PlayerPrefs.GetInt("Pref/loginTypeValue",0);}
	}

	public static int totalPlayer {
		set { 
			PlayerPrefs.SetInt ("Pref/TotalPlayer", value);
		}
		get{ 
			return PlayerPrefs.GetInt("Pref/TotalPlayer",0);
		}
	}
	#endregion

	/// <summary>
	/// <para>Banner Code marks off Event and IAP Banner</para>
	/// <para>0. No Banner</para>
	/// <para>1. Event Banner</para>
	/// <para>2. IAP Banner</para>
	/// </summary>
	public static int[] BannerCode = new int[2];
	public static Texture[] BannerTexture = new Texture[2];
	public static bool bhasBannerResponse = false;

	public static class GilaMode{
		const string KEY_STAR = "GilaMode/Star";
		const string KEY_GILAMODEUNLOCKED = "GilaMode/UnlockValue";
		//const string KEY_CLEAREDSTAGE = "GilaMode/ClearedStage";

		/// <summary>
		/// <para>Determines GileMode is on or off</para>
		///<para>0 = GilaMode not activated</para>
		///<para>1 = GilaMode activated</para>
		/// </summary>
		public static int iGilaMode {
			get{ return PlayerPrefs.GetInt(KEY_STAR,0);}
			set{ PlayerPrefs.SetInt(KEY_STAR,value);}
		}

		/// <summary>
		/// Sets the star value according to corresponding world and level.
		/// </summary>
		public static void SetStarValue (int World, int Level, int Value){
			string temp = KEY_STAR+"/W"+World+"L"+Level;
			PlayerPrefs.SetInt(temp, Value);
		}

		/// <summary>
		/// Gets the star value according to corresponeing world and level.
		/// </summary>
		public static int GetStarValue(int World, int Level){
			string temp = KEY_STAR+"/W"+World+"L"+Level;
			return PlayerPrefs.GetInt(temp,0);
		}

		public static int iGilaModeUnlockValue {
			get{ return PlayerPrefs.GetInt(KEY_GILAMODEUNLOCKED,0);}
			set{ PlayerPrefs.SetInt(KEY_GILAMODEUNLOCKED,value);}
		}

//		public static void SetClearedStageValue (int World, int Level, int Value){
//			string temp = KEY_CLEAREDSTAGE+"/W"+World+"L"+Level;
//			PlayerPrefs.SetInt(temp, Value);
//		}
//
//		public static int GetClearedStageValue (int World, int Level){
//			string temp = KEY_CLEAREDSTAGE+"/W"+World+"L"+Level;
//			return PlayerPrefs.GetInt(temp,0);
//		}
	}

	public static class EnglishMode{
		const string KEY_STAR = "EnglishMode/Star";
		const string KEY_ENGLISHMODEUNLOCKED = "EnglishMode/UnlockValue";

		/// <summary>
		/// <para>Determines EnglishMode is on or off</para>
		///<para>0 = GilaMode not activated</para>
		///<para>1 = GilaMode activated</para>
		/// </summary>
		public static int iEnglishMode {
			get{ return PlayerPrefs.GetInt(KEY_STAR,0);}
			set{ PlayerPrefs.SetInt(KEY_STAR,value);}
		}

		/// <summary>
		/// Sets the star value according to corresponding world and level.
		/// </summary>
		public static void SetStarValue (int World, int Level, int Value){
			string temp = KEY_STAR+"/W"+World+"L"+Level;
			PlayerPrefs.SetInt(temp, Value);
		}

		/// <summary>
		/// Gets the star value according to corresponeing world and level.
		/// </summary>
		public static int GetStarValue(int World, int Level){
			string temp = KEY_STAR+"/W"+World+"L"+Level;
			return PlayerPrefs.GetInt(temp,0);
		}

		public static int iEnglishModeUnlockValue {
			get{ return PlayerPrefs.GetInt(KEY_ENGLISHMODEUNLOCKED,0);}
			set{ PlayerPrefs.SetInt(KEY_ENGLISHMODEUNLOCKED,value);}
		}
	}
}
