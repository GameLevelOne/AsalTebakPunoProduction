using UnityEngine;
using System.Collections;

public class GetReward : MonoBehaviour {
	public GameObject[] rewardList;

	public static GetReward instance;

	void Awake(){
		instance = this;
	}

	public void getReward(){
		GameData._randomReward = Random.Range (0,5);
		rewardList [GameData._randomReward].gameObject.SetActive (true);

		if(GameData._randomReward == 0){
			GameData.gotOnePowerUp(3,4);
			GameData._rewardName = GameData.pu_show1word + " x4";
		}else if(GameData._randomReward == 1){
			GameData.gotOnePowerUp(4,3);
			GameData._rewardName = GameData.pu_show2words + " x3";
		}else if(GameData._randomReward == 2){
			GameData.gotOnePowerUp(1,4);
			GameData._rewardName = GameData.pu_ext1 + " x4";
		}else if(GameData._randomReward == 3){
			GameData.gotOnePowerUp(2,3);
			GameData._rewardName = GameData.pu_ext2 + " x3";
		}else if(GameData._randomReward == 4){
			GameData.gotOnePowerUp(5,4);
			GameData._rewardName = GameData.pu_stopTime1 + " x4";
		}else if(GameData._randomReward == 5){
			GameData.gotOnePowerUp(6,3);
			GameData._rewardName = GameData.pu_stopTime2 +  "x3";
		}
	}
}
