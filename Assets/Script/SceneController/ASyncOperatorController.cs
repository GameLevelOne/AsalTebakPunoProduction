using UnityEngine;
using System.Collections;

public class ASyncOperatorController : MonoBehaviour {
	public static ASyncOperatorController instance;

	void Awake(){
		instance = this;
	}

	public void GoScene(){
		StartCoroutine(DoAsync());
	}

	IEnumerator DoAsync() {
		AsyncOperation async = Application.LoadLevelAsync(PlayerPrefs.GetString(GameData.Key_SceneToGo));
//		PlayerPrefs.DeleteKey(GameData.Key_SceneToGo);

		yield return async;
	}
	
}
