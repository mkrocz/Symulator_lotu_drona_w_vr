using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// Editor window tool for converting Unity Terrain trees into individual GameObjects.
// Creates a parent group for all generated trees and removes them from the TerrainData.
public class TreeSwitchEditor : EditorWindow
{
    private Terrain terrain;
    private GameObject treePrefab;
    private string groupName = "Generated_Trees";

    [MenuItem("Tools/Tree Switcher")]
    public static void ShowWindow()
    {
        GetWindow<TreeSwitchEditor>("Tree Switcher");
    }

    void OnGUI()
    {
        GUILayout.Label("Zamiana drzew z Terrain na GameObjecty", EditorStyles.boldLabel);

        terrain = (Terrain)EditorGUILayout.ObjectField("Terrain:", terrain, typeof(Terrain), true);
        treePrefab = (GameObject)EditorGUILayout.ObjectField("Prefab drzewa:", treePrefab, typeof(GameObject), false);
        groupName = EditorGUILayout.TextField("Nazwa grupy:", groupName);

        if (GUILayout.Button("Zamień drzewa na obiekty"))
        {
            if (terrain == null)
                terrain = Terrain.activeTerrain;

            if (terrain == null || treePrefab == null)
            {
                EditorUtility.DisplayDialog("Uwaga", "Brakuje referencji: teren lub prefab drzewa", "OK");
                return;
            }

            ConvertTrees();
        }
    }

    private void ConvertTrees()
    {
        TerrainData data = terrain.terrainData;
        TreeInstance[] trees = data.treeInstances;

        if (trees == null || trees.Length == 0)
        {
            EditorUtility.DisplayDialog("Brak drzew", "Na tym terenie nie ma żadnych drzew do zamiany.", "OK");
            return;
        }

        GameObject parentGroup = new GameObject(groupName);
        parentGroup.transform.position = terrain.transform.position;

        Undo.RegisterCreatedObjectUndo(parentGroup, "Create Tree Group");

        int count = 0;

        foreach (TreeInstance tree in trees)
        {
            Vector3 worldPos = Vector3.Scale(tree.position, data.size) + terrain.transform.position;
            worldPos.y = terrain.SampleHeight(worldPos) + terrain.transform.position.y;
            Quaternion rotation = Quaternion.Euler(0f, tree.rotation * Mathf.Rad2Deg, 0f);

            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(treePrefab);
            obj.transform.position = worldPos;
            obj.transform.rotation = rotation;
            obj.transform.SetParent(parentGroup.transform);

            Undo.RegisterCreatedObjectUndo(obj, "Create Tree Object");
            count++;
        }

        data.treeInstances = new TreeInstance[0];
        data.treePrototypes = new TreePrototype[0];

        EditorUtility.SetDirty(terrain);
        EditorUtility.SetDirty(terrain.terrainData);
        AssetDatabase.SaveAssets();

        terrain.Flush();

        EditorUtility.DisplayDialog("Zakończono",
            $"Zamieniono {count} drzew na GameObjecty i usunięto je z TerrainData.\n" +
            $"Nowe obiekty są w grupie \"{groupName}\".",
            "OK");
    }
}
