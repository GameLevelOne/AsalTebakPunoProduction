using UnityEngine;
using System.Collections;

public class GPAchievementController : MonoBehaviour {
	public static GPAchievementController instance;

	void Awake(){
		instance = this;
		Debug.Log("gpachievementcontroller");
	}

	public void _achievement_answer_question_in_10_sec(){
		float lastTimer;

		lastTimer = StarController.instance.loadLastTime ();
		if(lastTimer >= 80f){
			Social.ReportProgress (GP_Achievement.achievement_answer_question_in_10_sec,100.0f,(bool success) => {});
		}
	}

	public void _achievement_answer_question_in_30_sec(){
		float lastTimer;

		lastTimer = StarController.instance.loadLastTime ();
		if(lastTimer >= 60f){
			Social.ReportProgress (GP_Achievement.achievement_answer_question_in_30_sec,100.0f,(bool success) => {});
		}
	}

	public void _achievement_new_player(){
		Social.ReportProgress (GP_Achievement.achievement_new_player,100.0f,(bool success) => {});
	}

	public void _achievement_clear_world_1(){
		int starStage;

		starStage = PlayerPrefs.GetInt (GameData.Key_lastStageWorldJKT);
		if(starStage > 0){
			Social.ReportProgress (GP_Achievement.achievement_clear_world_1,100.0f,(bool success) => {});
		}
	}

	public void _achievement_clear_world_2(){
		int starStage;

		starStage = PlayerPrefs.GetInt (GameData.Key_lastStageWorldBL);
		if(starStage > 0){
			Social.ReportProgress (GP_Achievement.achievement_clear_world_2,100.0f,(bool success) => {});
		}
	}
	public void _achievement_clear_world_3(){
		int starStage;

		starStage = PlayerPrefs.GetInt (GameData.Key_lastStageWorldPD);
		if(starStage > 0){
			Social.ReportProgress (GP_Achievement.achievement_clear_world_3,100.0f,(bool success) => {});
		}
	}

	public void _achievement_first_use_power_up(){
		int totalUsingPowerUp;

		totalUsingPowerUp = PlayerPrefs.GetInt (GameData.Key_usingPowerUp);
		if(totalUsingPowerUp == 1){
			Social.ReportProgress (GP_Achievement.achievement_first_use_power_up,100.0f,(bool success) => {});
		}
	}

	public void _achievement_collect_30_star(){
		int totalStar;

		//totalStar = GameData.totalStarAllWorld ();
		totalStar = PlayerPrefs.GetInt(GameData.Key_starCurrency);
		if(totalStar >= 30){
			Social.ReportProgress (GP_Achievement.achievement_collect_30_star,100.0f,(bool success) => {});
		}
	}

	public void _achievement_collect_60_star(){
		int totalStar;

		//totalStar = GameData.totalStarAllWorld ();
		totalStar = PlayerPrefs.GetInt(GameData.Key_starCurrency);
		if(totalStar >= 60){
			Social.ReportProgress (GP_Achievement.achievement_collect_60_star,100.0f,(bool success) => {});
		}
	}

}
