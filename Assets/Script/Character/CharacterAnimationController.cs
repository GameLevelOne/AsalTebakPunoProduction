using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterAnimationController : MonoBehaviour {
	public CharacterMouthController MouthController;
	public static CharacterAnimationController instance;

	public string CharCode,animTrigger;

	public Image EyeBlink,EyeDownOrUp, Hand_L, Hand_R_Correct, Hand_R_AlmostOrWrong, Hand_Idle_L, Hand_Idle_R, Mouth;
	public Animator AC_Hand_L, AC_Hand_R_Correct, AC_Hand_R_Almost, AC_Finger_R_Wrong, AC_SEMAR_Talk;

	const string ACString_Talk = "IsTalking";
	const string ACString_Wave = "isWaving";

	#region Char Animation State
	//set between talking and idle
	public void SetState_Almost(){
		CursorController.instance.waitingAnimation = true; //false-nya ada di AnimationController.cs function hideBubbleResult
		//AC_Hand_L.SetBool(ACString_Wave,true);
		AC_Hand_R_Almost.SetBool(ACString_Wave,true);
		//Debug.Log (ACString_Wave);
		if (CharCode != GameData.CHARACTER_SEMAR) { 
			//change hand
			Hand_Idle_R.gameObject.SetActive (false);
			EyeBlink.GetComponent<Image> ().enabled = false;
			EyeDownOrUp.gameObject.SetActive (true);
			Hand_R_AlmostOrWrong.gameObject.SetActive (true);

			MouthController.StartTalking ();
		} else if (CharCode == GameData.CHARACTER_SEMAR) {
			SetState_SEMAR_Talk ();
		}

		GameData.idleWaitTime = GameData.idleWaitShortTime;
		StartCoroutine ("_waitingToIdle");
	}

	public void SetState_Wrong(){
		CursorController.instance.waitingAnimation = true; //false-nya ada di AnimationController.cs function hideBubbleResult
		//AC_Hand_L.SetBool(ACString_Wave,true);
		AC_Finger_R_Wrong.SetBool(ACString_Wave,true);

		if (CharCode != GameData.CHARACTER_SEMAR) { 
			//change hand
			Hand_Idle_R.gameObject.SetActive (false);
			EyeBlink.GetComponent<Image> ().enabled = false; 
			EyeDownOrUp.gameObject.SetActive (true);
			Hand_R_AlmostOrWrong.gameObject.SetActive (true);

			MouthController.StartTalking ();
		} else if (CharCode == GameData.CHARACTER_SEMAR) {
			SetState_SEMAR_Talk ();
		}

		GameData.idleWaitTime = GameData.idleWaitShortTime;
		StartCoroutine ("_waitingToIdle");
	}

	public void SetState_Correct(){
		CursorController.instance.waitingAnimation = true; //false-nya ada di AnimationController.cs function hideBubbleResult
		//AC_Hand_L.SetBool(ACString_Wave,true);
		AC_Hand_R_Correct.SetBool(ACString_Wave,true);

		if (CharCode != GameData.CHARACTER_SEMAR) { 
			//change hand
			Hand_Idle_R.gameObject.SetActive (false);
			EyeBlink.gameObject.SetActive (false);
			EyeDownOrUp.gameObject.SetActive (true);
			EyeDownOrUp.transform.localScale = new Vector2 (1, -1); //ekspresi mata jadi senang
			Hand_R_Correct.gameObject.SetActive (true);

			MouthController.StartTalking ();

			GameData.idleWaitTime = GameData.idleWaitLongTime;
			StartCoroutine ("_waitingToIdle");
		} else if (CharCode == GameData.CHARACTER_SEMAR) {
			SetState_SEMAR_Talk ();
		}
	}

	public void SetState_Idle(){
		//AC_Hand_L.SetBool(ACString_Wave,false);
		AC_Hand_R_Almost.SetBool(ACString_Wave,false);
		AC_Finger_R_Wrong.SetBool(ACString_Wave,false);
		if(CharCode != GameData.CHARACTER_SEMAR){ 
			Hand_Idle_R.gameObject.SetActive(true);
			EyeBlink.GetComponent<Image> ().enabled = true;
			//Hand_L.gameObject.SetActive(false);
			EyeDownOrUp.gameObject.SetActive (false);
			Hand_R_AlmostOrWrong.gameObject.SetActive(false);

			MouthController.StopTalking();
		}
		else if(CharCode == GameData.CHARACTER_SEMAR) SetState_SEMAR_Idle();
	}

	//SEMAR ONLY
	public void SetState_SEMAR_Talk(){
		AC_SEMAR_Talk.SetBool(ACString_Talk,true);
	}

	public void SetState_SEMAR_Idle(){
		AC_SEMAR_Talk.SetBool(ACString_Talk,false);
	}
	#endregion

	/*public void Talk(){
		SetState_Talk();
	}*/

	public void Stop(){
		SetState_Idle();
	}

	//get animation trigger
	private void triggerAnim(){
		animTrigger = GameData._animTrigger;

		if(animTrigger == GameData.isAlmost){
			SetState_Almost();
		}else if(animTrigger == GameData.isWrong){
			SetState_Wrong ();
		}else if(animTrigger == GameData.isCorrect){
			SetState_Correct ();
		}
	}

	//blink
	void FixedUpdate(){	
		triggerAnim ();
	}

	/*void Update(){
		
	}*/

	#region numerator
	IEnumerator _waitingToIdle(){
		yield return new WaitForSeconds (GameData.idleWaitTime);

		if(GameData._animTrigger == GameData.isAlmost || GameData._animTrigger == GameData.isWrong){
			GameData._animTrigger = "";
			GameSceneController.instance.HideBubbleResult ();
			GameSceneController.instance.ShowBubbleQuestion ();
			SetState_Idle ();
		}else if(GameData._animTrigger == GameData.isCorrect){
			GameData._animTrigger = "";
			GameSceneController.instance.CloseTransition ();
		}

		StopCoroutine ("_waitingToIdle");
	}
	#endregion
}
