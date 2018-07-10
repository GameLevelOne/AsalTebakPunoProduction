using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using LGCoreLite;

public class Logger : MonoBehaviour
{
	public Text consoleText;
	public Scrollbar verticalScrollbar;
	public bool autoScrollToBottom = true;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Debug( string msg )
	{
		UnityEngine.Debug.Log(msg);

		consoleText.text = consoleText.text + msg + "\n\n";

		if( autoScrollToBottom && verticalScrollbar != null ) {
			verticalScrollbar.value = 0;
		}
	}
	public void Clear()
	{
		consoleText.text = "";
		if( verticalScrollbar != null ) {
			verticalScrollbar.value = 1;
		}
	}


}
