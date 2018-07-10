using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml.Serialization;
using System.IO;

public class LGCoreLiteSettingUI : EditorWindow 
{

	private const string SETTING_FILEPATH = "Assets/Editor/LGCoreLiteSetting.lgxml";

	[MenuItem("LGCoreLite/Post Build Setup...", false, 1)]
	public static void MenuItemIosSetup()
	{
		EditorWindow.GetWindow( typeof(LGCoreLiteSettingUI) );
	}

	void OnEnable()
	{
		LoadSetting( SETTING_FILEPATH );

	}

	private void LoadSetting(string settingPath)
	{
		if (File.Exists(settingPath))
		{
			using (FileStream inStream = File.OpenRead(settingPath))
			{
			   XmlSerializer x = new XmlSerializer(typeof(LGCoreLiteSetting));
			   LGCoreLiteSetting setting = (LGCoreLiteSetting)x.Deserialize(inStream);
			   LGCoreLiteSetting.UpdateGlobalSetting(setting);
			}
		}

	}

	private void SaveSetting(string settingPath)
	{
		XmlSerializer ser = new XmlSerializer(typeof(LGCoreLiteSetting));
		TextWriter writer = new StreamWriter(settingPath);
		ser.Serialize(writer, LGCoreLiteSetting.GetGlobalSetting());
		writer.Close();
	}

	Vector2 scrollPosition = Vector2.zero;
	void OnGUI()
	{
		scrollPosition = GUILayout.BeginScrollView( scrollPosition, GUI.skin.box );

		EditorGUILayout.HelpBox("Check \"Enable Post Build\" to enable post build process for Line Game SDK setup. To avoid unwanted overwrite, it's " +
			"recommanded to only enable it when first create project. Please see the Document for further detail", 
		                        	MessageType.Info);
		LGCoreLiteSetting.GetGlobalSetting().PBEnabled = EditorGUILayout.Toggle("Enable Post Build", LGCoreLiteSetting.GetGlobalSetting().PBEnabled);
		GUILayout.Space(10);

		LGCoreLiteSetting.GetGlobalSetting().AppID = EditorGUILayout.TextField( "App ID",LGCoreLiteSetting.GetGlobalSetting().AppID );
		LGCoreLiteSetting.GetGlobalSetting().ChannelID = EditorGUILayout.TextField( "Channel ID", LGCoreLiteSetting.GetGlobalSetting().ChannelID );
		LGCoreLiteSetting.GetGlobalSetting().BundleID = EditorGUILayout.TextField( "iOS Bundle ID", LGCoreLiteSetting.GetGlobalSetting().BundleID );

		if( GUILayout.Button("Save Current Setting") )
		{
			SaveSetting(SETTING_FILEPATH);
		}


		GUILayout.EndScrollView();
	}

	
	
}
