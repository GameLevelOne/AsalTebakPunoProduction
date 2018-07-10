using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingBarController : MonoBehaviour {

	public Image IMG_BarContent;

	public float speed;
	public bool loading;

	void Awake(){
		loading = false;
	}

	void Start(){
		DoLoading();
	}

	public void DoLoading(){
		IMG_BarContent.fillAmount = 0f;
		loading = true;
	}

	void Update(){
		if(loading){
			IMG_BarContent.fillAmount += speed;
			if(IMG_BarContent.fillAmount >= 1f){
				loading = false;

				ASyncOperatorController.instance.GoScene();
			}
		}
	}
}
