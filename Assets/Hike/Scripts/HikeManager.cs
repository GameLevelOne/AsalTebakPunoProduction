using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Boomlagoon.JSON;

public class HikeManager : MonoBehaviour {

	public static HikeManager instance;

	public string adspot_id;

	private string ad_type = "&ad_type=5";
	private string audience_id_type = "&audience_id_type=3";

	private string os = "&os=android"; //GANTI jadi &os=ios  untuk target device ios
	private int number = 50;
	private string numberString = "&number=";

	private int totalAdsCount=0;
	private int currentAdsNumber;
	public bool adsReady=false;

	Texture[] adsIcon;
	Texture[] adsImage;
	string[] adsIconUrl;
	string[] adsName;
	string[] adsDescription;
	string[] adsImpressionUrl;
	string[] adsClickUrl;
	string[] adsImageUrl;
	string[] adsAdvertiser;
	int[] adsPosition;

	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
			numberString = numberString + number;
			DontDestroyOnLoad (this.gameObject);
			//GetAds (adspot_id);

		} else
			Destroy (gameObject);

		#if UNITY_IOS
		os = "&os=ios";
		#else
		os = "&os=android";
		#endif

	}

	public void GetAds(string ads)
	{
		adspot_id = ads;
		adsReady = false;
		print ("start get ads");
		StartCoroutine (getAds());
	}

	IEnumerator getAds()
	{

		string adsUrl = "https://ad.mtburn.com/ad?adspot_id="+
			adspot_id + ad_type + audience_id_type + os;
		print ("adsurl : " + adsUrl);

		string result="";

		WWW myWWW = new WWW (adsUrl);
		yield return myWWW;
		print ("result : " +myWWW.text.ToString ());
		if(myWWW.error!=null)
		{
			print (myWWW.error.ToString ());
			GetAds (adspot_id);
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
				adsAdvertiser = new string[length];
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
					adsAdvertiser[i] = jObject.GetString("displayed_advertiser");
					adsName[i] = jObject.GetString ("title");
					adsDescription[i] = jObject.GetString ("description");
					adsImpressionUrl[i] = jObject.GetString ("imp_url");
					adsClickUrl[i] = jObject.GetString ("click_url");
					adsPosition[i] = int.Parse (jObject.GetString ("position"));
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
		//call the impression after ready
		//impressionAds ();
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

		adsReady = true;
		//call the impression after ready
		//impressionAds ();
	}

	public Texture getAdsTexture()
	{
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;

		//untuk image icon
		return adsIcon [currentAdsNumber];

		//untuk image 
		//return adsImage[currentAdsNumber];
	}

	public Sprite getAdsSprite()
	{
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;

		//untuk sprite icon
		Texture2D texture = (Texture2D)adsIcon [currentAdsNumber];

		//untuk sprite image
		//Texture2D texture = (Texture2D)adsImage [currentAdsNumber];

		Rect rec = new Rect(0, 0, texture.width, texture.height);
		Sprite spr = Sprite.Create(texture,rec,new Vector2(0.5f,0.5f),100);

		return spr;
	}

	public string getAdsAdvertiser()
	{
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;
		return adsAdvertiser [currentAdsNumber];
	}

	public string getAdsTitle()
	{
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;
		return adsName [currentAdsNumber];
	}

	public string getAdsContent()
	{
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;
		return adsDescription [currentAdsNumber];
	}

	public void showAds()
	{
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;
		string UUID = GetUniqueIdentifier().ToUpper();
		string url = adsImpressionUrl[currentAdsNumber]+ UUID + "&session_id=" +  UUID;
		StartCoroutine (sendAdsImpression (url));
		Debug.Log(url);
		currentAdsNumber++;
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;
		
	}

	IEnumerator sendAdsImpression(string url)
	{
		WWW myWWW = new WWW(url);
		yield return myWWW;
	}


	public void clickAds()
	{
		if (currentAdsNumber >= totalAdsCount)
			currentAdsNumber = 0;
		string UUID = GetUniqueIdentifier().ToUpper();
		string url = adsClickUrl[currentAdsNumber] + UUID + "&session_id=" +  UUID;

		StartCoroutine (sendClickAds(url));
		Application.OpenURL (url);
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
