using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

public class TambahSoalController : MonoBehaviour {
	public KeyboardTambahSoal keyboardTambahSoal;

	public GameObject transition,popupResult;
	public GameObject loadingScreen;
	public Text fbNameText,sendText;
	public Animator Anim_popupResult;
	public AudioSource sfxMenu, sfxMenu2, sfxMenu3;
	public AudioClip sfxButton;
	private string soalText, jawabanText;


	private void Start (){
	loadingScreen.SetActive(false);
		getFBNameText ();
		ServerTimeManager.self.RequestServerTime();
	}

	private void getFBNameText(){
		fbNameText.text = PlayerPrefs.GetString(GameData.Key_fbname);//GameData._fbName;
	}

	public void OnBtn_Close(string sceneState){
		GameData._sceneState = sceneState;
		GameData._buttonTag = GameData.Tag_Close;

		if(sceneState == GameData.BUATSOAL_MENU){ 
			if (soalText.Length >= 6 && jawabanText.Length >= 6) {
				//GameData._sceneState = GameData.Scene_TambahSoal;
				GameData._onMenuScreen = false;
				PlayerPrefs.SetString (GameData.Key_SceneToGo, GameData.Scene_Menu);
				transition.gameObject.SetActive (true);
			} else {
				Anim_popupResult.SetTrigger (GameData.Hide);
			}
		}
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	public void closeTambahSoal () {
		GameData._sceneState = GameData.Scene_TambahSoal;
		GameData._onMenuScreen = false;
		PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Menu);
		transition.gameObject.SetActive (true);

		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxMenu, sfxButton);
	}

	public void sendingToEmail(){
		bool canSubmit = true;

		//countSubmit();

//		if (GameData.isInternetConnect ()) {
		//erase cursor on soal
		if (keyboardTambahSoal.inputField_soal.text [keyboardTambahSoal.inputField_soal.text.Length - 1].ToString () == "_") {
			string tempString;

			tempString = keyboardTambahSoal.inputField_soal.text.ToString ();
			tempString = tempString.Substring (0, tempString.Length - 1);
			soalText = tempString;
		} else {
			soalText = keyboardTambahSoal.inputField_soal.text.ToString ();
		}

		//erase cursor on jawaban
		if (keyboardTambahSoal.inputField_jawaban.text [keyboardTambahSoal.inputField_jawaban.text.Length - 1].ToString () == "_") {
			string tempString;

			tempString = keyboardTambahSoal.inputField_jawaban.text.ToString ();
			tempString = tempString.Substring (0, tempString.Length - 1);
			jawabanText = tempString;
		} else {
			jawabanText = keyboardTambahSoal.inputField_jawaban.text.ToString ();
		}

//		try{
//			MailMessage mail = new MailMessage ();
//
//			mail.From = new MailAddress ("franki@gamelevelone.com");
//			mail.To.Add ("franki@gamelevelone.com");
//			mail.Subject = "Konten PUNO " + DateTime.Now;
//
//			if(soalText.Length >= 6 && jawabanText.Length >= 6){
//				mail.Body = "Nama Facebook : " + fbNameText.text.ToString () + "\n\nEmail : " + PlayerPrefs.GetString(GameData.Key_fbemail) + "\n\nSoal :\n" + soalText + "\n\nJawaban :\n" + jawabanText;
//
//				SmtpClient smtpServer = new SmtpClient ("smtp.gmail.com");
//
//				smtpServer.Port = 587;
//				smtpServer.Credentials = new NetworkCredential ("franki@gamelevelone.com", "123456frambos") as ICredentialsByHost;
//				smtpServer.EnableSsl = true;
//				ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
//					return true;
//				};
//				smtpServer.Send (mail);
//
//				countSubmit();
//
//				print ("CALL BACK : " + ServicePointManager.ServerCertificateValidationCallback);
//				print ("input field soal : " + soalText);
//				print ("input field jawaban : " + jawabanText);
//				print ("send to email success");
//
//				sendText.alignment = TextAnchor.UpperCenter;
//				GetReward.instance.getReward();
//				_popupResult ("\nSUBMIT SOAL BERHASIL.\nANDA MENDAPATKAN\n" + GameData._rewardName);
//
//				AppsFlyerController.instance._trackRichEvent("submit_soal_button","success_submit_soal","submit_soal_event");
//			}else{
//				_popupResult ("\nSUBMIT SOAL GAGAL.\nSOAL DAN JAWABAN MINIMAL 6 KARAKTER\n");
//			}
//		}catch{
//			_popupResult ("SUBMIT SOAL GAGAL, SILAHKAN CEK KONEKSI INTERNET ANDA");
//			AppsFlyerController.instance._trackRichEvent("submit_soal_button","failed_submit_soal","submit_soal_event");
//		}

		if (soalText.Length >= 6 && jawabanText.Length>=6)
			canSubmit = true;
		else
			canSubmit = false;

		if (canSubmit) {
			string jsonString = "{\"name\":\"" + fbNameText.text.ToString () + "\"," +
			                    "\"fbid\":\"" + PlayerPrefs.GetString (GameData.Key_fbuserid) + "\"," +
			                    "\"question\":\""	+ soalText + "\"," +
			                    "\"answer\":\"" + jawabanText + "\"" +
			                    "}";

			string URL = "http://api.gemugemu.com/apipuno/submitsoal.php?idgame="+GameData.GAMEID;
			print (jsonString);
			sendRequest (URL, jsonString);
		} else {
			_popupResult ("\nSUBMIT SOAL GAGAL.\nSOAL DAN JAWABAN MINIMAL 6 KARAKTER\n");
		}


	}

	void sendRequest (string URL, string jsonString){
		var encoding = new System.Text.UTF8Encoding ();
		WWW www = new WWW (URL,encoding.GetBytes(jsonString));
		StartCoroutine (WaitForRequest (www));
	}

	IEnumerator WaitForRequest (WWW www)
	{
		loadingScreen.SetActive (true);
		yield return www;
		loadingScreen.SetActive (false);

		print (www.text);
		if (www.error == null) {
			countSubmit ();

			int tempCount = PlayerPrefs.GetInt (GameData.Key_totalTambahSoal);

			if (tempCount == 0) {
				sendText.alignment = TextAnchor.UpperCenter;
				GetReward.instance.getReward ();
				_popupResult ("\nSUBMIT SOAL BERHASIL.\nANDA MENDAPATKAN\n" + GameData._rewardName);
			}

			#if !UNITY_EDITOR
			AppsFlyerController.instance._trackRichEvent("submit_soal_button","success_submit_soal","submit_soal_event");
			#endif
		}else{
			_popupResult ("SUBMIT SOAL GAGAL, SILAHKAN CEK KONEKSI INTERNET ANDA");
			#if !UNITY_EDITOR
			AppsFlyerController.instance._trackRichEvent("submit_soal_button","failed_submit_soal","submit_soal_event");
			#endif
		}

		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);
	}

	private void _popupResult(string text){
		GameData._sceneState = GameData.BUATSOAL_MENU;
		GameData._buttonTag = GameData.Tag_Close;
		popupResult.gameObject.SetActive (true);
		sendText.text = text;
	}

	private void countSubmit(){
		int tempTambahSoal;

		tempTambahSoal = PlayerPrefs.GetInt (GameData.Key_totalTambahSoal);
		print ("tempTambahSoal : " + tempTambahSoal);
		tempTambahSoal -= 1; 
		PlayerPrefs.SetInt (GameData.Key_totalTambahSoal,tempTambahSoal);
		print ("tempTambahSoal new : " + PlayerPrefs.GetInt (GameData.Key_totalTambahSoal));

		DateTime tempToday = new DateTime(GameData.ServerTime.Year,GameData.ServerTime.Month,GameData.ServerTime.Day,0,0,0);
		PlayerPrefs.SetString(GameData.key_lastTimePlay,(tempToday).ToString()); //save last time using tambah soal

		print("Waktu Simpan : " + PlayerPrefs.GetString(GameData.key_lastTimePlay));
		PlayerPrefs.Save ();
	}
}
