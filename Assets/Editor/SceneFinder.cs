using UnityEngine;
using UnityEditor;
using System.IO;

public class SceneFinder
{
    [MenuItem("Tools/Find All Scenes in Project")]
    public static void FindScenes()
    {
        string[] guids = AssetDatabase.FindAssets("t:Scene");
        Debug.Log("------ SCENES FOUND ------");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log(path);
        }

        Debug.Log("------ END ------");
    }
}
