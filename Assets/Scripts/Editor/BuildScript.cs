using UnityEngine;
using UnityEditor;

public class BuildScript : MonoBehaviour
{
	const string FILE_NAME = "A Small Carrot World";

	static string[] GetScenePaths()
	{
		string[] scenes = new string[EditorBuildSettings.scenes.Length];

		for (int i = 0; i < scenes.Length; i++) {
			scenes[i] = EditorBuildSettings.scenes[i].path;
		}

		return scenes;
	}

	[MenuItem("Build/OS X")]
	static void PerformOSXBuild()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneOSXUniversal);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Build/ascw-osx/" + FILE_NAME, BuildTarget.StandaloneOSXUniversal, BuildOptions.None);
	}

	[MenuItem("Build/Windows")]
	static void PerformWindowsBuild()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
		BuildPipeline.BuildPlayer(GetScenePaths(), "Build/ascw-win/" + FILE_NAME, BuildTarget.StandaloneWindows, BuildOptions.None);
	}

	[MenuItem("Build/Build All")]
	static void BuildAll()
	{
		PerformOSXBuild();
		PerformWindowsBuild();
	}
}
