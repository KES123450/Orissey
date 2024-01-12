using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainCurve))]
public class TerrainCurveEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        

        if (GUILayout.Button("TerrainCurveGererate"))
        {
            generateAnimationCurve();
        }
    }

    private void generateAnimationCurve()
    {
        TerrainCurve terrainCurve = (TerrainCurve)target;

        int terrainNum = terrainCurve.transform.childCount;
        Dictionary<int, List<Vector2>> curveDictionary = new Dictionary<int, List<Vector2>>();
        List<Vector2> curvePointList = new List<Vector2>();

        for (int i = 0; i < terrainNum; i++)
        {
            List<Vector2> curveList = new List<Vector2>(terrainCurve.transform.GetChild(i).GetComponent<PolygonCollider2D>().points);
            curveList.RemoveAt(0);
            curveList.RemoveAt(curveList.Count - 1);
            curveDictionary[i] = curveList;
        }

        Vector2 pointOffset = new Vector2(0, 0);
        pointOffset.y -= curveDictionary[0].First().y;

        for (int i=0; i < terrainNum; i++)
        {
            foreach(Vector2 point in curveDictionary[i])
            {
                curvePointList.Add(point + pointOffset);
            }

            if (i == terrainNum - 1)
                break;

            pointOffset= curvePointList.Last();
            pointOffset.y -= curveDictionary[i+1].First().y;
        }

        AnimationCurve animationTerrainCurve = new AnimationCurve();

        foreach(Vector2 point in curvePointList)
        {
            Keyframe keyframe = new Keyframe(point.x, point.y);
            animationTerrainCurve.AddKey(keyframe);
        }

        terrainCurve.animationTerrainCurve = animationTerrainCurve;
    }
}
