using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierMeshGenerator))]
public class BezierMeshEditor : Editor
{
    private void OnSceneGUI()
    {
        BezierMeshGenerator bezierMeshGenerator = (BezierMeshGenerator)target;

        bezierMeshGenerator.p1 = Handles.PositionHandle(bezierMeshGenerator.p1, Quaternion.identity);
        bezierMeshGenerator.p2 = Handles.PositionHandle(bezierMeshGenerator.p2, Quaternion.identity);
        bezierMeshGenerator.p3 = Handles.PositionHandle(bezierMeshGenerator.p3, Quaternion.identity);
        bezierMeshGenerator.p4 = Handles.PositionHandle(bezierMeshGenerator.p4, Quaternion.identity);

        int count = 30;
        for(float i=0; i<count; i++)
        {
            float beforeIndex = i / count;
            Vector3 beforePoint = bezierMeshGenerator.bezierGenerator.BezierPoint(
                bezierMeshGenerator.p1, bezierMeshGenerator.p2, bezierMeshGenerator.p3, bezierMeshGenerator.p4, beforeIndex);

            float afterIndex =( i+1) / count;
            Vector3 afterPoint =  bezierMeshGenerator.bezierGenerator.BezierPoint(
                bezierMeshGenerator.p1, bezierMeshGenerator.p2, bezierMeshGenerator.p3, bezierMeshGenerator.p4, afterIndex);

            Handles.DrawLine(beforePoint, afterPoint);
        }
    }

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
