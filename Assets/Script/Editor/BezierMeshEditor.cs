using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierMeshGenerator))]
public class BezierMeshEditor : Editor
{
    private Collider2D prevTerrain;
    private Collider2D nextTerrain;
    private BezierMeshGenerator bezierMeshGenerator;
    private bool checkSnap;
    private void OnSceneGUI()
    {
        bezierMeshGenerator = (BezierMeshGenerator)target;

        bezierMeshGenerator.p1 = Handles.PositionHandle(bezierMeshGenerator.p1, Quaternion.identity);
        Handles.Label(bezierMeshGenerator.p1, "p1");
        bezierMeshGenerator.p2 = Handles.PositionHandle(bezierMeshGenerator.p2, Quaternion.identity);
        Handles.Label(bezierMeshGenerator.p2, "p2");
        bezierMeshGenerator.p3 = Handles.PositionHandle(bezierMeshGenerator.p3, Quaternion.identity);
        Handles.Label(bezierMeshGenerator.p3, "p3");
        bezierMeshGenerator.p4 = Handles.PositionHandle(bezierMeshGenerator.p4, Quaternion.identity);
        Handles.Label(bezierMeshGenerator.p4, "p4");


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

        if (!checkSnap)
            return;

        Collider2D terrain1 = Physics2D.OverlapCircle(bezierMeshGenerator.p1, 1f);
        prevTerrain = terrain1;
        if (terrain1 != null)
        {
            EdgeCollider2D collider = terrain1.GetComponent<EdgeCollider2D>();
            bezierMeshGenerator.p1 = collider.transform.TransformPoint(collider.points[collider.points.Length - 2]);
        }

        Collider2D terrain2 = Physics2D.OverlapCircle(bezierMeshGenerator.p4, 1f);
        nextTerrain = terrain2;
        if (terrain2 != null)
        {
            EdgeCollider2D collider = terrain2.GetComponent<EdgeCollider2D>();
            bezierMeshGenerator.p4 = collider.transform.TransformPoint(collider.points[1]);
        }
    }

    private void SetP2Pos()
    {
        TerrainData data = prevTerrain.GetComponent<TerrainData>();
        Vector3 offset = data.cp4 - data.cp3;
        bezierMeshGenerator.p2 = bezierMeshGenerator.p1 + offset;
    }

    private void SetP3Pos()
    {
        TerrainData data = nextTerrain.GetComponent<TerrainData>();
        Vector3 offset = data.cp1 - data.cp2;
        bezierMeshGenerator.p3 = bezierMeshGenerator.p4 + offset;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        checkSnap = GUILayout.Toggle(checkSnap, "checkSnap");

        if (GUILayout.Button("MeshGenerate"))
        {
            bezierMeshGenerator.CreateBezierMesh();
        }

        if (GUILayout.Button("SmoothP2"))
        {
            SetP2Pos();
        }

        if (GUILayout.Button("SmoothP3"))
        {
            SetP3Pos();
        }
    }
}
