using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;
using UnityEngine.UI;

public class HikeBanner : MonoBehaviour {

	public static HikeBanner instance;
	public string banner_adspot_id;
	public bool dontDestroy=true;
	public bool autoRefresh = true;
	public int autoRefreshInSeconds=60;

	public GameObject banner_ads_image;
	public Text banner_ads_text;


	private string ad_type = "&ad_type=5";
	private string audience_id_type = "&audience_id_type=3";

	private string os = "&os=android"; //GANTI jadi &os=ios  untuk target device ios
	private int number = 50;
	private string numberString = "&number=";

	private int totalAdsCount=0;
	private int currentAdsNumber;

	Texture[] adsIcon;
	Texture[] adsImage;
	string[] adsIconUrl;
	string[] adsImageUrl;
	string[] adsName;
	string[] adsDescription;
	string[] adsImpressionUrl;
	string[] adsClickUrl;
	int[] adsPosition;


	void Awake () {

		instance = this;
		numberString = numberString + number;
		if(dontDestroy)
		{
			DontDestroyOnLoad (this.gameObject);
		}
		else
			currentAdsNumber = 0;

		if (autoRefreshInSeconds < 30)
			autoRefreshInSeconds = 30;
		
		GetAds ();

	}

	public void hideAds()
	{
		gameObject.SetActive (false);
	}

	public void unhideAds()
	{
		gameObject.SetActive (true);
	}

	void GetAds()
	{
		print ("start get ads");
		StartCoroutine (getAds());
	}

	IEnumerator getAds()
	{
		
		string adsUrl = "https://ad.mtburn.com/ad?adspot_id="+
			banner_adspot_id + ad_type + audience_id_type + os;
		print ("adsurl : " + adsUrl);

		string result="";

		WWW myWWW = new WWW (adsUrl);
		yield return myWWW;
		print ("result : " +myWWW.text.ToString ());
		if(myWWW.error!=null)
		{
			print (myWWW.error.ToString ());
			GetAds ();
		}
		else
		{
			result = myWWW.text.ToString ();

			string arrayString = result.Remove (0, 9);
			arrayString = arrayString.Remove (arrayString.Length-1);

			print ("result : " + arrayString);
			JSONArray jArray =  JSONArray.Parse ( arrayString);

			if(jArray!=null)
			{
				int length = jArray.Length;
				totalAdsCount = length;
				adsIconUrl = new string[length];
				adsImageUrl = new string[length];
				adsName = new string[length];
				adsDescription = new string[length];
				adsImpressionUrl = new string[length];
				adsClickUrl = new string[length];
				adsIcon = new Texture[length];
				adsImage = new Texture[length];
				adsPosition = new int[length];

				for(int i =0 ; i < length ; i++)
				{
					JSONObject jObject = JSONObject.Parse (jArray [i].ToString ());
					adsIconUrl[i] = jObject.GetString ("icon_creative_url");
					adsImageUrl [i] = jObject.GetString ("creative_url");
					adsName[i] = jObject.GetString ("title");
					adsDescription[i] = jObject.GetString ("description");
					adsImpressionUrl[i] = jObject.GetString ("imp_url");
					adsClickUrl[i] = jObject.GetString ("click_url");
					adsPosition[i] = int.Parse (jObject.GetString ("position"));
					//print (adsName[i] + "\n");
				}
			}
		}


		StartCoroutine(GetIcon());
	}

	IEnumerator GetIcon()
	{
		for(int i = 0; i < adsIconUrl.Length; i++)
		{
			WWW www = new WWW(adsIconUrl[i]);
			yield return www;
			if(www.texture != null)
			{
				adsIcon[i] = (Texture)www.texture;
			}
		}

		getImage ();
	}

	void getImage()
	{
		StartCoroutine (GetImage ());
	}

	IEnumerator GetImage()
	{
		for(int i = 0; i < adsImageUrl.Length; i++)
		{
			WWW www = new WWW(adsImageUrl[i]);
			yield return www;
			if(www.texture != null)
			{
				adsImage[i] = (Texture)www.texture;
			}
		}

		//call the impression after ready
		impressionAds ();
	}


	public void impressionAds()
	{
		//untuk gunakan image icon
		banner_ads_image.GetComponent<RawImage>().texture = adsIcon [currentAdsNumber];
		//untuk gunakan image iklan
		//banner_ads_image.GetComponent<RawImage>().texture = adsImage [currentAdsNumber];

		banner_ads_text.text = adsName [currentAdsNumber];
		string UUID = GetUniqueIdentifier().ToUpper();
		string url = adsImpressionUrl[currentAdsNumber]+ UUID + "&session_id=" +  UUID;
		StartCoroutine (sendAdsImpression (url));
		currentAdsNumber++;
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;
		if(autoRefresh)
		{
			Invoke ("impressionAds", autoRefreshInSeconds);
		}
	}

	IEnumerator sendAdsImpression(string url)
	{
		WWW myWWW = new WWW(url);
		yield return myWWW;
	}
		

	public void clickAds()
	{
		string UUID = GetUniqueIdentifier().ToUpper();
		string url = adsClickUrl[currentAdsNumber] + UUID + "&session_id=" +  UUID;

		StartCoroutine (sendClickAds(url));
		Application.OpenURL (url);

		currentAdsNumber++;
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;
		impressionAds ();
	}

	IEnumerator sendClickAds(string url)
	{
		WWW myWWW = new WWW(url);
		yield return myWWW;
	}

	public static string GetUniqueIdentifier()
	{
		System.Guid uid = System.Guid.NewGuid();
		string str = EncodeTo64(uid.ToString());
	
		return str;
	}

	public static string EncodeTo64(string toEncode)
	{
		byte[] toEncodeAsBytes
		= System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
		string returnValue
		= System.Convert.ToBase64String(toEncodeAsBytes);
		return returnValue;
	}


}
