using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockedStageController : MonoBehaviour {
	public GameObject[] lockIcon = new GameObject[GameData.TotalStagePerWorld-1];
	public Text resetStarAmountText;
	private int World;
	private string StarDataPrefKey;
	private int Stage;
	private bool checkStage = false;

	void Start(){
		// for(int i=0;i<GameData.TotalStagePerWorld-1;i++){
		// 	Debug.Log (GameData.temp_checkLockedStg[i]);
		// }
	}

	void Update ()
	{
		if (checkStage == false) {
			checkLockedStg ();
		}
	}

	void OnEnable(){
		int tempStar = PlayerPrefs.GetInt("powerUp7",0);
		Debug.Log("resetStarAmount: "+tempStar);
		resetStarAmountText.text="x "+tempStar.ToString();
	}

	private void checkLockedStg(){
		World = PlayerPrefs.GetInt (GameData.Key_World);

		if(GameData._checkUnlockedStg == true){
			for(Stage=1 ; Stage<GameData.TotalStagePerWorld  ; Stage++){
				StarDataPrefKey = "StarWorld"+World.ToString()+"Stage"+Stage.ToString();

				#region prod
				if (PlayerPrefs.HasKey (StarDataPrefKey)) { //checking
					lockIcon [Stage-1].gameObject.SetActive (false);
				} else{
					lockIcon [Stage-1].gameObject.SetActive (true);
				}
				#endregion

				#region test (unlock all)
//				lockIcon[Stage-1].gameObject.SetActive(false);
				#endregion

				if(Stage == GameData.TotalStagePerWorld-2){ //checking finish
					GameData._checkUnlockedStg = false;
				}
			}
		}
		checkStage=true;
	}
}
