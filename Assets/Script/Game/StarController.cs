using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarController : MonoBehaviour {
	public static StarController instance;

	public static int StarInStage;

	public GameObject[] StarObj = new GameObject[3];

	public Image TimerBar;

	public static float timeLimit,maxTime = 90f;

	public int tempLoadStar,tempStar = 0;

	public bool isTiming;

	public GameObject transition,loseStar1,loseStar2,loseStar3,warning;

	public Button buttonSubmit;

	private static float _timeLimit{
		set{
			timeLimit = value;
		}
		get{
			return timeLimit;
		}
	}

	private static float _maxTime{
		set{
			maxTime = value;
		}
		get{
			return maxTime;
		}
	}

	void Awake(){
		instance = this;
		isTiming = true;
	}

	public void ResetStar(){
		for(int i = 0;i<StarObj.Length;i++){
			StarObj[i].SetActive(true);
		}
	}

	int StarCounter = 0;
	public void RemoveStar(){
		if(StarCounter >= 2){} //nothing
		else{
			StarObj[StarCounter].SetActive(false);
			StarCounter++;
		}

		if(StarCounter == 1){
			loseStar3.gameObject.SetActive (true);
		}else if(StarCounter == 2){
			loseStar2.gameObject.SetActive (true);
		}
	}

	public void SetStar ()
	{
		for (int i = 0; i < StarObj.Length; i++) {
			if (StarObj [i].activeSelf) {
				tempStar++;
			}
		}

		GameData._tempStar = tempStar;

		PlayerPrefs.SetInt (GameData.Key_tempStar, tempStar);

		int world = PlayerPrefs.GetInt (GameData.Key_World);
		int stage = PlayerPrefs.GetInt (GameData.Key_StageText) - 1;

		string StarDataPrefKey = "";

		if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
			StarDataPrefKey = "StarWorld" + world.ToString () + "Stage" + stage.ToString ();
			tempLoadStar = PlayerPrefs.GetInt (StarDataPrefKey);
		} else if (GameData.GilaMode.iGilaMode == 1) {
			tempLoadStar = GameData.GilaMode.GetStarValue (world, stage);
		} else if (GameData.EnglishMode.iEnglishMode == 1) {
			tempLoadStar = GameData.EnglishMode.GetStarValue(world,stage);
		}

		//kalau star yg di simpen lebih kecil dari star yg didapet -> replace
		if (tempLoadStar < tempStar) {
			if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) {
				PlayerPrefs.SetInt (StarDataPrefKey, tempStar);
			} else if(GameData.GilaMode.iGilaMode == 1){
				GameData.GilaMode.SetStarValue(world,stage,tempStar);
			} else if(GameData.EnglishMode.iEnglishMode == 1){
				GameData.EnglishMode.SetStarValue(world,stage,tempStar);
			}
			updateStarCurrency ();
		}
	}

	public void updateStarCurrency ()
	{
		if (!PlayerPrefs.HasKey (GameData.Key_starCurrency)) { //create new star currency
			PlayerPrefs.SetInt (GameData.Key_starCurrency, 0);
		}

		int tempStarCurrency;
		int getStarCurrency;

		tempStarCurrency = PlayerPrefs.GetInt (GameData.Key_starCurrency);
		getStarCurrency = tempStar - tempLoadStar;
		tempStarCurrency += getStarCurrency;
		PlayerPrefs.SetInt (GameData.Key_starCurrency, tempStarCurrency);
		
	}

	public void saveLastTime(){
		PlayerPrefs.SetFloat (GameData.Key_lastTimer,timeLimit);
	}

	public float loadLastTime(){
		return PlayerPrefs.GetFloat (GameData.Key_lastTimer);
	}

	void Start(){
		timeLimit = maxTime;
	}

	void Update(){
		if(isTiming){
			//max time
			if(timeLimit >= maxTime){
				timeLimit = maxTime;
				print ("timeLimit : " + timeLimit);
			}

			//timer
			timeLimit -= Time.deltaTime;
			TimerBar.fillAmount = timeLimit / maxTime;

			//lose star
			if(TimerBar.fillAmount <= 0.68f){
				//set off star3
				if(StarObj[0].activeSelf) StarObj[0].SetActive(false);
				loseStar3.gameObject.SetActive (true);
			}
			if(TimerBar.fillAmount <= 0.35f){
				//set off star2
				if(StarObj[1].activeSelf) StarObj[1].SetActive(false);
				loseStar2.gameObject.SetActive (true);
			}
			if(timeLimit <= 15f){
				//show warning
				warning.gameObject.SetActive (true);
			}


			//times up
			if(timeLimit <= 0f){
				//end game
				isTiming = false;
				//disable submit button
				buttonSubmit.enabled = false;
				//set off star 1
				if(StarObj[2].activeSelf) StarObj[2].SetActive(false);
				loseStar1.gameObject.SetActive (true);
				//ResultPanelController.instance.ShowResultPanel(ResultType.TIMESUP);
				PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Result);
				PlayerPrefs.SetString(GameData.Key_Result,ResultType.TIMESUP.ToString());
				StartCoroutine ("waitingTimesUp");
			}
		}
	}

	IEnumerator waitingTimesUp(){
		yield return new WaitForSeconds (0.8f);
		transition.gameObject.SetActive (true);
	}
}
