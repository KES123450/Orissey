using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierMeshGenerator))]
public class BezierMeshGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BezierMeshGenerator bezierMeshGenerator = (BezierMeshGenerator)target;
        if (GUILayout.Button("MeshGenerate"))
        {
            bezierMeshGenerator.CreateBezierMesh();
        }
    }

}
