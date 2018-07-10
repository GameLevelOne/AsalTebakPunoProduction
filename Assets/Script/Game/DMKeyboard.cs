using UnityEngine;
using System.Collections;
//INPUT KEY FROM PC KEYBOARD, ONLY ON EDITOR MODE
public class DMKeyboard : MonoBehaviour {
	private void _inputPCKey(){
		KeyboardController keyboardControler = new KeyboardController ();

		if (Input.GetKeyDown (KeyCode.Q)) {
			keyboardControler.OnAlphabetButton ("Q");
		} else if (Input.GetKeyDown (KeyCode.W)) {
			keyboardControler.OnAlphabetButton ("W");
		} else if (Input.GetKeyDown (KeyCode.E)) {
			keyboardControler.OnAlphabetButton ("E");
		} else if (Input.GetKeyDown (KeyCode.R)) {
			keyboardControler.OnAlphabetButton ("R");
		} else if (Input.GetKeyDown (KeyCode.T)) {
			keyboardControler.OnAlphabetButton ("T");
		} else if (Input.GetKeyDown (KeyCode.Y)) {
			keyboardControler.OnAlphabetButton ("Y");
		} else if (Input.GetKeyDown (KeyCode.U)) {
			keyboardControler.OnAlphabetButton ("U");
		} else if (Input.GetKeyDown (KeyCode.I)) {
			keyboardControler.OnAlphabetButton ("I");
		} else if (Input.GetKeyDown (KeyCode.O)) {
			keyboardControler.OnAlphabetButton ("O");
		} else if (Input.GetKeyDown (KeyCode.P)) {
			keyboardControler.OnAlphabetButton ("P");
		} else if (Input.GetKeyDown (KeyCode.A)) {
			keyboardControler.OnAlphabetButton ("A");
		} else if (Input.GetKeyDown (KeyCode.S)) {
			keyboardControler.OnAlphabetButton ("S");
		} else if (Input.GetKeyDown (KeyCode.D)) {
			keyboardControler.OnAlphabetButton ("D");
		} else if (Input.GetKeyDown (KeyCode.F)) {
			keyboardControler.OnAlphabetButton ("F");
		} else if (Input.GetKeyDown (KeyCode.G)) {
			keyboardControler.OnAlphabetButton ("G");
		} else if (Input.GetKeyDown (KeyCode.H)) {
			keyboardControler.OnAlphabetButton ("H");
		} else if (Input.GetKeyDown (KeyCode.J)) {
			keyboardControler.OnAlphabetButton ("J");
		} else if (Input.GetKeyDown (KeyCode.K)) {
			keyboardControler.OnAlphabetButton ("K");
		} else if (Input.GetKeyDown (KeyCode.L)) {
			keyboardControler.OnAlphabetButton ("L");
		} else if (Input.GetKeyDown (KeyCode.Z)) {
			keyboardControler.OnAlphabetButton ("Z");
		} else if (Input.GetKeyDown (KeyCode.X)) {
			keyboardControler.OnAlphabetButton ("X");
		} else if (Input.GetKeyDown (KeyCode.C)) {
			keyboardControler.OnAlphabetButton ("C");
		} else if (Input.GetKeyDown (KeyCode.V)) {
			keyboardControler.OnAlphabetButton ("V");
		} else if (Input.GetKeyDown (KeyCode.B)) {
			keyboardControler.OnAlphabetButton ("B");
		} else if (Input.GetKeyDown (KeyCode.N)) {
			keyboardControler.OnAlphabetButton ("N");
		} else if (Input.GetKeyDown (KeyCode.M)) {
			keyboardControler.OnAlphabetButton ("M");
		}
	}

	void Update () {
		_inputPCKey ();
	}
}
