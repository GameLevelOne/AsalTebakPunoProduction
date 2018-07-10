using UnityEngine;
using System.Collections;

public class StageStarController : MonoBehaviour {
	public int Stage;
	public GameObject star1, star2, star3;
	private int star;
	private bool checkStar=false;
	public static StageStarController instance;

	void Awake(){
		instance=this;
	}

	void Update ()
	{
		getStar();
	}

	public void getStar ()
	{
		string StarDataPrefKey = "";

		if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
			StarDataPrefKey = "StarWorld" + GameData._worldNumber.ToString () + "Stage" + Stage;
			star = PlayerPrefs.GetInt (StarDataPrefKey);
		} else if (GameData.GilaMode.iGilaMode == 1) {
			star = GameData.GilaMode.GetStarValue (GameData._worldNumber, Stage);
		} else if (GameData.EnglishMode.iEnglishMode == 1) {
			star = GameData.EnglishMode.GetStarValue(GameData._worldNumber,Stage);
		}

		if (star == 1) {
			star1.gameObject.SetActive (true);
		} else if (star == 2) {
			star1.gameObject.SetActive (true);
			star2.gameObject.SetActive (true);
		} else if (star == 3) {
			star1.gameObject.SetActive (true);
			star2.gameObject.SetActive (true);
			star3.gameObject.SetActive (true);
		} else {
			star1.gameObject.SetActive (false);
			star2.gameObject.SetActive (false);
			star3.gameObject.SetActive (false);
		}

		star = 0;
//		checkStar=true;
//		GameData._checkUnlockedStg = false;
	}
}
