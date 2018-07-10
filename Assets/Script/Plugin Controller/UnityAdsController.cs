using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsController : MonoBehaviour
{
	public static UnityAdsController instance;

	void Awake(){
		instance = this;
		Debug.Log("unityadscontroller");
	}

	public void ShowAd()
	{
		print ("ENTER SHOW AD");
		if (Advertisement.IsReady())
		{
			print ("ENTER ADS IS READY");
			Advertisement.Show();
		}
	}
}