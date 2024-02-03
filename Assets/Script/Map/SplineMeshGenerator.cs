using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMeshGenerator : MonoBehaviour
{

    public GeneratorMesh generatorMesh;

    public GameObject pivot;
    public Transform pivotParent;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;

    public int pointNum;

    private Vector2 Interpolate(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
      
        Vector2 position = 0.5f * ((2 * p2) +
            (-p1 + p3) * t +
            (2 * p1 - 5 * p2 + 4 * p3 - p4) * (t * t) +
            (-p1 + 3 * p2 - 3 * p3 + p4) * (t * t * t));
        
        return position;
    }

    public Vector2[] GenerateSplineList(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        Vector2[] pointList = new Vector2[pointNum+2];

        pointList[0] = new Vector2(p2.x, p2.y - 200f);
        p1.y = p1.y *1.5f;
        p4.y = p4.y * 6f;
        for (int i = 1; i < pointNum+1; i++)
        {
            float t = (float) (i-1) / pointNum;
            pointList[i] = Interpolate(p1, p2, p3, p4, t);
        }
        pointList[pointNum-1] = Interpolate(p1, p2, p3, p4, (float)(pointNum-1)/pointNum);
        pointList[pointNum] = Interpolate(p1, p2, p3, p4, 1);
        pointList[pointNum + 1] = new Vector2(p3.x, p3.y - 200f);

        foreach(Vector2 p in pointList)
        {
            GameObject point = Instantiate(pivot, p, Quaternion.identity);
            point.transform.parent = pivotParent;
        }
        return pointList;
    }

    public GameObject CreateSplineMesh(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        GameObject ground = new GameObject();
        PolygonCollider2D polygonCollider2D = new PolygonCollider2D();
        
        polygonCollider2D = ground.AddComponent<PolygonCollider2D>();
        Vector2[] splinePoints = GenerateSplineList(p1, p2, p3, p4);
        polygonCollider2D.points = splinePoints;

        generatorMesh.generateMesh(ground);

        return ground;
    }
}
