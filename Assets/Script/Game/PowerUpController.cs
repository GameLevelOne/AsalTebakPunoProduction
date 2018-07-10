using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpController : MonoBehaviour {	
	public Image ext1, ext2, showWord1, showWord2, stopTime1, stopTime2, barTime;
	public Sprite freezeBar,extendedBar,normalBar;
	public Animator Anim_Shop,Anim_Hint;
	public Text ext1Text, ext2Text, show1Text, show2Text, stop1Text, stop2Text;
	public AudioSource sfxGame;
	public AudioClip sfxButton;
	private char[] tempAnswer;
	private int index,timerPowerUp,whichRow,indexMinInRow,indexMaxInRow,indexLeft,indexRight,randomIndex,prizeCode;
	private bool usedExt1,usedExt2,usedShowWord1,usedShowWord2,usedStopTime1,usedStopTime2;
	//usedShowLetter

	public static PowerUpController instance;
	public StarController timer;

	void Awake(){
		instance = this;
	}

	void Start(){
		usedExt1 = false;
		usedExt2 = false;
		usedShowWord1 = false;
		usedShowWord2 = false;
		usedStopTime1 = false;
		usedStopTime2 = false;

		_PowerUpAmountText ();
	}

	#region powerUpButton
	public void extTime1_btn(){
		if(PlayerPrefs.GetInt(GameData.Key_PowerUp + 1.ToString()) > 0){
			extendedTime1 ();
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
	}

	public void extTime2_btn(){
		if(PlayerPrefs.GetInt(GameData.Key_PowerUp + 2.ToString()) > 0){
			extendedTime2 ();
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
	}

	public void showOneWord_btn(){
		if(PlayerPrefs.GetInt(GameData.Key_PowerUp + 3.ToString()) > 0){
			showOneWord ();
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
	}

	public void showTwoWord_btn(){
		if(PlayerPrefs.GetInt(GameData.Key_PowerUp + 4.ToString()) > 0){
			showTwoWord ();
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
	}

	public void stopTime1_btn(){
		if(PlayerPrefs.GetInt(GameData.Key_PowerUp + 5.ToString()) > 0){
			_stopTime1 ();
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
	}

	public void stopTime2_btn(){
		if(PlayerPrefs.GetInt(GameData.Key_PowerUp + 6.ToString()) > 0){
			_stopTime2 ();
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxButton);
	}
	#endregion

	#region powerUpMehtod
	public void extendedTime1(){
		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if(usedExt1 == false){
			extendedTime (5);
			if(prizeCode == 0){ //if not using roulette
				usedExt1 = updateButton(ext1Text,ext1,usedExt1,1);
			}
			GameData.updateUsingPowerUp ();
		}
	}

	public void extendedTime2(){
		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if (usedExt2 == false) {
			extendedTime (10);
			if (prizeCode == 0) { //if not using roulette
				usedExt2 = updateButton(ext2Text,ext2,usedExt2,2);
			}
			GameData.updateUsingPowerUp ();
		}
	}

//	public void showLetter(){
//		if (usedShowLetter == false) {
//			int checkLetter = 10;//cek huruf sebanyak 10x
//
//			while (checkLetter > 0) {
//				index = Random.Range (0, AnswerGenerator.instance.ANSWER.Length);
//				if (AnswerGenerator.instance.AnswerChars [index].GetComponent<Text> ().text == "_") {
//					AnswerGenerator.instance.AnswerChars [index].GetComponent<Text> ().text = AnswerGenerator.instance.ANSWER [index].ToString ();
//					checkLetter--;
//				}
//			}
//			usedShowLetter = true;
//		}
//	}

	public void showOneWord(){
		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if(usedShowWord1 == false){
			checkLetters ();
			afterUsingPowerUp (1);

			if (prizeCode == 0) { //if not using roulette
				usedShowWord1 = updateButton(show1Text,showWord1,usedShowWord1,3);
			}
			GameData.updateUsingPowerUp ();
		}
	}

	public void showTwoWord(){
		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if(usedShowWord2 == false){
			//first word
			checkLetters ();

			//second word
			checkLetters ();

			afterUsingPowerUp (2);

			if (prizeCode == 0) { //if not using roulette
				usedShowWord2 = updateButton(show2Text,showWord2,usedShowWord2,4);
			}
			GameData.updateUsingPowerUp ();
		}
	}

	public void _stopTime1(){
		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if(usedStopTime1 == false){
			stopTime ("timeIsStop1");
			if (prizeCode == 0) { //if not using roulette
				usedStopTime1 = updateButton(stop1Text,stopTime1,usedStopTime1,5);
			}
			GameData.updateUsingPowerUp ();
		}
	}

	public void _usingStopTime1(){
		StartCoroutine ("timeIsStop1");
	}

	public void _stopTime2(){
		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if(usedStopTime2 == false){
			stopTime ("timeIsStop2");
			if (prizeCode == 0) { //if not using roulette
				usedStopTime2 = updateButton(stop2Text,stopTime2,usedStopTime2,6);
			}
			GameData.updateUsingPowerUp ();
		}
	}

	public void _usingStopTime2(){
		StartCoroutine ("timeIsStop2");
	}
	#endregion

	#region privateFunction
	private void stopTime(string numeratorName){
		StarController.instance.isTiming = false;
		barTime.sprite = freezeBar;
		StartCoroutine(numeratorName);
		timer.enabled = true;
		print ("TEST USING POWER UP 2");
	}

	private void extendedTime(int extTime){
		//cek sisa kelebihan max waktu
		if (Mathf.FloorToInt (StarController.maxTime - StarController.timeLimit) <= extTime) {
			timerPowerUp = Mathf.FloorToInt (StarController.maxTime - StarController.timeLimit);
		} else {
			timerPowerUp = extTime;
		}

		barTime.sprite = extendedBar;
		StartCoroutine ("timerBarNormal");

		StarController.timeLimit += extTime;
		timer.enabled = true;
		print ("TEST USING POWER UP");
	}

	private bool updateButton(Text textObject,Image powerUpBtn,bool usingPowerUp,int indexPowerUp){
		textObject.text = _updatePowerUpAmount(textObject,indexPowerUp);
		powerUpBtn.color = new Color (0.5f,0.5f,0.5f,1);
		usingPowerUp = true;
		_hideHintMenu ();

		return usingPowerUp;
	}

	private void afterUsingPowerUp(int decreaseLetterChecked){
		if(GameData._letterChecked < GameData._totalLetters){
			GameData._letterChecked -= decreaseLetterChecked;
		}
		GameData.maxCountLetter ();
		CursorController.instance.ShowHideSubmitButton ();
		timer.enabled = true;
		print ("TEST USING POWER UP 1");
	}

	private void checkLetters(){
		int emptyIndex = 0;

		for(int index=0;index<AnswerGenerator.instance.AnswerChars.Length;index++){
			if(AnswerGenerator.instance.AnswerChars[index].GetComponent<Text>().text == "_"){
				emptyIndex++;
			}
		}

		print ("emptyIndex : " + emptyIndex);
		print ("totalLetters : " + GameData._totalLetters);

		if(emptyIndex > 0 && emptyIndex <= GameData._totalLetters){
			checkIndex ();
			try{
				showWord();
			}catch{
				Debug.Log ("it's okay");
			}
		}else if(emptyIndex == 0){
			//do nothing
		}
	}

	private void checkIndex(){
		do {
			randomIndex = Random.Range (0, AnswerGenerator.instance.ANSWER.Length);
		} while(AnswerGenerator.instance.AnswerChars[randomIndex].GetComponent<Text>().text == " " || AnswerGenerator.instance.AnswerChars[randomIndex].GetComponent<Text>().text != "_");

		print ("randomIndex : " + randomIndex);

		//index awal yang akan dibuka hurufnya
		indexLeft = randomIndex;
		indexRight = randomIndex;

		//random index berada di row ke berapa
		whichRow = (int) (Mathf.Ceil(randomIndex / AnswerGenerator.instance.charLimitRow)) + 1;
		print ("whichRow : " + whichRow);
		//index terakhir pada row tersebut (whichRow)
		indexMaxInRow = whichRow * AnswerGenerator.instance.charLimitRow;
		print ("indexMaxInRow : " + indexMaxInRow);
		//index awal pada row tersebut (whichRow)
		indexMinInRow = indexMaxInRow - AnswerGenerator.instance.charLimitRow;
		print ("indexMinInRow : " + indexMinInRow);
	}

	private void showWord(){
		do{
			if(indexLeft >= 0){
				AnswerGenerator.instance.AnswerChars[indexLeft].GetComponent<Text>().text = AnswerGenerator.instance.ANSWER[indexLeft].ToString();
				indexLeft--;

				if(indexLeft < 0){
					indexLeft = 0;
				}

				GameData._letterChecked += 1;
			}
		}while(AnswerGenerator.instance.AnswerChars[indexLeft].GetComponent<Text>().text == "_" && indexLeft != (indexMinInRow-1));

		do{
			if(indexRight <= AnswerGenerator.instance.ANSWER.Length-1){
				AnswerGenerator.instance.AnswerChars[indexRight].GetComponent<Text>().text = AnswerGenerator.instance.ANSWER[indexRight].ToString();
				indexRight++;

				if(indexRight > AnswerGenerator.instance.AnswerChars.Length){
					indexRight = AnswerGenerator.instance.AnswerChars.Length;
				}

				GameData._letterChecked += 1;
			}
		}while(AnswerGenerator.instance.AnswerChars[indexRight].GetComponent<Text>().text == "_" && indexRight != indexMaxInRow);
	}

	private void _hideHintMenu(){
		GameData._sceneState = GameData.HINT_MENU;
		GameData._buttonTag = GameData.Tag_Used;
		Anim_Hint.SetTrigger (GameData.Hide);
	}

	private void _PowerUpAmountText(){
		ext1Text.text = GameData._getPowerUpAmount (ext1Text,1);
		ext2Text.text = GameData._getPowerUpAmount (ext2Text,2);
		show1Text.text = GameData._getPowerUpAmount (show1Text,3);
		show2Text.text = GameData._getPowerUpAmount (show2Text,4);
		stop1Text.text = GameData._getPowerUpAmount (stop1Text,5);
		stop2Text.text = GameData._getPowerUpAmount (stop2Text,6);
	}

	private string _updatePowerUpAmount(Text amountText,int indexPowerUp){
		string powerUpPrefKey;
		int powerUpAmount;

		powerUpPrefKey = GameData.Key_PowerUp + indexPowerUp.ToString();
		powerUpAmount = PlayerPrefs.GetInt (powerUpPrefKey);
		powerUpAmount--;
		PlayerPrefs.SetInt (powerUpPrefKey,powerUpAmount);
		amountText.text = powerUpAmount.ToString();

		return amountText.text;
	}
	#endregion

	#region numerator
	public IEnumerator timerBarNormal(){
		yield return new WaitForSeconds (timerPowerUp);
		barTime.sprite = normalBar;
		StopCoroutine ("timerBarNormal");
	}

	public IEnumerator timeIsStop1(){
		yield return new WaitForSeconds (5);
		StarController.instance.isTiming = true;
		GameSceneController._usedStopTime1 = false;
		barTime.sprite = normalBar;
		StopCoroutine ("timeIsStop1");
	}

	public IEnumerator timeIsStop2(){
		yield return new WaitForSeconds (10);
		StarController.instance.isTiming = true;
		GameSceneController._usedStopTime2 = false;
		barTime.sprite = normalBar;
		StopCoroutine ("timeIsStop2");
	}
	#endregion
}
