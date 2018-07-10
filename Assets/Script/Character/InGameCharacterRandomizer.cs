using UnityEngine;
using System.Collections;

public class InGameCharacterRandomizer : MonoBehaviour {
	public static InGameCharacterRandomizer instance;


	//fix x & y position of characters
	private float[] Y = new float[]{98f, 35f, 95f, 57f};
	private  float[] X = new float[]{-360f, 360f};

	//bagong gareng petruk semar
	public GameObject[] Characters = new GameObject[4];
	
	public GameObject AskingChar, AnsweringChar;

	void Awake(){
		instance = this;
	}

	public void GenerateCharacter(){
		//randomize index (ask -> all char | ans -> without semar)
		int randAsk = Random.Range(0,4);
		int randAns = Random.Range(0,3);
		//no 2 chars
		while(randAns == randAsk){
			randAns = Random.Range(0,3);
		}

		//generate asking character
		AskingChar = GameObject.Instantiate(Characters[randAsk],Vector3.zero,Quaternion.identity) as GameObject;
		AskingChar.transform.SetParent(this.gameObject.transform,false);
		AskingChar.GetComponent<CharacterAnimationController> ().enabled = false;

		RectTransform a = AskingChar.GetComponent<RectTransform>();
		a.anchoredPosition = new Vector2(X[0], Y[randAsk]);


		//generate answering character
		AnsweringChar = GameObject.Instantiate(Characters[randAns],Vector3.zero,Quaternion.identity) as GameObject;
		AnsweringChar.transform.SetParent(this.gameObject.transform,false);

		RectTransform b = AnsweringChar.GetComponent<RectTransform>();
		b.anchoredPosition = new Vector2(X[1],Y[randAns]);
	}

	public void ResetCharacter(){
		Destroy(AskingChar);
		Destroy(AnsweringChar);

		AskingChar = null;
		AnsweringChar = null;
	}

}
