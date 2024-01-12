using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLocalLog : MonoBehaviour
{
    public PolygonCollider2D startTerrain;
    public PolygonCollider2D endTerrain;

    public void StartPointLog()
    {
        Vector3 startPoint = new Vector3(startTerrain.points[1].x, startTerrain.points[1].y, 0);
        Vector3 startPointToWorld = startTerrain.transform.TransformPoint(startPoint);
        Debug.Log("StartPoint : " + startPointToWorld.ToString("N3"));
    }
    public void EndPointLog()
    {
        int pointNum = endTerrain.points.Length;
        Vector3 endPoint = new Vector3(endTerrain.points[pointNum-2].x, endTerrain.points[pointNum-2].y, 0);
        Debug.Log("EndPoint : " + endTerrain.transform.TransformPoint(endPoint).ToString("N3"));
    }
}
