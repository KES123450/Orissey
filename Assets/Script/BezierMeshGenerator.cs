using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMeshGenerator : MonoBehaviour
{
    public BezierGenerator bezierGenerator;
    public GeneratorMesh generatorMesh;
    public List<Vector3> bezierPointList;

    public Vector3 p1; //테스트용
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;
    public int pointNum;
    public string meshName;

    public bool smoothMode;
    public void CreateBezierMesh()
    {
        GameObject ground = new GameObject();
        PolygonCollider2D polygonCollider2D= new PolygonCollider2D();
        
        Vector2[] bezierPoints = bezierGenerator.GenerateBezierList(p1, p2, p3, p4,pointNum);
        polygonCollider2D= ground.AddComponent<PolygonCollider2D>();
        polygonCollider2D.points = bezierPoints;

        generatorMesh.generateMesh(ground,meshName);
        ground.AddComponent<TerrainData>();
        ground.GetComponent<TerrainData>().Init(p1, p2, p3, p4);
    }

}
