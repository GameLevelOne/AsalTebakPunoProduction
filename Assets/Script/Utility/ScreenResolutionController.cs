using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionController : MonoBehaviour {

	void Start () {
		if(2 * Screen.width <= Screen.height)
        {
            var canvasScaler = this.GetComponent<CanvasScaler>();
            if (canvasScaler)
            {
                if (canvasScaler.screenMatchMode == CanvasScaler.ScreenMatchMode.MatchWidthOrHeight)
                {
                    canvasScaler.matchWidthOrHeight = 0;
                }
            }
        }
	}

}
