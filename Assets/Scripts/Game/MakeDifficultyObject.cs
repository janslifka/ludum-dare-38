using UnityEngine;
using UnityEditor;

public static class MakeDifficultyObject
{
	[MenuItem("Assets/Create/Difficulty Object")]
	public static void CreateDifficultyObject()
	{
		DifficultyObject asset = ScriptableObject.CreateInstance<DifficultyObject>();
		AssetDatabase.CreateAsset(asset, AssetDatabase.GetAssetPath(Selection.activeObject) + "/NewDifficulty.asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}

}
