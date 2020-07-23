using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour {
	public static StartSceneController self;

	public GameObject gemuSplash,gl1Splash,lineSplash;

	public RawImage blackScreen;

	void Awake(){
		self = this;
//		PlayerPrefs.DeleteAll ();

		//EventBannerChecker.self.RequestBanner ();
		//StartCoroutine(loadSplashScreens());
	}

	void Start(){
		googlePlayLogin ();

		//register notification for iOS
		#if UNITY_IOS
		UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert| UnityEngine.iOS.NotificationType.Badge |  UnityEngine.iOS.NotificationType.Sound);
		#endif
	}

	private void googlePlayLogin(){
		Social.localUser.Authenticate((bool success) => {
			if(success){
				print("google play login success");
                if (PlayerPrefs.GetInt("GoogleLogin", 0) == 1 && !GameData._isLoggedIn)
                {
                    GPSController.Instance.OnSilentSignInSuccess = (bool success1, string name) =>
                    {
                        GameData._isLoggedIn = success1;
                        GameData.loginUserNameValue = name;
                        SceneManager.LoadScene(GameData.Scene_Menu);
                    };
                    GPSController.Instance.OnSilentSignInFailed = () =>
                    {
                        PlayerPrefs.SetInt("GoogleLogin", 0);
                        GameData._isLoggedIn = false;
                        SceneManager.LoadScene(GameData.Scene_Menu);
                    };
                    GPSController.Instance.SilentSignIn();
                }
                else
                {
                    SceneManager.LoadScene(GameData.Scene_Menu);
                }
			}else{
				print("google play login failed");
				#if UNITY_EDITOR
				SceneManager.LoadScene(GameData.Scene_Menu);
				#endif
			}
		});
	}

	IEnumerator loadSplashScreens ()
	{
		Debug.Log("asd");
//		yield return new WaitForSeconds(7);
//		lineSplash.SetActive (false);
//		yield return new WaitForSeconds (2);
//		gemuSplash.SetActive(false);
		yield return new WaitForSeconds(2);
		Application.LoadLevel(GameData.Scene_Menu);
	}

	IEnumerator fadeInOut (float fadeTo)
	{
		float elapsedTime=0;
		float time = 1;

		Color currColor = blackScreen.color;

		while (elapsedTime < time) {
			if (fadeTo == 1) {
				currColor = new Color (0, 0, 0, Mathf.Lerp (0, 1, (elapsedTime / time)));
			} else {
				currColor = new Color (0, 0, 0, Mathf.Lerp (1, 0, (elapsedTime / time)));
			}
			elapsedTime+=(Time.deltaTime);
			blackScreen.color=currColor;
			yield return null;
		}
	}

}
