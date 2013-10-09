using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public static class AutoBuilder {

	static string GetProjectName()
	{
		string[] s = Application.dataPath.Split('/');
		return s[s.Length - 2];
	}

        static string APPNAME = "AndroidAngryBots";
	static string TARGET = "/tmp/workspace/android-angrybots-unity";
	static string[] GetScenePaths()
	{
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
		{
			if(scene.enabled) 
				EditorScenes.Add(scene.path);
		}
		EditorScenes.Add("Assets/Scenes/RobotLab.unity");
		
		return EditorScenes.ToArray();
	}
	
	[MenuItem("File/AutoBuilder/iOS")]
	static void PerformiOSBuild ()
	{
		PlayerSettings.productName= "iosBasic";
		PlayerSettings.bundleIdentifier = "com.ea.SimpleTextEditor";
		PlayerSettings.bundleVersion = "1.0";
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iPhone);
		string target_dir = "/tmp/workspace/iOS-angrybots-unity";
		string error = BuildPipeline.BuildPlayer(GetScenePaths(),target_dir,BuildTarget.iPhone,BuildOptions.None);
		if (error != null && error.Length > 0) {
            		throw new Exception("Build failed: " + error);
        	}
	}
	[MenuItem("File/AutoBuilder/Android")]
	static void PerformAndroidBuild ()
	{
		string target_dir = TARGET+"/"+APPNAME+".apk";
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
		string error = BuildPipeline.BuildPlayer(GetScenePaths(), target_dir, BuildTarget.Android, BuildOptions.None);
		if(error!=null && error.Length>0)
		{
			throw new Exception("Build failed: " + error);
		}
	}


}
