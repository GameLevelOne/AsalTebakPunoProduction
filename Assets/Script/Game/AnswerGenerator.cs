using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class AnswerGenerator : MonoBehaviour {
	public static AnswerGenerator instance;

	public GameObject ContentObject, CharObject;
	public string real_ANSWER,ANSWER,addSpace;
	public int letterExist;

	private string temp1;
	private string temp2;

	private char[] answerTemp;
	private char[] trimmedChar;
	private char[] replaceChar;
	public GameObject[] AnswerChars;

	TextAsset asset;

	#region CONSTANTS
	//base anchored position
	const float baseX = -300f;
	const float baseY = 20f;

	//multiplier
	const float multX = 42f;
	const float multY = -80f;
	#endregion

	float tempX, tempY;

	//char limit in a row
	public int charLimitRow = 17;//12

	void Awake(){
		instance = this;
	}
	//Anchored Position Base
	//X = -300f, Y = 20f


	//get the answer from database (JSON)
	//create GameObject Alphabet as much as answer length.
	//put display inside answer BubbleChat

	public void sortingAnswerText(int startIndex){
		//print ("startIndex : " + startIndex);

		int totalRow = Mathf.FloorToInt ((float) ANSWER.Length / (float) charLimitRow);
		int nextLetterInLastRow;
		string testPrint;

		answerTemp = ANSWER.ToCharArray ();

		addSpace = "";

		try{
			for(int a=startIndex;a<=totalRow;a++){
				nextLetterInLastRow = (a + 1) * charLimitRow;

				if(answerTemp[nextLetterInLastRow] != ' ' && answerTemp[nextLetterInLastRow-1] != ' '){
					int checkIndex;

					checkIndex = nextLetterInLastRow - 1;

					while(answerTemp[checkIndex] != ' '){
						letterExist++;
						checkIndex--;
					}

					letterExist += 1;
					answerTemp [checkIndex] = '*';

					testPrint = new string (answerTemp);

					for(int c=0;c<letterExist;c++){
						print ("c : " + c);
						addSpace = addSpace + " ";
					}

					letterExist = 0;

					replaceChar = new string (answerTemp).Replace ("*", addSpace).ToCharArray ();
					ANSWER = new string (replaceChar);
				}

				addSpace = "";

				if(answerTemp[nextLetterInLastRow] == ' '){
					trimmedChar = new string(answerTemp).Remove(nextLetterInLastRow,1).ToCharArray();
					ANSWER = new string(trimmedChar);
					//sortingAnswerText(i+1);
				}

				answerTemp = ANSWER.ToCharArray ();

				//print ("ANSWER: " + ANSWER);
			}
		}catch{
			print ("it's okay");
		}
	}

	public void checkTotalLetters(){
		for(int index = 0;index < ANSWER.Length;index++){
			if(AnswerChars[index].GetComponent<Text>().text == "_"){
				GameData._totalLetters++;
			}
		}
	}

	public void GenerateAnswer ()
	{
		int world = PlayerPrefs.GetInt (GameData.Key_World) + 1;
		int stage = PlayerPrefs.GetInt (GameData.Key_Stage);

		string String_World = "world" + world.ToString ();

		//get data from resource
		if (GameData.GilaMode.iGilaMode == 0 && GameData.EnglishMode.iEnglishMode == 0) { //normal mode
			asset = (TextAsset)Resources.Load (GameData.LevelDataResource, typeof(TextAsset));
		} else if (GameData.GilaMode.iGilaMode == 1) { //gila mode
			asset = (TextAsset)Resources.Load (GameData.LevelDataResource_GilaMode, typeof(TextAsset));
		} else if (GameData.EnglishMode.iEnglishMode == 1) { //english mode
			asset = (TextAsset)Resources.Load (GameData.LevelDataResource_EnglishMode, typeof(TextAsset));
		}
		JSONNode node = JSON.Parse (asset.text);

		real_ANSWER = node[String_World][stage]["answer"];
		ANSWER = real_ANSWER;

		sortingAnswerText (0);
		//trimAnswerText ();

		//get length
		int Ans_Length = ANSWER.Length;
		print ("ans-length : " + Ans_Length);

		//initialize new objects
		AnswerChars = new GameObject[Ans_Length];

		for(int i=0;i<Ans_Length;i++){
			//generate position
			tempX = GenerateX(i);
			tempY = GenerateY(i);

			//isntantiate obj to , attach to the parent obj.
			AnswerChars	[i] = GameObject.Instantiate(CharObject, Vector3.zero, Quaternion.identity) as GameObject;
			AnswerChars	[i].name = "Char"+i.ToString();
			AnswerChars	[i].transform.SetParent(ContentObject.transform,false);
			AnswerChars [i].GetComponent<AnswerLetterController> ().charIndex = i;
			AnswerChars	[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(tempX,tempY);

			//validate space
			char tempChar = ANSWER[i];
			if (tempChar == ' ') {
				AnswerChars [i].GetComponent<Text> ().text = string.Empty;
			}
		}

		//check total letters
		checkTotalLetters ();

		//store answer objects to container
		AnswerContainer.instance.setCorrectAnswer(ANSWER); //real correct answer (string)
		AnswerContainer.instance.SetAnswerChars(AnswerChars); //answer objects (char)
	}

	#region PRIVATE Function
	float GenerateX(int index){
		return ( ( ( (float)(index % charLimitRow) ) * multX ) + baseX);
	}
	float GenerateY(int index){
		int row = Mathf.FloorToInt(index / charLimitRow);
		return ( ( ( (float) row ) * multY ) + baseY);
	}
	#endregion
}
