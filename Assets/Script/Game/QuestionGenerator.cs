using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class QuestionGenerator : MonoBehaviour {
	//1 baris 22 char (termasuh spasi)
	public static QuestionGenerator instance;

	private string QUESTION;
	private char[] questionTemp;

	const int charLimitRow = 22;

	public Text Text_Question;

	TextAsset asset;

	void Awake(){
		instance = this;
	}

	public void GenerateQuestion ()
	{
		//load data from pref
		int world = PlayerPrefs.GetInt (GameData.Key_World) + 1;
		int stage = PlayerPrefs.GetInt (GameData.Key_Stage);

		string String_World = "world" + world.ToString ();

		//load and parse data resource (JSON)
		if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) { //normal mode
			asset = (TextAsset)Resources.Load (GameData.LevelDataResource, typeof(TextAsset));
		} else if (GameData.GilaMode.iGilaMode == 1) { //gila mode
			asset = (TextAsset)Resources.Load (GameData.LevelDataResource_GilaMode, typeof(TextAsset));
		} else if (GameData.EnglishMode.iEnglishMode == 1) { //english mode
			asset = (TextAsset)Resources.Load (GameData.LevelDataResource_EnglishMode, typeof(TextAsset));
		}
		JSONNode node = JSON.Parse (asset.text);

		QUESTION = node[String_World][stage]["question"];
		Text_Question.text = QUESTION;
	}

	public void validatingNextRow(){
		int totalRow = Mathf.FloorToInt ((float) QUESTION.Length / (float) charLimitRow);
		int lastLetterInRow;

		questionTemp = Text_Question.text.ToCharArray();
		for(int i=0;i<totalRow;i++){			
			lastLetterInRow = (i+1) * charLimitRow;

			try{
				while(questionTemp[lastLetterInRow] != '\n'){
					if(questionTemp[lastLetterInRow] == ' '){
						questionTemp  [lastLetterInRow] = '\n';						
					}else if(questionTemp[lastLetterInRow] != ' '){
						lastLetterInRow++;
						if(questionTemp[lastLetterInRow] == ' '){
							questionTemp  [lastLetterInRow] = '\n';	
						}
					}
				}
			}
			catch{
				Debug.Log ("it's okay");
			}
		}
		Text_Question.text = new string (questionTemp);
	}
}
