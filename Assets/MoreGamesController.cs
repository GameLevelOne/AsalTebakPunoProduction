using UnityEngine;
using System.Collections;

public class MoreGamesController : MonoBehaviour {

	public void OnBaskethoopButton(){
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.GemuGemu.BasketHoop");
	}
	public void OnClawmaniaButon(){
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.GameLevelOne.ClawMania");
	}
	public void OnFingerdashButton(){
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.GemuGemu.FingerDash");
	}
	public void OnPixelgameButton(){
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.gemugemu.pixelgame");
	}
	public void OnSMArenaButton (){
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.gamelevelone.arena");
	}
}
