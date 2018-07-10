using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;
using System.IO;
using System;
using System.Linq;
using System.Diagnostics;

namespace LGCUnity.Editor
{

	internal sealed class PlistBuddyHelper
	{
		private readonly string mPlistPath;
		private const string PlistBuddyPath = "/usr/libexec/PlistBuddy";
		
		private PlistBuddyHelper(string plistPath) {
			mPlistPath = plistPath;
		}
		
		internal static PlistBuddyHelper ForPlistFile(string filepath) {
			if (!File.Exists(filepath)) {
				throw new ArgumentException("File " + filepath + " did not exist!");
			}
			
			if (!File.Exists(PlistBuddyPath)) {
				throw new InvalidOperationException("Could not locate an installation of PlistBuddy!");
			}
			
			return new PlistBuddyHelper(filepath);
		}
		
		internal bool AddArray(params object[] fieldPath) {
			return ExecuteCommand("Add " + ToEntryName(fieldPath) + " array") != null;
		}
		
		internal bool AddDictionary(params object[] fieldPath) {
			return ExecuteCommand("Add " + ToEntryName(fieldPath) + " dict") != null;
		}
		
		internal bool AddString(string fieldPath, string stringValue) {
			return ExecuteCommand("Add " + fieldPath + " string " + stringValue) != null;
		}
		internal bool SetString(string fieldPath, string stringValue) {
			return ExecuteCommand("Set " + fieldPath + " " + stringValue) != null;
		}
		
		internal bool AddBool(string fieldPath, bool boolValue) {
			return ExecuteCommand("Add " + fieldPath + " bool " + (boolValue ? "true" : "false")) != null;
		}
		internal bool SetBool(string fieldPath, bool boolValue) {
			return ExecuteCommand("Set " + fieldPath + " " + (boolValue ? "true" : "false")) != null;
		}
		
		internal void RemoveEntry(params object[] fieldPath) {
			ExecuteCommand("Delete " + ToEntryName(fieldPath));
		}
		
		internal string EntryValue(params object[] fieldPath) {
			var value = ExecuteCommand("print " + ToEntryName(fieldPath));
			
			// Plistbuddy adds a trailing newline onto the output - strip it here.
			if (value != null) {
				return value.Replace("\n", "");
			}
			
			return null;
		}
		
		private string ExecuteCommand(string command) {
			using (var process = new Process()) {
				process.StartInfo.FileName = "/usr/libexec/PlistBuddy";
				process.StartInfo.Arguments = string.Format("-c \"{0}\" \"{1}\"", command, mPlistPath);
//				UnityEngine.Debug.Log("Executing PlistBuddy command: " + process.StartInfo.Arguments);

				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.RedirectStandardOutput = true;
				
				try {
					process.Start();
					process.StandardError.ReadToEnd();
					var stdOutput = process.StandardOutput.ReadToEnd();
//					var stdError = process.StandardError.ReadToEnd();

//					UnityEngine.Debug.Log("Plistbuddy stdout: " + stdOutput);
//					UnityEngine.Debug.Log("Plistbuddy stderr: " + stdError);
					
					if (!process.WaitForExit(10 * 1000)) {
						throw new Exception("PlistBuddy did not exit in a timely fashion");
					}
					
					if (process.ExitCode != 0) {
						return null;
					}
					
					return stdOutput.Replace("\n", "").Trim();
				} catch (Exception e) {
					throw new Exception("Encountered unexpected error while editing Info.plist.", e);
				}
			}
		}
		
		public static string ToEntryName(params object[] fields) {
			return string.Join(":", fields.Select(o => o.ToString()).ToArray());
		}
	}

}