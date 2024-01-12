using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierGenerator : MonoBehaviour
{
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;
    public GameObject pointOBJ;

    public List<Vector3> bezierList = new List<Vector3>();
    public Vector3 BezierPoint(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        Vector3 A = Vector3.Lerp(p1, p2, t);
        Vector3 B = Vector3.Lerp(p2, p3, t);
        Vector3 C = Vector3.Lerp(p3, p4, t);

        Vector3 D = Vector3.Lerp(A, B, t);
        Vector3 E = Vector3.Lerp(B, C, t);

        Vector3 F = Vector3.Lerp(D, E, t);
        return F;
    }

    public Vector2[] GenerateBezierList(
        Vector3 p1,
        Vector3 p2,
        Vector3 p3,
        Vector3 p4,
        int pointNum)
    {
        Vector2[] bezierPoints= new Vector2[pointNum+2];

        float interval = (float)1 / pointNum;

        bezierPoints[0] = new Vector2(p1.x, p1.y - 200f);
        for (int i = 1; i < pointNum+1; i++)
        {
            Vector3 point=BezierPoint(p1, p2, p3, p4, interval*i);
            bezierPoints[i] = new Vector2(point.x, point.y);
        }
        
        bezierPoints[pointNum+1] = new Vector2(p4.x, p4.y - 200f);

        return bezierPoints;
    }
}
