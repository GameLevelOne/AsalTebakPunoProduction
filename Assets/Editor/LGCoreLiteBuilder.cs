using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor;
using System.IO;
using UnityEditor.iOS.Xcode;
using LGCUnity.Editor;

public class LGCoreLiteBuilder 
{

	[PostProcessBuild]
	static void onPostProcessBuild( BuildTarget buildTarget, string path )
	{
		if(!LGCoreLiteSetting.GetGlobalSetting().PBEnabled)
			return;

		UnityEngine.Debug.Log( "PostProcessBuild LGCoreLite Begins" );

		//post processing xcode project
		if (buildTarget == BuildTarget.iOS) 
		{
			// LGCoreLite ios files Dest dir
			bool hasPluginFiles = false; //flag indicate it's Unity5 and copies plugin files itself
			string projDestRelPath = "Frameworks/Plugins/iOS/LGCoreLite";
			
			if(Directory.Exists(path + "/" + projDestRelPath))
			{
				UnityEngine.Debug.Log( "Plugin files already copied" );
				hasPluginFiles = true;
			}
			//no plugin files copied
			else
			{
				projDestRelPath = "LGCoreLite";
			}
			string lgDestDir = path + "/" + projDestRelPath;

			// LGCoreLite ios files source dir
			const string PluginPath = "Assets/Plugins/iOS/";
			string lgSourceDir = PluginPath + "LGCoreLite";

			// xcode project path
			string projectPath = Path.Combine (path, "Unity-iPhone.xcodeproj/project.pbxproj");
			PBXProject proj = new PBXProject ();

			//append project file
			proj.ReadFromString (File.ReadAllText (projectPath));

			//target
			string target = PBXProject.GetUnityTargetName ();
			target = proj.TargetGuidByName (target);
			UnityEngine.Debug.Log( "target: " + target );
			
			// Add System Frameworks
			UnityEngine.Debug.LogWarning ("Adding system frameworks to xcodeproj...");
			proj.AddFrameworkToProject (target, "CoreTelephony.framework", false);
			proj.AddFrameworkToProject (target, "Security.framework", false);
			proj.AddFrameworkToProject (target, "CoreGraphics.framework", false);
			proj.AddFrameworkToProject (target, "AdSupport.framework", false);
			proj.AddFrameworkToProject (target, "libc++.dylib", false);

			CopyDirectory( lgSourceDir, lgDestDir );
			CopyDirectory(lgSourceDir + "/LGCoreLite.framework/Resources", lgDestDir + "/Resources" ); 

			//setup project structure
			AddDirectoryToXcodeproj (proj, target, path,  projDestRelPath); 

			//set up build setting
			proj.SetBuildProperty (target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
			//string[] oldVals = {"$(PROJECT_DIR)/" + projDestRelPath};
			//string[] newVals = {"$(PROJECT_DIR)/" + projDestRelPath};
			proj.AddBuildProperty (target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/" + projDestRelPath);
			string[] linkerFlagsToAdd = {"-ObjC", "-force_load $(PROJECT_DIR)/" + projDestRelPath + "/LGCoreLite.framework/LGCoreLite"};
			proj.UpdateBuildProperty (target, "OTHER_LDFLAGS", linkerFlagsToAdd, null);


			File.WriteAllText (projectPath, proj.WriteToString ());

			//setup plists
			UpdateInfoPlist(path);

			//LineAdapter.plist
			var lineAdapterPlist = PlistBuddyHelper.ForPlistFile (Path.Combine (path, projDestRelPath + "/Resources/Plist/LineAdapter.plist"));
			lineAdapterPlist.RemoveEntry ("ChannelId");
			lineAdapterPlist.AddString ("ChannelId", LGCoreLiteSetting.GetGlobalSetting().ChannelID);

		}

		UnityEngine.Debug.LogWarning( "PostProcessBuild LGCoreLite Ends" );
	}


	internal static void CopyDirectory( string srcPath, string dstPath )
	{
		if( ! Directory.Exists(dstPath) ) {
			Directory.CreateDirectory( dstPath );
		}
		
		foreach( var file in Directory.GetFiles(srcPath) ) {
			File.Copy( file, Path.Combine(dstPath, Path.GetFileName(file)), true );
		}
		
		foreach( var dir in Directory.GetDirectories(srcPath) ) {
			CopyDirectory( dir, Path.Combine(dstPath, Path.GetFileName(dir)) );
		}
	}

	internal static void AddDirectoryToXcodeproj( PBXProject proj, string targetGuid, string projectPath, string path )
	{
		string fullPath = Path.Combine( projectPath, path );
		
		if( Directory.Exists(fullPath) ) 
		{
			foreach( var file in Directory.GetFiles(fullPath) ) {
				if( file.EndsWith("Info.plist")) continue;
				if( file.EndsWith(".meta") ) continue;

				string filePath = Path.Combine( path, Path.GetFileName(file) );
				UnityEngine.Debug.Log( "Adding to xcodeproj: " + filePath );
				proj.AddFileToBuild( targetGuid, proj.AddFile(filePath, filePath, PBXSourceTree.Source) );
				
			}

			foreach( var dir in Directory.GetDirectories(fullPath) ) {
				string dirPath = Path.Combine( path, Path.GetFileName(dir) );
				UnityEngine.Debug.Log( "Adding to xcodeproj: " + dirPath );

				if(dirPath.EndsWith(".framework") || dirPath.EndsWith(".bundle") || dirPath.EndsWith(".plist") || dirPath.EndsWith(".lproj"))
				{
					proj.AddFileToBuild( targetGuid, proj.AddFile(dirPath, dirPath, PBXSourceTree.Source) );
				}
				else
					AddDirectoryToXcodeproj(proj, targetGuid, projectPath, dirPath);
			}
		}
		else {
			UnityEngine.Debug.LogWarning( "Directory doesn't exist: " + fullPath );
		}
	}

	internal static void UpdateInfoPlist( string path )
	{
		var plist = PlistBuddyHelper.ForPlistFile( Path.Combine(path, "Info.plist") );
		
		// Bundle Identifier
//		plist.SetString( "CFBundleIdentifier", bundleId );
		plist.RemoveEntry( "CFBundleIdentifier" );
		plist.AddString( "CFBundleIdentifier", LGCoreLiteSetting.GetGlobalSetting().BundleID );
		
		// Add URL-scheme
		//
		// <key>CFBundleURLTypes</key>
		// <array>
		//   <dict>
		// 		<key>CFBundleTypeRole</key>
		//		<string>Editor</string>
		//		<key>CFBundleURLName</key>
		//		<string>com.linecorp.LGSDKTEST</string>
		//		<key>CFBundleURLSchemes</key>
		//		<array>
		//			<string>line3rdp.com.linecorp.LGSDKTEST</string>
		//		</array>
		//   </dict>
		// </array>x
		plist.RemoveEntry("CFBundleURLTypes");
		plist.AddArray( "CFBundleURLTypes" );
		plist.AddDictionary( "CFBundleURLTypes", "0" );
		plist.AddString( "CFBundleURLTypes:0:CFBundleTypeRole", "Editor" );
		plist.AddString( "CFBundleURLTypes:0:CFBundleURLName", LGCoreLiteSetting.GetGlobalSetting().BundleID );
		plist.AddArray( "CFBundleURLTypes", "0", "CFBundleURLSchemes" );
		plist.AddString( "CFBundleURLTypes:0:CFBundleURLSchemes:0", "line3rdp." + LGCoreLiteSetting.GetGlobalSetting().BundleID );

		plist.AddArray("CFBundleLocalizations");
 		// Get Localization files
		const string PluginPath = "Assets/Plugins/iOS/";
		string lgSourceDir = PluginPath + "LGCoreLite";
		string lgBundle = lgSourceDir + "/Resources/Bundles/LGCoreLite.bundle";
		string[] lprojs = Directory.GetDirectories(lgBundle, "*.lproj");
		int i = 0;
		foreach (string lproj in lprojs) {
			string lang = Path.GetFileNameWithoutExtension(lproj);
			plist.AddString("CFBundleLocalizations:"+i, lang);
			i++;
		}

	}
	


		
}
