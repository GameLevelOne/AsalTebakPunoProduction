#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class PreloadSigningAlias
{

	static PreloadSigningAlias ()
	{
		PlayerSettings.Android.keystorePass = "punogl1";
		PlayerSettings.Android.keyaliasName = "puno";
		PlayerSettings.Android.keyaliasPass = "punogl1";
	}

}

#endif