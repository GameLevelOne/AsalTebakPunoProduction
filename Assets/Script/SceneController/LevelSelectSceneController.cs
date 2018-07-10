using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Facebook.Unity;

public class LevelSelectSceneController : MonoBehaviour {
	public static LevelSelectSceneController self;
	const string Tag_Btn = "Btn_LevelSelectScene";

	public Animator Anim_World;
	public Sprite jakartaSprite,jakartaSprite1,jakartaSprite2,padangSprite,padangSprite1,padangSprite2,
	baliSprite,baliSprite1,baliSprite2,torajaSprite,torajaSprite1,torajaSprite2,jogjaSprite,jogjaSprite1,jogjaSprite2;
	public GameObject Obj_WorldSelect, Obj_StageSelect,obj_popUpLogin,transition,buatSoalBtn,bgmMenu,notifTambahSoal,resetStarNotif,resetStarConfirm;
	public AudioSource sfxMenu;
	public AudioClip sfxButton;
	public Text resetStarNotifText,resetStarConfirmText,resetStarAmountText;

	void Awake(){
		self = this;
	}

	void Start(){
		
	}
//----------------------------------------------------------------------------------------------------
	#region Button functions
//	public void SetWorldImage(int imageCode){
//		switch(imageCode){
//		case 1: GameData._worldSprite = jakartaSprite; break;
//		case 2:	GameData._worldSprite = jakartaSprite1;	break;
//		case 3:	GameData._worldSprite = jakartaSprite2;	break;
//		case 4: GameData._worldSprite = baliSprite; break;
//		case 5:	GameData._worldSprite = baliSprite1; break;
//		case 6:	GameData._worldSprite = baliSprite2; break;
//		case 7: GameData._worldSprite = padangSprite; break;
//		case 8:	GameData._worldSprite = padangSprite1; break;
//		case 9:	GameData._worldSprite = padangSprite2; break;	
//		case 10: GameData._worldSprite = torajaSprite; break;
//		case 11: GameData._worldSprite = torajaSprite1; break;
//		case 12: GameData._worldSprite = torajaSprite2; break;		
//		}
//	}

	public void SetWorldName(string worldName){
		GameData._worldName = worldName;
	}

	public void OnBuatSoalButton(){
		#region replaced with coming soon banner
//		if (FB.IsLoggedIn) {
//			if (PlayerPrefs.GetInt (GameData.Key_totalTambahSoal) >0) {
//				PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.Scene_TambahSoal);
//				transition.gameObject.SetActive (true);
//			} else {
//				notifTambahSoal.gameObject.SetActive (true);
//			}
//		} else {
//			GameData._sceneState = GameData.WORLD_MENU;
//			GameData._buttonTag = GameData.Tag_BuatSoal;
//			Anim_World.SetTrigger (GameData.Hide);
//		}
		#endregion

		GameData._sceneState = GameData.WORLD_MENU;
		GameData._buttonTag = GameData.Tag_BuatSoal;
		Anim_World.SetTrigger (GameData.Hide);

		//sementara------------------------------------------
//		Application.LoadLevel (GameData.Scene_TambahSoal);
//		DontDestroyOnLoad(bgmMenu);
		//---------------------------------------------------
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	//intro button
	public void OnIntroButton ()
	{
		int world;

		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxMenu, sfxButton);

		world = PlayerPrefs.GetInt (GameData.Key_World);
		if (world == 0 || world == 1 || world == 2) {
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_jkt_1);
		} else if (world == 3 || world == 4 || world == 5) {
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_bl_1);
		} else if (world == 6 || world == 7 || world == 8) {
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_pd_1);
		} else if (world == 9 || world == 10 || world == 11) {
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_trj);
		} else if (world == 12 || world == 13 || world == 14) {
			PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_jog);
		}

		GameData._sceneState = GameData.Scene_Menu;
		transition.gameObject.SetActive (true);
	}

	//World Select
	public void OnWorldButton (int world)
	{
		GoogleMobileAdsDemoScript.instance.HideBannerAd();
		Debug.Log ("onworldbutton " + " world:" + world);
		PlayerPrefs.SetInt (GameData.Key_World, world);
		int worldComic = GameData.loadWorldComicUnlocked ();

		GameData._worldNumber = world;

		PlayerPrefs.SetInt (GameData.Key_Background, world);
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxMenu, sfxButton);

		if (worldComic == world) { //play comic for first time in this world
			
			GameData._sceneState = GameData.Scene_Menu;

			if (worldComic == 0) {
				PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_jkt_1);
			} else if (worldComic == 3) {
				PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_bl_1);
			} else if (worldComic == 6) {
				PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_pd_1);
			} else if (worldComic == 9) {
				PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_trj);
			} else if (worldComic == 12) {
				PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.comic_jog);
			}
			transition.gameObject.SetActive (true);

		} else { //already play comic before, in this world
			GameData._buttonTag = GameData.Tag_World;
			GameData._sceneState = GameData.WORLD_MENU;
			GameData._checkUnlockedStg = true;
			Anim_World.SetTrigger (GameData.Close);
			print("SOMETHING");
			WorldSelectController.self.SetWorldNameAndIcon(world);
			BackgroundImgController.instance.setBackgroundImage();
		}

		#if !UNITY_EDITOR
		worldSelectTracking (world);
		#endif

	}

	public void worldSelectTracking (int world)
	{
		if (world == 0) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "jakarta1", "world_select_event");
		} else if (world == 1) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "jakarta2", "world_select_event");
		} else if (world == 2) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "jakarta3", "world_select_event");
		} else if (world == 3) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "bali1", "world_select_event");
		} else if (world == 4) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "bali2", "world_select_event");
		} else if (world == 5) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "bali3", "world_select_event");
		} else if (world == 6) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "padang1", "world_select_event");
		} else if (world == 7) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "padang2", "world_select_event");
		} else if (world == 8) {
			AppsFlyerController.instance._trackRichEvent ("world_select", "padang3", "world_select_event");
		} else if(world == 9){
			AppsFlyerController.instance._trackRichEvent ("world_select", "toraja1", "world_select_event");
		} else if(world == 10){
			AppsFlyerController.instance._trackRichEvent ("world_select", "toraja2", "world_select_event");
		} else if(world == 11){
			AppsFlyerController.instance._trackRichEvent ("world_select", "toraja3", "world_select_event");
		} else if(world == 12){
			AppsFlyerController.instance._trackRichEvent ("world_select", "jogja1", "world_select_event");
		} else if(world == 13){
			AppsFlyerController.instance._trackRichEvent ("world_select", "jogja2", "world_select_event");
		} else if(world == 14){
			AppsFlyerController.instance._trackRichEvent ("world_select", "jogja3", "world_select_event");
		}

	}

	//Stage Select
	public void OnStageButton(int stage){//dalam parameter sebelumnya yaitu int stage
		int firstStage=0;

		GoogleMobileAdsDemoScript.instance.HideBannerAd();

		//if questions are random
		//stage = Random.Range (0,(GameData.TotalStagePerWorld-1));

		if (stage == 0)
			firstStage = 1;

		Debug.Log ("soal:" + stage);

		//to game scene
		if((PlayerPrefs.GetInt(GameData.Key_World) == 0 && firstStage == 1) && !PlayerPrefs.HasKey(GameData.Key_firstPlay)){
			PlayerPrefs.SetInt (GameData.Key_firstPlay, 1);
		}

		GameData._sceneState = GameData.GAME_MENU;
		PlayerPrefs.SetInt (GameData.Key_setDefaultBg,1);
		PlayerPrefs.SetInt(GameData.Key_Stage,stage);
		PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Game);
		transition.gameObject.SetActive (true);
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
		//StartCoroutine(ChangeScene(GameData.Scene_Game));
	}

	public void StageText(int stageText){
		PlayerPrefs.SetInt (GameData.Key_StageText,stageText);
	}

	//Submit Soal
	public void OnSubmitButton(){
		//submit
	}

	public void OnResetStarConfirmation ()
	{
		int currentWorld = PlayerPrefs.GetInt(GameData.Key_World);

		resetStarConfirm.SetActive (true);
		resetStarConfirmText.text = "Ingin reset star untuk "+GameData.worldNameList[currentWorld]+" ?";
	}

	public void OnResetStarYes ()
	{
		MenuSceneController.instance.OnBtn_Close (GameData.RESET_STAR_CONFIRM);

		string resetStarPowerUp = "powerUp7";
		int currentResetAmount = PlayerPrefs.GetInt (resetStarPowerUp, 0);
		int currentWorld = PlayerPrefs.GetInt (GameData.Key_World);

		if (currentResetAmount > 0) {
			if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
				Debug.Log ("reset world " + currentWorld + " star");
				for (int i = 0; i < GameData.TotalStagePerWorld; i++) {
					PlayerPrefs.SetInt ("StarWorld" + currentWorld.ToString () + "Stage" + i.ToString (), 0);
				}
			} else if (GameData.GilaMode.iGilaMode == 1) {
				Debug.Log ("reset world " + currentWorld + " star");
				for (int i = 0; i < GameData.TotalStagePerWorld; i++) {
					GameData.GilaMode.SetStarValue (currentWorld, i, 0);
				}
			} else if (GameData.EnglishMode.iEnglishMode == 1) {
				Debug.Log ("reset world " + currentWorld + " star");
				for (int i = 0; i < GameData.TotalStagePerWorld; i++) {
					GameData.EnglishMode.SetStarValue (currentWorld, i, 0);
				}
			}

			currentResetAmount--;
			PlayerPrefs.SetInt (resetStarPowerUp, currentResetAmount);
			resetStarNotif.SetActive (true);
			resetStarNotifText.text = "Berhasil reset star untuk " + GameData.worldNameList [currentWorld];
			resetStarAmountText.text = "x " + PlayerPrefs.GetInt (resetStarPowerUp).ToString ();

		} else {
			resetStarNotif.SetActive(true);
			resetStarNotifText.text="Item reset star tidak mencukupi";

			Debug.Log("no power up");
		}
	}

	public void OnResetStarNo(){
		MenuSceneController.instance.OnBtn_Close(GameData.RESET_STAR_NOTIF);
	}

	public void OnResetStarNotification (){
		MenuSceneController.instance.OnBtn_Close(GameData.RESET_STAR_CONFIRM);
	}
	/*public void OnBackButton(){
		ShowWorldSelect();
	}*/
	#endregion
//----------------------------------------------------------------------------------------------------
	#region PUBLIC Functions


	public void EnableSceneButtons(){
		GameObject[] buttons = GameObject.FindGameObjectsWithTag(Tag_Btn);
		foreach (GameObject button in buttons){
			button.GetComponent<Button>().enabled = true;
		}
	}
	public void DisableSceneButtons(){
		GameObject[] buttons = GameObject.FindGameObjectsWithTag(Tag_Btn);
		foreach (GameObject button in buttons){
			button.GetComponent<Button>().enabled = false;
		}
	}
	#endregion
//----------------------------------------------------------------------------------------------------
	#region PRIVATE Functions

	#endregion
//----------------------------------------------------------------------------------------------------
	#region Coroutines
	IEnumerator ChangeScene(string SceneName){		
		yield return new WaitForSeconds(GameData.SceneWaitTime);
		
		GameData.GoToScene(SceneName);
	}
	#endregion

}
