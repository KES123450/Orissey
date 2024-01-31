using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMeshGenerator : MonoBehaviour
{
    public BezierGenerator bezierGenerator;
    public GeneratorMesh generatorMesh;
    public List<Vector3> bezierPointList;

    public Vector3 p1 { get; set; }
    public Vector3 p2 { get; set; }
    public Vector3 p3 { get; set; }
    public Vector3 p4 { get; set; }
    public int pointNum;
    public string meshName;

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

    public GameObject CreateBezierMesh(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        GameObject ground = new GameObject();
        PolygonCollider2D polygonCollider2D = new PolygonCollider2D();

        Vector2[] bezierPoints = bezierGenerator.GenerateBezierList(p1, p2, p3, p4, pointNum);
        polygonCollider2D = ground.AddComponent<PolygonCollider2D>();
        polygonCollider2D.points = bezierPoints;

        generatorMesh.generateMesh(ground);
        ground.AddComponent<TerrainData>();
        ground.GetComponent<TerrainData>().Init(p1, p2, p3, p4);
        return ground;
    }

}
