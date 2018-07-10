using UnityEngine;
using System.Collections;

public class RoulettePointerController : MonoBehaviour {
	public Animator roulettePointer;

	void Start () {
		StartCoroutine (_pointing());
	}

	IEnumerator _pointing(){
		yield return new WaitForSeconds (10);
		roulettePointer.SetTrigger (GameData.Hide);
	}
}
