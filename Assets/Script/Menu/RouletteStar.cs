using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class RouletteStar : MonoBehaviour {
	public Image Img_Wheel;
	public Button close_btn;
	public Animator Anim_RouletteMenu;
	public GameObject cahaya,pointer;
	public Object Obj_Wheel;
	private Object objectName;
	public float minSwipeDistY,minSwipeDistX;

	private Vector2 startPos;
	private bool isSpinning;

	public AnimationCurve animationCurve;

	public AudioSource sfxGame;
	public AudioClip sfxRoulette;

	public float fEndAngle;
	private float fAngleResult;

	void Start(){
		fEndAngle = 0;
		isSpinning = false;
	}

	void Update(){
		objectClickedCheck ();
		swipeRoulette ();
	}

	private void objectClickedCheck(){
		if(Input.GetMouseButtonDown(0)){
			if (EventSystem.current.IsPointerOverGameObject ()) {
				Debug.Log (EventSystem.current.currentSelectedGameObject);
				if(EventSystem.current.currentSelectedGameObject == Obj_Wheel){
					objectName = EventSystem.current.currentSelectedGameObject;
				}
			}
		}
	}

	private void swipeRoulette ()
	{
		if (objectName == Obj_Wheel && Input.touchCount > 0) {
			Touch touch = Input.touches [0];

			switch (touch.phase) {
			case TouchPhase.Began:
				startPos = touch.position;
				break;
			case TouchPhase.Ended:
				float swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;

				if (swipeDistVertical > minSwipeDistY) {
					float swipeValue = Mathf.Sign (touch.position.y - startPos.y);

//					if (swipeValue > 0) {//up swipe
//						DoSpin();
//					} else 
					if (swipeValue < 0) {//down swipe
						if (isSpinning == false) {
							DoSpin ();	
						}
					}
				}

				float swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
				if (swipeDistHorizontal > minSwipeDistX) {
					float swipeValue = Mathf.Sign (touch.position.x - startPos.x);

//					if (swipeValue > 0) {//right swipe
//						DoSpin();
//					} else
//					if (swipeValue < 0) {//left swipe
//						DoSpin();
//					}
				}
				break;
			}
		}

	}

	private void DoSpin(){
		//randomize prize, every 60 degree {0, 60, 120, 180, 240, 300}
		isSpinning = true;

		cahaya.gameObject.SetActive (true);
		pointer.gameObject.SetActive (false);
		StartCoroutine(Spin(60f));
		close_btn.enabled = false;
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE,sfxGame,sfxRoulette);
	
		AppsFlyerController.instance._trackRichEvent ("do_spin_roulette_star", "roulette_spinning", "roulette_star_event");
	}

	private void winningPrize(float result){
		print ("result : " + result);
		if(result >= 0 && result <= 1){
			PlayerPrefs.SetInt (GameData.Key_prizeCode, 1);
			print ("test");

			AppsFlyerController.instance._trackRichEvent ("get_prize_roulette_star", "got_3star", "roulette_star_event");
		}else if(result >= 60 && result <= 61){
			PlayerPrefs.SetInt (GameData.Key_prizeCode, 6);
			print ("test5");

			AppsFlyerController.instance._trackRichEvent ("get_prize_roulette_star", "got_1star", "roulette_star_event");
		}else if(result >= 120 && result <= 121){
			PlayerPrefs.SetInt (GameData.Key_prizeCode, 5);
			print ("test4");			

			AppsFlyerController.instance._trackRichEvent ("get_prize_roulette_star", "got_2star", "roulette_star_event");
		}else if(result >= 180 && result <= 181){
			PlayerPrefs.SetInt (GameData.Key_prizeCode, 4);
			print ("test3");

			AppsFlyerController.instance._trackRichEvent ("get_prize_roulette_star", "got_4star", "roulette_star_event");
		}else if(result >= 240 && result <= 241){
			PlayerPrefs.SetInt (GameData.Key_prizeCode, 3);
			print ("test2");

			AppsFlyerController.instance._trackRichEvent ("get_prize_roulette_star", "got_1star", "roulette_star_event");
		}else if(result >= 300 && result <= 301){
			PlayerPrefs.SetInt (GameData.Key_prizeCode, 2);
			print ("test1");

			AppsFlyerController.instance._trackRichEvent ("get_prize_roulette_star", "got_2star", "roulette_star_event");
		}

		GameData._usedRoulette--;
		GameData._onMenuScene = GameData.WORLD_MENU;
		GameData._sceneState = GameData.ROULETTE_STAR_MENU;
		GameData._buttonTag = GameData.Tag_Used;

		isSpinning = false;

		Anim_RouletteMenu.SetTrigger (GameData.Hide);
	}

	IEnumerator Spin(float fAngle){
		int randomSpd = Random.Range (20, 32);

		fAngle = -1f * ((float) randomSpd * fAngle);//10,18
		Debug.Log ("fangle : " + fAngle + " | randomSpd : " + randomSpd);

		float fTimer = 0;
		float fStartAngle = Img_Wheel.gameObject.transform.eulerAngles.z;

		while(fTimer < 10f){
			fEndAngle = animationCurve.Evaluate(fTimer/10f) * fAngle;
			Img_Wheel.gameObject.transform.eulerAngles = new Vector3(0f,0f,(fEndAngle + fStartAngle));
			fTimer += Time.deltaTime;
			yield return 0;
		}

		fAngleResult = Img_Wheel.gameObject.transform.eulerAngles.z;
		//z = the angle result from spinning. validate with appropriate prize; 
		winningPrize(fAngleResult);
		print (fAngleResult);
	}
}
