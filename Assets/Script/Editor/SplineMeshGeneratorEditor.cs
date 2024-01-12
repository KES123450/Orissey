using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplineMeshGenerator))]
public class SplineMeshGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SplineMeshGenerator splineMeshGenerator = (SplineMeshGenerator)target;
        if (GUILayout.Button("SplineGenerate"))
        {
            splineMeshGenerator.GenerateSplineList(splineMeshGenerator.p1, splineMeshGenerator.p2, splineMeshGenerator.p3, splineMeshGenerator.p4);
        }

        if (GUILayout.Button("SplineMeshGenerate"))
        {
            splineMeshGenerator.CreateSplineMesh(splineMeshGenerator.p1, splineMeshGenerator.p2, splineMeshGenerator.p3, splineMeshGenerator.p4);
        }
    }
}
