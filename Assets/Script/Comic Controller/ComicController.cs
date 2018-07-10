using UnityEngine;
using System.Collections;

public class ComicController : MonoBehaviour {
	public GameObject transition;
	private Animator comic;
	public AudioSource sfxComic;
	public AudioClip sfxButton;

	public void nextButtonController (string comicPageName)
	{
		GameData.soundSourceAnotherGO (GameData.SFX_SOUNDSOURCE, sfxComic, sfxButton);

		//controller next button komik jakarta
		if (comicPageName == GameData.comic_jkt_1) {
			Application.LoadLevel (GameData.comic_jkt_2);
			#if !UNITY_EDITOR
			AppsFlyerController.instance._trackRichEvent ("reading_comic", "reading_comic_jakarta", "reading_comic_event");
			#endif
		}

		if (comicPageName == GameData.comic_jkt_2) {
			Application.LoadLevel (GameData.comic_jkt_3);
		}

		if (comicPageName == GameData.comic_jkt_3) {
			if (PlayerPrefs.GetInt (GameData.Key_World_Comic) == 0) {
				PlayerPrefs.SetInt (GameData.Key_World_Comic, 3); //see you on next comic (Bali)
			}

			changeScene ();
		}

		//controller next button komik bali
		if (comicPageName == GameData.comic_bl_1) {
			Application.LoadLevel (GameData.comic_bl_2);
			#if !UNITY_EDITOR
			AppsFlyerController.instance._trackRichEvent ("reading_comic", "reading_comic_bali", "reading_comic_event");
			#endif
		}

		if (comicPageName == GameData.comic_bl_2) {
			if (PlayerPrefs.GetInt (GameData.Key_World_Comic) == 3) {
				PlayerPrefs.SetInt (GameData.Key_World_Comic, 6); //see you on next comic (Padang)
			}

			changeScene ();
		}

		//controller next button komik padang
		if (comicPageName == GameData.comic_pd_1) {
			Application.LoadLevel (GameData.comic_pd_2);
			#if !UNITY_EDITOR
			AppsFlyerController.instance._trackRichEvent ("reading_comic", "reading_comic_padang", "reading_comic_event");
			#endif
		}

		if (comicPageName == GameData.comic_pd_2) {
			if (PlayerPrefs.GetInt (GameData.Key_World_Comic) == 6) {
				PlayerPrefs.SetInt (GameData.Key_World_Comic, 9);
			}

			changeScene ();
		}

		if (comicPageName == GameData.comic_trj) {
			#if !UNITY_EDITOR
			AppsFlyerController.instance._trackRichEvent("reading_comic", "reading_comic_toraja", "reading_comic_event");
			#endif
			if (PlayerPrefs.GetInt (GameData.Key_World_Comic) == 9) {
				PlayerPrefs.SetInt(GameData.Key_World_Comic,12);
			}
			changeScene();
		}

		if (comicPageName == GameData.comic_jog) {
			#if !UNITY_EDITOR
			AppsFlyerController.instance._trackRichEvent("reading_comic", "reading_comic_jogja", "reading_comic_event");
			#endif
			if (PlayerPrefs.GetInt (GameData.Key_World_Comic) == 12) {
				PlayerPrefs.SetInt(GameData.Key_World_Comic,15);
			}
			changeScene();
		}
	}

	private void changeScene(){
		if(GameData._sceneState == GameData.Scene_Menu){
			PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Menu);
			GameData._sceneState = GameData.Scene_Comic;
			GameData._onMenuScreen = true;
			GameData._checkUnlockedStg = true;
		}else if(GameData._sceneState == GameData.Scene_Result){
			PlayerPrefs.SetString (GameData.Key_SceneToGo,GameData.Scene_Game);
		}

		transition.gameObject.SetActive (true);
	}

	public void skipPanel(){
		comic = GameObject.Find ("ComicController").GetComponent<Animator> ();
		comic.ForceStateNormalizedTime (GameData._frameAnim);
		Debug.Log(GameData._frameAnim);
	} 

	IEnumerator autoChangePage(string page){
		yield return new WaitForSeconds (5);
		Application.LoadLevel (page);
	}
}
