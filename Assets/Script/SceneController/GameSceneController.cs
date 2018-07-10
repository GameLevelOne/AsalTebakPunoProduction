using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Facebook.Unity;
using System.Collections;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;

public class GameSceneController : MonoBehaviour {
	public static GameSceneController instance;
	public GameObject transition,bubbleResult,backToMainMenuBox,hintBox,rouletteMenu,helpBox,fbsharebtn,exitNotification,shopMenu;
	public GameObject txtRandomMode;
	public Text[] textAmount = new Text[GameData.TotalPowerUp];
	public Animator Anim_BubbleResult,Anim_BubbleQuestion,Anim_MainMenuBox,Anim_Hint,Anim_Shop,Anim_Roulette,Anim_GetPrize,Anim_rewardShare,Anim_Help,Anim_SelamatBermain,Anim_BuyNotification,Anim_ExitNotification,Anim_RouletteNotif;
	public Text bubbleResultText;
	public Sprite Sprite_roulettePU;
	public Image rouletteBtn,rouletteBtn2,Img_Wheel;
	public string message;

	public StarController timer;
	public PowerUpController powerUpController;
	public ShopController shopController;

	public AudioSource sfxGame;
	public AudioClip sfxButton;

	public Sprite[] howToPlaySprite = new Sprite[2];
	public Image howToPlayImage;

	private bool isProcessing = false;

	private static bool usedStopTime1, usedStopTime2;

	private string adspot_id="OTc3Ojk2OA";

	void Awake(){
		instance = this;
		//GameObject.Find(GameData.gameObject_PluginController).GetComponent<AdmobController>().RequestInterstitial();
	}

	void Start ()
	{
		firstPlayTutorial ();
		InitializeGame ();
		GameData.loadUsingPowerUp ();
		GameData.soundCondSetting ();

		GameData._onMenuScene = GameData.GAME_MENU;

		if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
			txtRandomMode.SetActive (false);
		} else if (GameData.GilaMode.iGilaMode == 1) {
			txtRandomMode.SetActive (true); 
			txtRandomMode.GetComponent<Text>().text="RANDOM MODE";
		} else if (GameData.EnglishMode.iEnglishMode == 1) {
			txtRandomMode.SetActive (true); 
			txtRandomMode.GetComponent<Text>().text="ENGLISH MODE";
		}
		BackgroundImgController.instance.setBackgroundImage();

//		if(HikeManager.instance)
//			HikeManager.instance.GetAds (adspot_id);
		GoogleMobileAdsDemoScript.instance.RequestInterstitial();
	}

	void Update(){
		rouletteHasBeenUsed ();
		exitGame ();
		#if !UNITY_EDITOR
		GPAchievementController.instance._achievement_first_use_power_up ();
		#endif
	}
	//----------------------------------------------------------------------------------------------------
	#region Button Functions
	//	public void OnNextStageButton(){
	//		
	//		int stage = PlayerPrefs.GetInt(GameData.Key_Stage);
	//		stage++;
	//		PlayerPrefs.SetInt(GameData.Key_Stage,stage);
	//		
	//		ClearCurrentStageData();
	//		
	//		LevelTitleController.instance.SetStageLevel();
	//		
	//		//ResultPanelController.instance.HideResultPanel();
	//		StarController.instance.ResetStar();
	//		
	//		InGameCharacterRandomizer.instance.ResetCharacter();
	//		
	//		InitializeGame();
	//	}
	public void OnBtn_YesExit(){
		//GameData._NotificationTimer ();
		Application.Quit();
	}

	//main menu button
	public void OnMainMenuButton(){
		timer.enabled = false;
		backToMainMenuBox.gameObject.SetActive (true);
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
	}

	public void OnYesButton(){
		GameData.resetVariable ();

		GameData._sceneState = GameData.Scene_Game;
		PlayerPrefs.SetInt (GameData.Key_setDefaultBg,0);
		PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Menu);
		//GameObject.Find(GameData.gameObject_PluginController).GetComponent<AdmobController>().DestroyInterstitial();
		transition.gameObject.SetActive (true);
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
	}
	//---------------------------------------
	//hint button
	public void OnHintButton(){
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
		timer.enabled = false;
		GameData._onMenuScene = GameData.Scene_Game;
		hintBox.gameObject.SetActive (true);

		GameData._onMenuScene = GameData.HINT_MENU;

		updatePowerUpAmount ();
	}

	public void OnShopButton(){
		Debug.Log("onshopbutton");
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);

		shopMenu.SetActive(true);

		GameData._onMenuScene = GameData.Scene_Game;
		GameData._sceneState = GameData.HINT_MENU;
		GameData._buttonTag = GameData.Tag_Shop;
		Anim_Hint.SetTrigger (GameData.Hide);
	}

	public void OnRouletteButton(){
		print ("usedRoulette : " + GameData._usedRoulette);
		if(GameData._usedRoulette > 0){
			openRouletteMenu();
		}
	}

	public void rouletteHasBeenUsed(){
		if(GameData._usedRoulette == 0){
			rouletteBtn.color = new Color (0.5f,0.5f,0.5f,1);
			rouletteBtn.GetComponent<Animator> ().enabled = false;
			rouletteBtn2.color = new Color (0.5f,0.5f,0.5f,1);
			rouletteBtn2.GetComponent<Animator> ().enabled = false;
		}
	}

	public void usedPrizePowerUp(){
		print ("USING POWER UP WITH ROULETTE");
		int prizeCode;

		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if(prizeCode == 1){
			//powerUpController.extendedTime1 ();
			print ("USING THIS POWER UP EX TIME 1");
			PowerUpController.instance.extendedTime1 ();
		}else if(prizeCode == 2){
			//powerUpController.extendedTime2 ();
			print ("USING THIS POWER UP EX TIME 2");
			PowerUpController.instance.extendedTime2 ();
		}else if(prizeCode == 3){
			//powerUpController.showOneWord ();
			print ("USING THIS POWER UP SHOW 1 WORD");
			PowerUpController.instance.showOneWord ();
		}else if(prizeCode == 4){
			//powerUpController.showTwoWord ();
			print ("USING THIS POWER UP SHOW 2 WORD");
			PowerUpController.instance.showTwoWord ();
		}else if(prizeCode == 5){
			//powerUpController._stopTime1 ();
			print ("USING THIS POWER UP STOP TIME 1");
			PowerUpController.instance._stopTime1 ();
		}else if(prizeCode == 6){
			//powerUpController._stopTime2 (); 
			print ("USING THIS POWER UP STOP TIME 2");
			PowerUpController.instance._stopTime2 ();
		}
		PlayerPrefs.SetInt (GameData.Key_prizeCode, 0);  //reset roulette
	}

	//buttonClose
	public void OnBtn_Close (string sceneState)
	{
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxGame, sfxButton);

		GameData._sceneState = sceneState;
		GameData._buttonTag = GameData.Tag_Close;
		if (sceneState == GameData.HINT_MENU) {
			timer.enabled = true;
			//usingStopTime ();
			Anim_Hint.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.SHOP_MENU) {
			updatePowerUpAmount ();
			Anim_Shop.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.ROULETTE_MENU) {
			Anim_Roulette.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.GETPRIZE_MENU) {
			usedPrizePowerUp ();
			GameData._onMenuScene = string.Empty;
			RouletteController.instance._disableSpin ();
			Anim_GetPrize.SetTrigger (GameData.Hide);
		} else if (GameData._sceneState == GameData.HELP_MENU) {
			if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
				firstPlayReward ();
				Debug.Log ("reward");
			} else if (GameData.GilaMode.iGilaMode == 1) {
				PlayerPrefs.SetInt(GameData.Key_firstPlayRandomMode,0);
				timer.enabled=true;
			}
			Anim_Help.SetTrigger (GameData.Close);
		}else if (GameData._sceneState == GameData.REWARDSHARE_MENU) {
			GetReward.instance.rewardList [GameData._randomReward].gameObject.SetActive (false);
			Anim_rewardShare.SetTrigger (GameData.Hide);
		} else if (GameData._sceneState == GameData.BACKTOMAINMENU_MENU) {
			GameData._buttonTag = GameData.Tag_BackToMainMenu;
			timer.enabled = true;
			Anim_MainMenuBox.SetTrigger (GameData.Hide);
		} else if (GameData._sceneState == GameData.GAME_MENU) {
			timer.enabled = true;
			Anim_SelamatBermain.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.BUY_NOTIFICATION_MENU) {
			Anim_BuyNotification.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.EXIT_NOTIF_MENU) {
			Anim_ExitNotification.SetTrigger (GameData.Hide);
		} else if (sceneState == GameData.ROULETTE_NOTIF_MENU) {
			CheckingInternetConnection.instance.checkInternetConnection ();
			Anim_RouletteNotif.SetTrigger (GameData.Hide);
		}
	}
	//-----------------------------------------------

	//	public void OnMainMenuButtonFromResult(){
	//		StartCoroutine(ChangeSceneAfterResultScene(GameData.Scene_Menu));
	//	}
	#endregion
	//----------------------------------------------------------------------------------------------------
	#region PUBLIC Functions
	//screenshot sekarang
	public void shareScreenshot ()
	{
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
		if(!isProcessing){
			StartCoroutine(ShareSS());
		}

		AppsFlyerController.instance._trackRichEvent ("share_content", "do_share_content", "share_content_event");
	}

	public void showBubbleResult(string text){
		//hide bubble question
		Anim_BubbleQuestion.SetTrigger(GameData.Hide);

		//show bubble result
		bubbleResultText.text = text;
		bubbleResult.gameObject.SetActive (true);
	}

	public void HideBubbleResult(){		
		Anim_BubbleResult.SetTrigger (GameData.Close);
	}

	public void ShowBubbleQuestion(){		
		Anim_BubbleQuestion.SetTrigger (GameData.Show);
	}

	public void CloseTransition(){
		transition.gameObject.SetActive (true);
	}
	#endregion
	//----------------------------------------------------------------------------------------------------
	#region PRIVATE Functions
	void exitGame(){
		if(Input.GetKeyUp(KeyCode.Escape)){
			exitNotification.gameObject.SetActive (true);
		}
	}

	void updatePowerUpAmount(){
		//minus reset star
		for(int powerUpCode=0;powerUpCode<(GameData.TotalPowerUp-1);powerUpCode++){
			textAmount [powerUpCode].text = GameData._getPowerUpAmount (textAmount [powerUpCode], powerUpCode+1);
		}
	}

	void openRouletteMenu(){
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);

		timer.enabled = false;
		rouletteMenu.gameObject.SetActive (true);
	}

	void firstPlayReward(){
		if(PlayerPrefs.GetInt(GameData.Key_firstPlay) == 1){
			print ("TEST");
			int tempAmount;

			//get add 10 star currency
			tempAmount = PlayerPrefs.GetInt (GameData.Key_starCurrency);
			tempAmount += 10;
			PlayerPrefs.SetInt (GameData.Key_starCurrency,tempAmount);
			shopController.GetComponent<ShopController> ().updateTotalStar ();

			//get 10 power up every power up -> minus reset star
			for(int powerUpCode=0;powerUpCode<6;powerUpCode++){
				tempAmount = PlayerPrefs.GetInt (GameData.Key_PowerUp+(powerUpCode+1).ToString());
				tempAmount += 10;
				print ("TEST 1 : " + tempAmount);
				PlayerPrefs.SetInt(GameData.Key_PowerUp+(powerUpCode+1).ToString(),tempAmount);
				textAmount [powerUpCode].text = GameData._getPowerUpAmount (textAmount [powerUpCode], powerUpCode+1);
			}

			PlayerPrefs.SetInt (GameData.Key_firstPlay,0);
		}
	}

	void firstPlayTutorial ()
	{
		if (GameData.EnglishMode.iEnglishMode == 0) {
			if (GameData.GilaMode.iGilaMode == 0) {
				if (PlayerPrefs.GetInt (GameData.Key_firstPlay) == 1) {
					timer.enabled = false;
					helpBox.gameObject.SetActive (true);
				}
			} else {
				if (PlayerPrefs.GetInt (GameData.Key_firstPlayRandomMode, 1) == 1) {
					timer.enabled = false;
					helpBox.gameObject.SetActive (true);
				}
			}
			howToPlayImage.sprite = howToPlaySprite [GameData.GilaMode.iGilaMode];
		}
	}

	void InitializeGame(){
		InGameCharacterRandomizer.instance.GenerateCharacter();

		QuestionGenerator.instance.GenerateQuestion();
		QuestionGenerator.instance.validatingNextRow ();
		AnswerGenerator.instance.GenerateAnswer();

		CursorController.instance.SetCharValues(AnswerGenerator.instance.AnswerChars);
	}

	#if UNITY_IOS
	public struct ConfigStruct
	{
		public string title;
		public string message;
	}

	[DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);

	public struct SocialSharingStruct
	{
		public string text;
		public string url;
		public string image;
		public string subject;
	}

	[DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);

	public static void CallSocialShare(string title, string message)
	{
		ConfigStruct conf = new ConfigStruct();
		conf.title  = title;
		conf.message = message;
		showAlertMessage(ref conf);
	}


	public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
	{
		SocialSharingStruct conf = new SocialSharingStruct();
		conf.text = defaultTxt;
		conf.url = url;
		conf.image = img;
		conf.subject = subject;

		showSocialSharing(ref conf);
	}
	#endif

	//using stop time 1 : 5s / 2 : 20s
	//	void usingStopTime(){
	//		if(usedStopTime1 == true){//5s
	//			powerUpController._usingStopTime1 ();
	//		}else if(usedStopTime2 == true){//20s
	//			powerUpController._usingStopTime2 ();
	//		}
	//	}
	#endregion
	//----------------------------------------------------------------------------------------------------
	#region set_get_variable
	public static bool _usedStopTime1{
		set{
			usedStopTime1 = value;
		}
		get{
			return usedStopTime1;
		}
	}

	public static bool _usedStopTime2{
		set{
			usedStopTime2 = value;
		}
		get{
			return usedStopTime2;
		}
	}
	#endregion
	//----------------------------------------------------------------------------------------------------
	#region Coroutines
	//share screenshot sekarang
	public IEnumerator ShareSS()
	{
		isProcessing = true;
		// wait for graphics to render
		yield return new WaitForEndOfFrame();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		// create the texture
		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		// put buffer into texture
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		// apply
		screenTexture.Apply();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		byte[] dataToSave = screenTexture.EncodeToPNG();
		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		File.WriteAllBytes(destination, dataToSave);
		if(!Application.isEditor)
		{
			#if UNITY_ANDROID
			// block to open the file and share it ------------START
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), ""+ message);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

			currentActivity.Call("startActivity", intentObject);

			#elif UNITY_IOS
			CallSocialShareAdvanced(message, "PUNO", "", destination);
			#endif

		}
		isProcessing = false;
		//buttonShare.enabled = true;
	}
	#endregion
	//----------------------------------------------------------------------------------------------------
}
