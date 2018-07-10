using UnityEngine;
using System.Collections;

public class CloudAnimController : MonoBehaviour {
	public Animation Awan1, Awan2, Awan3;

	const string AnimAwan1 = "Awan1";
	const string AnimAwan2 = "Awan2";
	const string AnimAwan3 = "Awan3";

	const float Awan1Start = 20.21f;
	const float Awan2Start = 5.2f;
	const float Awan3Start = 38f;

	public void PlayCloudAnim(){
		Awan1[AnimAwan1].time = Awan1Start;
		Awan2[AnimAwan2].time = Awan2Start;
		Awan3[AnimAwan3].time = Awan3Start;

		Awan1.Play(AnimAwan1);
		Awan2.Play(AnimAwan2);
		Awan3.Play(AnimAwan3);
	}
}
