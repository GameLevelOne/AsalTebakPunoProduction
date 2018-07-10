using UnityEngine;
using System.Collections;

public class GameTools : MonoBehaviour {
	public static void destroyGameObject(string gameObjectName){
		GameObject theObject;

		theObject = GameObject.Find (gameObjectName);
		Destroy (theObject);
	}
}
