using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMouthController : MonoBehaviour {
	public Image Img_Mouth;

	public Sprite Sprite_Idle;
	public Sprite[] Sprite_Talk;

	int SpriteAmt, tempr;
	float t;
	public bool IsTalking;
	public float TalkGap = 0.2f;

	#region talk functions
	public void StartTalking(){
		IsTalking = true;
	}
	
	public void StopTalking(){
		IsTalking = false;
		Img_Mouth.sprite = Sprite_Idle;
		t = 0;
	}
	#endregion

	void Start(){
		t = tempr = 0;
		IsTalking = false;
		SpriteAmt = Sprite_Talk.Length;
	}

	void Update(){
		if(IsTalking){

			//randomize mouth sprite
			t += Time.deltaTime;

			if(t > TalkGap){
				//do randomize
				int rand = Random.Range(0,SpriteAmt);
				while(tempr == rand){
					rand = Random.Range(0,SpriteAmt);
				}
				tempr = rand;
				Img_Mouth.sprite = Sprite_Talk[rand];

				t = 0f;
			}
		}
	}
}
