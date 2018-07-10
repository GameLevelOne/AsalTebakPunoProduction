using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GetPrizeController : MonoBehaviour {
	private static int prizeCode;
	public Sprite ext1, ext2, show1Word, show2Words, stopTime1, stopTime2;
	public Image prizeImg;
	public Text prizeText,text_totalStar;

	void Update(){
		if (GameData._onMenuScene == GameData.GAME_MENU) {
			getPrizeResult ();
		} else if (GameData._onMenuScene == GameData.WORLD_MENU) {
			getStarResult ();
		}
	}

	public void getPrizeResult(){
		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if(prizeCode == 1){
			prizeImg.sprite = ext1;
			prizeText.text = "anda mendapatkan\n" + GameData.pu_ext1;
		}else if(prizeCode == 2){
			prizeImg.sprite = ext2;
			prizeText.text = "anda mendapatkan\n" + GameData.pu_ext2;
		}else if(prizeCode == 3){
			prizeImg.sprite = show1Word;
			prizeText.text = "anda mendapatkan\n" + GameData.pu_show1word;
		}else if(prizeCode == 4){
			prizeImg.sprite = show2Words;
			prizeText.text = "anda mendapatkan\n" + GameData.pu_show2words;
		}else if(prizeCode == 5){
			prizeImg.sprite = stopTime1;
			prizeText.text = "anda mendapatkan\n" + GameData.pu_stopTime1;
		}else if(prizeCode == 6){
			prizeImg.sprite = stopTime2;
			prizeText.text = "anda mendapatkan\n" + GameData.pu_stopTime2;
		}
	}

	public void getStarResult ()
	{
		string saveTime;

		prizeCode = PlayerPrefs.GetInt (GameData.Key_prizeCode);

		if (prizeCode == 1) {
			updateStarCurrency (3);
			prizeText.text = "anda mendapatkan 3 star";
		} else if (prizeCode == 2 || prizeCode == 5) {
			updateStarCurrency (2);
			prizeText.text = "anda mendapatkan 2 star";
		} else if (prizeCode == 3 || prizeCode == 6) {
			updateStarCurrency (1);
			prizeText.text = "anda mendapatkan 1 star";
		} else if (prizeCode == 4) {
			updateStarCurrency (4);
			prizeText.text = "anda mendapatkan 4 star";
		}

		prizeCode = 0;
		PlayerPrefs.SetInt (GameData.Key_prizeCode, prizeCode);

		text_totalStar.text = "= " + PlayerPrefs.GetInt (GameData.Key_starCurrency).ToString ();

		saveTime = (DateTime.Now).ToString();
		PlayerPrefs.SetString (GameData.Key_lastRouletteStar,saveTime);
		GameData._NotificationTimer ();
		GameData.rouletteStarReady = false;
	}

	private void updateStarCurrency (int getStar)
	{
		int tempStar;

		tempStar = PlayerPrefs.GetInt (GameData.Key_starCurrency);
		print ("tempStar : " + tempStar);
		PlayerPrefs.SetInt (GameData.Key_starCurrency, (tempStar + getStar));
		print ("starNow : " + PlayerPrefs.GetInt (GameData.Key_starCurrency));
		
	}
}
