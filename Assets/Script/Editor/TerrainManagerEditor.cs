using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainManager))]
public class TerrainManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TerrainManager terrainManager = (TerrainManager)target;

        if (GUILayout.Button("TerrainGererate"))
        {
            terrainManager.GenerateTerrain();
        }
    }
}
