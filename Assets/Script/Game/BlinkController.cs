using UnityEngine;
using System.Collections;

public class BlinkController : MonoBehaviour {
	public string CharCode;
	public Animator AC_Blink;

	const string ACString_Blink = "IsBlink";

	float t;

	void Start(){		
		t = 0f;
	}

	void FixedUpdate(){
		if(CharCode != GameData.CHARACTER_SEMAR){
			t += Time.deltaTime;

			if(t > 1f){
				float a = Random.value;
				if(a > 0.7f){
					SetState_Blink();
				}

				t = 0f;
			}
		}
	}

	//set blinking
	public void SetState_Blink(){
		AC_Blink.SetBool(ACString_Blink,true);
		StartCoroutine(SetStateBack());
	}

	IEnumerator SetStateBack(){
		yield return new WaitForSeconds(0.5f);
		AC_Blink.SetBool(ACString_Blink,false);
	}
}
