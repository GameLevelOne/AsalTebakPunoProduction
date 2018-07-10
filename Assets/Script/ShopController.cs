using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {
	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private int buyItemList = 0;

	private int tempStarCurrency;
	public GameObject buyNotification,iconGroup1,iconGroup2;
	public Text text_buyNotification,text_totalStar,pak1Price,pak2Price,pak3Price,pak4Price,pak5Price,thxTxt;
	public AudioSource sfxMenu;
	public AudioClip sfxButton;

	public Sprite[] iconSprites = new Sprite[7]; // ext1, ext2, show1, show2, freeze1, freeze2, reset
	public Image targetIcon;
	public Image targetIconPaket1;
	public Image targetIconPaket2;

	public static ShopController instance;

	void Awake(){
		instance = this;
	}

	void Start ()
	{
		updateTotalStar ();
		m_StoreController = UnityIAPController.m_StoreController;
		if (SceneManager.GetActiveScene ().name == "MenuScene") {
			setLocalizedPrice ();
		}
	}

	#region public
	public void updateTotalStar(){
		text_totalStar.text = "= " + PlayerPrefs.GetInt(GameData.Key_starCurrency).ToString();
	}

	#endregion

	private void setLocalizedPrice(){
		pak1Price.text = "IDR\n"+m_StoreController.products.WithID (GameData.sku_paket1).metadata.localizedPrice.ToString ();
		pak2Price.text = "IDR\n"+m_StoreController.products.WithID (GameData.sku_paket2).metadata.localizedPrice.ToString ();
		pak3Price.text = "IDR\n"+m_StoreController.products.WithID (GameData.sku_paket3).metadata.localizedPrice.ToString ();
		pak4Price.text = "IDR\n"+m_StoreController.products.WithID (GameData.sku_paket4).metadata.localizedPrice.ToString ();
		pak5Price.text = "IDR\n"+m_StoreController.products.WithID (GameData.sku_paket5).metadata.localizedPrice.ToString ();
	}

	#region private
	private void buyProcess(int starCost, int powerUpCode){
		iconGroup1.SetActive (false);
		iconGroup2.SetActive (true);
		targetIcon.sprite = iconSprites [powerUpCode - 1];
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxMenu,sfxButton);

		tempStarCurrency = PlayerPrefs.GetInt (GameData.Key_starCurrency);

		if (tempStarCurrency >= starCost) {
			tempStarCurrency -= starCost;
			GameData.gotOnePowerUp (powerUpCode, 1);
			PlayerPrefs.SetInt (GameData.Key_starCurrency, tempStarCurrency);

			updateTotalStar ();

			try{
				GameObject.Find("TotalScore").GetComponent<WorldController>().totalStarAllWorldText.text = "= " + PlayerPrefs.GetInt(GameData.Key_starCurrency).ToString();
			}catch{
				print ("it's okay");
			}
			thxTxt.gameObject.SetActive (true);
			showBuyNotif ("Anda telah membeli " + GameData.getPowerUpName (powerUpCode-1));
		} else {
			thxTxt.gameObject.SetActive (false);
			showBuyNotif ("Bintang tidak cukup untuk membeli " + GameData.getPowerUpName (powerUpCode-1));

			AppsFlyerController.instance._trackRichEvent ("buy_powerup_withstar", "star_is_not_enough_to_buy", "buy_powerup_event");
		}
	}

	public void setItemIcon(int code1,int code2){
		thxTxt.gameObject.SetActive (true);
		iconGroup1.SetActive (true);
		iconGroup2.SetActive (false);
		targetIconPaket1.sprite = iconSprites [code1 - 1];
		targetIconPaket2.sprite = iconSprites [code2 - 1];
	}

	public void showBuyNotif(string message){
		buyNotification.gameObject.SetActive (true);
		text_buyNotification.text = message;
	}
	#endregion

	#region ingame currency
	public void buyShow1Word(){
		buyProcess (1,3);
		AppsFlyerController.instance._trackRichEvent ("buy_powerup_withstar", "buy_powerup_show1word", "buy_powerup_event");
	}

	public void buyShow2Words(){
		buyProcess (2,4);
		AppsFlyerController.instance._trackRichEvent ("buy_powerup_withstar", "buy_powerup_show2words", "buy_powerup_event");
	}

	public void buyExtendedTime1(){
		buyProcess (1,1);
		AppsFlyerController.instance._trackRichEvent ("buy_powerup_withstar", "buy_powerup_extTime5s", "buy_powerup_event");
	}

	public void buyExtendedTime2(){
		buyProcess (2,2);
		AppsFlyerController.instance._trackRichEvent ("buy_powerup_withstar", "buy_powerup_extTime10s", "buy_powerup_event");
	}

	public void buyStopTime1(){
		buyProcess (1,5);
		AppsFlyerController.instance._trackRichEvent ("buy_powerup_withstar", "buy_powerup_stopTime5s", "buy_powerup_event");
	}

	public void buyStopTime2(){
		buyProcess (2,6);
		AppsFlyerController.instance._trackRichEvent ("buy_powerup_withstar", "buy_powerup_stopTime10s", "buy_powerup_event");
	}

	public void buyResetStar (){
		buyProcess(5,7);
		AppsFlyerController.instance._trackRichEvent("buy_powerup_withstar","buy_powerup_resetStar","buy_powerup_event");
	}
	#endregion
}
