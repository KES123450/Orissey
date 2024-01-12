using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TerrainLocalLog))]
public class TerrainLocalLogEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TerrainLocalLog terrainLocalLog = (TerrainLocalLog)target;

        if (GUILayout.Button("StartLocalPoint"))
        {
            terrainLocalLog.StartPointLog();
        }

        if (GUILayout.Button("EndLocalPoint"))
        {
            terrainLocalLog.EndPointLog();
        }
    }
}
