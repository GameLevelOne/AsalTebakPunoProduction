using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnswerContainer : MonoBehaviour {
	public static AnswerContainer instance;
	public StarController timer;
	public GameObject transition;
	private string CorrectAnswer;
	private string UserAnswer;
	private int CursorIndex,currentStage,currentWorld;
	public string StarDataPrefKey;
	public Animator ANIM_BubbleResult;
	public Text[] ANSWER_CHARS;
	public AudioSource sfxGame,sfxGame2;
	public AudioClip charTalkBad,charTalkGood,hooray;

	void Awake(){
		instance = this;
	}

	#region public function
	public void ResetContainer(){
		CorrectAnswer = null;
		ANSWER_CHARS = null;
	}

	public void setCorrectAnswer(string value){
		CorrectAnswer = value;
	}

	public void SetAnswerChars(GameObject[] Chars){
		ANSWER_CHARS = new Text[Chars.Length];
		for(int i=0;i<ANSWER_CHARS.Length;i++){
			ANSWER_CHARS[i] = Chars[i].GetComponent<Text>();
		}
	}

	public void UpdateAnswer(string Value, CursorMoveType type){
		CursorIndex = CursorController.instance.CursorPositionIndex;

		if(Value != "_" && type == CursorMoveType.FORWARD){
			//replace current indexed char with provided alphabet
			//CursorIndex = CursorController.instance.CursorPositionIndex;
			increaseLetterChecked();

			ANSWER_CHARS[CursorIndex].text = Value;

			GameData.maxCountLetter ();

			CursorController.instance.UpdateCursorPosition(type);
			CursorController.instance.ShowHideSubmitButton();//ANSWER_CHARS.Length, ANSWER_CHARS[ANSWER_CHARS.Length -1].text,
		}else if(Value == "_" && type == CursorMoveType.BACKWARD){
			//replace current indexed char with provided alphabet
			if(ANSWER_CHARS[CursorIndex].text != "_"){ //Apabila index char tersebut memiliki huruf
				decreaseLetterChecked();

				CursorIndex = CursorController.instance.CursorPositionIndex;
				ANSWER_CHARS[CursorIndex].text = Value;

				//GameData._charIndex = -1;
				CursorController.instance.ShowHideSubmitButton();
			}else if(CursorController.instance.CursorPositionIndex != ANSWER_CHARS.Length-1){
				decreaseLetterChecked();

				CursorController.instance.UpdateCursorPosition(type);
				CursorController.instance.ShowHideSubmitButton();//ANSWER_CHARS.Length, ANSWER_CHARS[ANSWER_CHARS.Length -1].text,

				CursorIndex = CursorController.instance.CursorPositionIndex;
				ANSWER_CHARS[CursorIndex].text = Value;
			}else if(CursorController.instance.CursorPositionIndex == ANSWER_CHARS.Length-1 && ANSWER_CHARS[ANSWER_CHARS.Length-1].text == "_"){
				decreaseLetterChecked();

				CursorController.instance.UpdateCursorPosition(type);
				CursorController.instance.ShowHideSubmitButton();//ANSWER_CHARS.Length, ANSWER_CHARS[ANSWER_CHARS.Length -1].text,

				CursorIndex = CursorController.instance.CursorPositionIndex;
				ANSWER_CHARS[CursorIndex].text = Value;
			}else if(CursorController.instance.CursorPositionIndex == ANSWER_CHARS.Length-1){
				decreaseLetterChecked();

				CursorIndex = CursorController.instance.CursorPositionIndex;
				ANSWER_CHARS[CursorIndex].text = Value;

				CursorController.instance.ShowHideSubmitButton();//ANSWER_CHARS.Length, ANSWER_CHARS[ANSWER_CHARS.Length -1].text,
			}
			GameData.minCountLetter ();
		}
		Debug.Log ("letterChecked : " + GameData._letterChecked + " | totalLetters : " + GameData._totalLetters);
	}
	#endregion

	#region private function
	private void decreaseLetterChecked(){
		try{
			if (ANSWER_CHARS [CursorIndex-1].text != "_") {
				GameData._letterChecked -= 1;
			}
		}catch{
			print ("it's okay");
		}
	}

	private void increaseLetterChecked(){
		if (ANSWER_CHARS [CursorIndex].text == "_") {
			GameData._letterChecked += 1;
		}
	}
	#endregion

	#region Answer Checking
	private ResultType result;
	//private string result;
	public void CheckAnswer(){
		//combine letters to string
		for(int i = 0;i<ANSWER_CHARS.Length;i++){
			if(ANSWER_CHARS[i].text == string.Empty){
				UserAnswer += " ";
			}
			else{
				UserAnswer += ANSWER_CHARS[i].text;
			}
		}

		//check answer per letter
		int point = CorrectAnswer.Length;
		for(int i = 0;i<CorrectAnswer.Length;i++){
			char a = CorrectAnswer[i];
			char b = UserAnswer[i];

			if(a != b){ point--; }
		}

		float ScorePercentage = (float) point /(float) CorrectAnswer.Length;

		// < 80% = wrong
		// 80% < x < 100% = almost
		// 100% = right
		if (ScorePercentage < 0.8f) { 
			//wrong
			wrongResult();
		} else if (ScorePercentage >= 0.8f && ScorePercentage < 1f) {
			//almost
			almostResult();
		} else {
			correctResult ();
		}
		//ANIM_BubbleResult.CrossFade ("show",1);

		//show result UI and clear answer
		UserAnswer = string.Empty;
	}
	#endregion

	#region result controller
	public void wrongResult(){
		result = ResultType.WRONG;
		StarController.instance.RemoveStar();

		GameData._animTrigger = GameData.isWrong;

		GameSceneController.instance.showBubbleResult ("Salah, ayo tebak lagi !");
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,charTalkBad);
	}

	public void almostResult(){
		result = ResultType.ALMOST;
		StarController.instance.RemoveStar();

		GameData._animTrigger = GameData.isAlmost;

		GameSceneController.instance.showBubbleResult ("Hampir benar, ayo tebak lagi !");
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,charTalkBad);
	}

	public void correctResult ()
	{
		//unlock next stage for development mode
//		currentWorld = PlayerPrefs.GetInt (GameData.Key_World);
//		currentStage = PlayerPrefs.GetInt (GameData.Key_Stage);
//		if(currentWorld != 0 && currentStage < 1){//ini cuman sementara
//			int world = PlayerPrefs.GetInt(GameData.Key_World);
//			currentStage = PlayerPrefs.GetInt (GameData.Key_Stage) + 1; //yang mau di unlock stage selanjutnya
//			string StarDataPrefKey = "StarWorld"+world.ToString()+"Stage"+currentStage.ToString();
//			PlayerPrefs.SetInt (StarDataPrefKey,0);
//		}else if(currentWorld == 0 && currentStage < 9){
//			int world = PlayerPrefs.GetInt(GameData.Key_World);
//			currentStage = PlayerPrefs.GetInt (GameData.Key_Stage) + 1; //yang mau di unlock stage selanjutnya
//			string StarDataPrefKey = "StarWorld"+world.ToString()+"Stage"+currentStage.ToString();
//			PlayerPrefs.SetInt (StarDataPrefKey,0);
//		}

		//unlock next stage
		currentWorld = PlayerPrefs.GetInt (GameData.Key_World);
		currentStage = PlayerPrefs.GetInt (GameData.Key_StageText);

		print ("world : " + currentWorld);
		print ("stage : " + currentStage);

		if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
			StarDataPrefKey = "StarWorld" + currentWorld.ToString () + "Stage" + currentStage.ToString ();

			print ("prefkey 1 : " + StarDataPrefKey);

			if (!PlayerPrefs.HasKey (StarDataPrefKey)) {
				if (PlayerPrefs.GetInt (GameData.Key_Stage) < GameData.TotalStagePerWorld) {
					currentWorld = PlayerPrefs.GetInt (GameData.Key_World);
					currentStage = PlayerPrefs.GetInt (GameData.Key_StageText);
					StarDataPrefKey = "StarWorld" + currentWorld.ToString () + "Stage" + currentStage.ToString ();
					print ("prefkey 2 : " + StarDataPrefKey);
					PlayerPrefs.SetInt (StarDataPrefKey, 0);
				}
			}
		} 

		//unlock next world for development mode
//		currentWorld = PlayerPrefs.GetInt (GameData.Key_World);
//		currentStage = PlayerPrefs.GetInt (GameData.Key_Stage);
//		if(currentWorld == 0 && currentStage == 9){//ini cuman sementara
//			currentWorld = PlayerPrefs.GetInt (GameData.Key_World)+1;
//			PlayerPrefs.SetInt (GameData.Key_UnlockedWorld + currentWorld,currentWorld);
//
//			GP_Achievement.instance._achievement_clear_world_1_jakarta ();
//			GP_Achievement.instance._achievement_clear_world_3_bali ();
//			GP_Achievement.instance._achievement_clear_world_5_padang ();
//		} else if(currentWorld != 0 && currentStage == 1){
//			currentWorld = PlayerPrefs.GetInt (GameData.Key_World)+1;
//			PlayerPrefs.SetInt (GameData.Key_UnlockedWorld + currentWorld,currentWorld);
//		}

		//unlock next world
		if(PlayerPrefs.GetInt (GameData.Key_StageText) == GameData.TotalStagePerWorld){
			currentWorld = PlayerPrefs.GetInt (GameData.Key_World)+1;
			PlayerPrefs.SetInt (GameData.Key_UnlockedWorld + currentWorld,currentWorld);
			Debug.Log("StageText:"+PlayerPrefs.GetInt (GameData.Key_StageText));
			Debug.Log("currentWorld:"+currentWorld);
		}

		//disable time count down
		timer.enabled = false;
		StarController.instance.saveLastTime ();

		//correct
		result = ResultType.CORRECT;
		StarController.instance.SetStar();

		#if !UNITY_EDITOR
		GPAchievementController.instance._achievement_collect_30_star ();
		GPAchievementController.instance._achievement_collect_60_star ();
		#endif
		//GameObject.Find(GameData.gameObject_PluginController).GetComponent<AdmobController>().DestroyInterstitial();

		//change scene
		PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Result);
		PlayerPrefs.SetString(GameData.Key_Result,result.ToString());
		GameData._animTrigger = GameData.isCorrect;
		//transition.gameObject.SetActive (true);

		//bubble result
		GameSceneController.instance.showBubbleResult ("Benar, selamat ya!");
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,charTalkGood);
		GameData.soundSourceAnotherGO (GameData.SFX2_SOUNDSOURCE,sfxGame2,hooray);
		GameData.soundVolume (GameData.SFX2_SOUNDSOURCE,sfxGame2,0.3f);

		//got achievement

		#if !UNITY_EDITOR
		GPAchievementController.instance._achievement_answer_question_in_10_sec ();
		GPAchievementController.instance._achievement_answer_question_in_30_sec ();

		GPAchievementController.instance._achievement_clear_world_1 ();
		GPAchievementController.instance._achievement_clear_world_2 ();
		GPAchievementController.instance._achievement_clear_world_3 ();
		#endif
	}
	#endregion
}
