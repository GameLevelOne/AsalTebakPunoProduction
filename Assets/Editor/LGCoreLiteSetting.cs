using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public class LGCoreLiteSetting 
{

	//true if enable lgcore lite post process.
	[System.Xml.Serialization.XmlElement("postbuild_enable")]
	public bool PBEnabled = false;

	[System.Xml.Serialization.XmlElement("bundle_id")]
	public string BundleID = "com.linecorp.LGSDKTEST";

	[System.Xml.Serialization.XmlElement("app_id")]
	public string AppID = "LGSDKTEST";


	[System.Xml.Serialization.XmlElement("chanunnel_id")]
	public string ChannelID = "1367581746";

	//[System.Xml.Serialization.XmlElement("postbuild_ios")]
	//public bool PBEnabled = false;

	public void CopyFrom(LGCoreLiteSetting src)
	{
		PBEnabled = src.PBEnabled;
		BundleID = src.BundleID;
		AppID = src.AppID;
		ChannelID = src.ChannelID;
	}


	private static LGCoreLiteSetting s_Setting = new LGCoreLiteSetting();
	public static LGCoreLiteSetting GetGlobalSetting()
	{
		return s_Setting;
	}
	public static void UpdateGlobalSetting(LGCoreLiteSetting setting)
	{
		
		s_Setting.CopyFrom( setting );
		
	}


}
