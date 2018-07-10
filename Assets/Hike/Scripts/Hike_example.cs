using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hike_example : MonoBehaviour {

	Text buttonText;
	public string adspot_id;
	public GameObject test_panel;
	public GameObject ads_image;
	public Text ads_title_text;
	public Text ads_content_text;

	void Start()
	{
		buttonText = transform.Find ("Text").GetComponent<Text>();


		//ini untuk memanggil ads, bisa dilakukan dimanapun tanpa harus di void Start
		//parameternya adalah string adspot id yang ingin dipanggil
		if(HikeManager.instance)
			HikeManager.instance.GetAds (adspot_id);
	}

	void Update () {

		if(HikeManager.instance)
		{
			if(HikeManager.instance.adsReady)
			{
				buttonText.text = "ADS READY";
			}
			else
			{
				buttonText.text = "ADS NOT READY";
			}
		}

	}



	public void onKunjungiButtonClick()
	{
		//ketika tombol kunjungi diklik, akan dibuka ke web iklan
		//panggil click dari HikeManager
		HikeManager.instance.clickAds ();
		test_panel.SetActive (false);
	}

	public void onButtonClick()
	{
		//cek apakah ads sudah ready
		if(HikeManager.instance.adsReady)
		{
			//set gambar, judul, dan content diambil dari HikeManager
			ads_image.GetComponent<RawImage> ().texture = HikeManager.instance.getAdsTexture ();
			ads_title_text.text = HikeManager.instance.getAdsTitle ();
			ads_content_text.text = HikeManager.instance.getAdsContent ();
			//setiap kali menampilkan ads, HARUS memanggil showAds() dari HikeManager
			HikeManager.instance.showAds ();
			test_panel.SetActive (true);
		}

	}

	public void onCloseButtonClick()
	{
		//close button diklik
		test_panel.SetActive (false);
	}



}
