using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldController : MonoBehaviour {
	private int totalStar;
	public string onCounting;
	public int world;
	public Text totalStarWorldText,totalStarAllWorldText;
	public GameObject lockedWorldIcon;

	public static WorldController instance;

	void Awake(){
		instance = this;
	}

	void Start () {
	}

	void OnEnable (){
		loadStar ();
		unlockWorld ();
	}

	public void loadStar ()
	{
		GameData.Load ();

		if (onCounting == "everyWorld") {
			totalStar = GameData.totalStarWorld (world);
			totalStarWorldText.text = totalStar.ToString () + "/" + GameData.TotalStagePerWorld;
		} else if (onCounting == "allWorld") {
//			totalStar = GameData.totalStarAllWorld ();
//			totalStarAllWorldText.text = "= " + totalStar.ToString();

			totalStarAllWorldText.text = "= " + PlayerPrefs.GetInt (GameData.Key_starCurrency).ToString ();
		}
	}

	private void unlockWorld(){
		int unlockWorld;

		unlockWorld = PlayerPrefs.GetInt (GameData.Key_UnlockedWorld + world);

		if(world == unlockWorld){
			if(lockedWorldIcon != null){ //exception
				lockedWorldIcon.gameObject.SetActive (false);
			}
		}
	}
}
