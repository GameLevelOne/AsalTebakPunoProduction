using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTitleController : MonoBehaviour {
	public static LevelTitleController instance;

	public Text Text_Level;

	void Awake(){
		instance = this;
		SetStageLevel();
	}

	public void SetStageLevel(){
		int Stage = PlayerPrefs.GetInt(GameData.Key_StageText);
		Text_Level.text = Stage.ToString();
	}
}
