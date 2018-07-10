using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RouletteController : MonoBehaviour {
	public Button btn_spinWatchAd;
	public Image roulette;
	public GameObject spinArrow,spinWatchAd,pointer,rouletteGO;
	public Animator Anim_RouletteStar;
	public Sprite spinWatchAdSprite;

	public static RouletteController instance;

	void Awake(){
		instance = this;
	}

	void Start(){
		_disableSpin ();
		StartCoroutine (checkingInternetConnection ());
	}

	void OnEnable(){
		spinWatchAd.GetComponent<Image>().sprite = spinWatchAdSprite;
	}

	public void _spinWatchAd(){
		//CheckingInternetConnection.instance.checkInternetConnection ();

		if (GameData._isConnected) {
			UnityAdsController.instance.ShowAd ();
			try{
				rouletteGO.GetComponent<RouletteStar> ().enabled = true;
			}catch{
				rouletteGO.GetComponent<Roulette> ().enabled = true;
			}
			roulette.color = new Color (1, 1, 1, 1);
			spinArrow.gameObject.SetActive (true);
			pointer.gameObject.SetActive (true);
			spinWatchAd.gameObject.SetActive (false);
		} else {
			GameData._sceneState = GameData.ROULETTE_MENU;
			GameData._buttonTag = GameData.Tag_Roulette;
			Anim_RouletteStar.SetTrigger (GameData.Hide);
		}
	}

	public void _disableSpin(){
		try{
			rouletteGO.GetComponent<RouletteStar> ().enabled = false;
		}catch{
			rouletteGO.GetComponent<Roulette> ().enabled = false;
		}roulette.color = new Color (0.5f,0.5f,0.5f,1);
		spinArrow.gameObject.SetActive (false);
		pointer.gameObject.SetActive (false);
		spinWatchAd.gameObject.SetActive (true);
	}

	IEnumerator checkingInternetConnection(){
		while(true){
			yield return new WaitForSeconds (0.5f);
			CheckingInternetConnection.instance.checkInternetConnection (); //check internet sekali lagi
		} 
	}
}
