using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Globalization;

public class BackgroundImgController : MonoBehaviour {
	public CloudAnimController CloudAnimController;
	public static BackgroundImgController instance;

	#region JakartaAsset
	public Image IMG_Bg,IMG_Fg,IMG_Semak,IMG_Langit,IMG_Awan_bg,IMG_Siluet,IMG_Awan1,IMG_Awan2,IMG_Awan3,IMG_Matahari;
	public Sprite awanPagi_gelap,awanPagi_terang,awanSiang_gelap,awanSiang_terang,awanMalam_gelap,awanMalam_terang,siluet_gedung,
	jkt_bg,jkt_fg,bali_bg,bali_fg,padang_bg,padang_fg,toraja_bg,toraja_fg,jogja_bg,jogja_fg,
	siluet_gunung_pagi,siluet_gunung_siang,siluet_gunung_malam;
	private Color bgColor,langitPagiColor,langitSiangColor,langitMalamColor;
	#endregion

	private DateTime testTime;

	public GameObject Matahari, Bulan, Bintang;
	public GameObject bgNorm,fgNorm,bgXmas,fgXmas;

	private bool setBG = false;

	void Awake(){
		instance = this;
	}

	void Start(){
		bgColor = new Color (0.8f,0.8f,0.8f,1);
		langitPagiColor = new Color (0.53f,0.82f,0.99f);
		langitSiangColor = new Color (0.51f,0.75f,1);
		langitMalamColor = new Color (0.13f,0.23f,0.34f,255);
		CloudAnimController.PlayCloudAnim();
		//setBackgroundDefault ();

		SetAndAnimateBackground(DateTime.Now);
	}

//	void Update ()
//	{
//		setBackgroundImage ();
//	}

	public void setSeasonalBGFG (){
		bgXmas.SetActive(false);
		fgXmas.SetActive(false);
		bgNorm.SetActive(true);
		fgNorm.SetActive(true);
	}

	public void SetAndAnimateBackground(DateTime time){
		if(time >= new DateTime(time.Year, time.Month, time.Day,5,0,0) && time < new DateTime(time.Year, time.Month, time.Day,10,59,59)){ //time: 5.00 to 10.59
			//TEMA PAGI
			IMG_Awan1.sprite = awanPagi_gelap;
			IMG_Awan2.sprite = awanPagi_terang;
			IMG_Awan3.sprite = awanPagi_terang;
			IMG_Langit.color = langitPagiColor;
			Matahari.SetActive(true);
			IMG_Siluet.color = new Color (1,1,1,1);
		}
		else if(time >= new DateTime(time.Year, time.Month, time.Day, 11,0,0) && time < new DateTime(time.Year, time.Month, time.Day, 16,59,0)){ //time: 11.00 to 16.59
			//TEMA SIANG
			IMG_Awan1.sprite = awanPagi_gelap;
			IMG_Awan2.sprite = awanPagi_terang;
			IMG_Awan3.sprite = awanPagi_terang;
			IMG_Langit.color = langitSiangColor;
			Matahari.SetActive(true);
			IMG_Siluet.color = new Color (0.86f,0.86f,0.86f,1);
		}else{//YA SISANYA BRO
			//TEMA MALAM
			IMG_Awan1.sprite = awanMalam_gelap;
			IMG_Awan2.sprite = awanMalam_terang;
			IMG_Awan3.sprite = awanMalam_terang;
			IMG_Langit.color = langitMalamColor;
			IMG_Fg.color = bgColor;
			IMG_Bg.color = bgColor;
			IMG_Semak.color = bgColor;
			IMG_Siluet.color = new Color (0.2f,0.2f,0.2f,1);
			IMG_Awan_bg.color = new Color(1,1,1,0.3f);
			Matahari.SetActive(false);
			Bintang.SetActive (true);
			//Bulan.SetActive(true);
		}

		CloudAnimController.PlayCloudAnim();
	}

	/// <summary>
	/// Sets the background image.
	/// </summary>
	public void setBackgroundImage ()
	{
		int world = PlayerPrefs.GetInt (GameData.Key_Background);
		print("WORLD = "+world);
		if (world == 0 || world == 1 || world == 2) {
			IMG_Bg.sprite = jkt_bg;
			IMG_Fg.sprite = jkt_fg;
			IMG_Siluet.sprite = siluet_gedung;
		} else if (world == 3 || world == 4 || world == 5) {
			IMG_Bg.sprite = bali_bg;
			IMG_Fg.sprite = bali_fg;
			IMG_Siluet.sprite = siluet_gunung_pagi;
		} else if (world == 6 || world == 7 || world == 8) {
			IMG_Bg.sprite = padang_bg;
			IMG_Fg.sprite = padang_fg;
			IMG_Siluet.sprite = siluet_gunung_pagi;
		} else if (world == 9 || world == 10 || world == 11) {
			IMG_Bg.sprite = toraja_bg;
			IMG_Fg.sprite = toraja_fg;
			IMG_Siluet.sprite = siluet_gunung_pagi;
		} else if (world == 12 || world == 13 || world == 14) {
			IMG_Bg.sprite = jogja_bg;
			IMG_Fg.sprite = jogja_fg;
			IMG_Siluet.sprite = siluet_gunung_pagi;
		}

	}

	public void setBackgroundDefault(){
		int isInGame;

		isInGame = PlayerPrefs.GetInt (GameData.Key_setDefaultBg);
		if(isInGame == 0){
			PlayerPrefs.SetInt (GameData.Key_Background,0); //reset background to Jakarta BG
		}
	}
}
