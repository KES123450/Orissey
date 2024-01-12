using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;


public class GeneratorMesh : MonoBehaviour
{
    private List<Vector3> vertiece = new List<Vector3>();
    private List<Vector3> sortVertiece = new List<Vector3>();
    private List<int> triagles = new List<int>();
    public Material groundMaterial;
    public Material groundOutlineMaterial;
    PolygonCollider2D point;


    public void generateMesh(GameObject ground,string meshName)
    {
        ground.AddComponent<MeshFilter>();
        ground.AddComponent<MeshRenderer>();
        PolygonCollider2D point = ground.GetComponent<PolygonCollider2D>();

        Vector3 tmp = new Vector3();
        for (int i = 0; i < point.GetTotalPointCount(); i++)
        {
            tmp.x = point.points[i].x;
            tmp.y = point.points[i].y;
            vertiece.Add(tmp);
        }

        sortVertiece = vertiece.OrderBy(x => x.x).ToList();
        Vector3 center = new Vector3(((sortVertiece[0].x + sortVertiece[point.GetTotalPointCount() - 1].x) / 2), sortVertiece[0].y, 0);

        sortVertiece.Insert(0, center);


        triagles.Clear();
        for (int i = 1; i < sortVertiece.Count-1; i++)
        {
            triagles.Add(0);
            triagles.Add(i);
            triagles.Add(i + 1);
        }
        

        Mesh mesh = new Mesh();
        mesh.name = meshName;
        mesh.vertices = sortVertiece.ToArray();
        mesh.triangles = triagles.ToArray();

        AssetDatabase.CreateAsset(mesh, "Assets/Prefab/Terrain/" + meshName + ".asset");// 새로추가
        AssetDatabase.SaveAssets();

        ground.GetComponent<MeshFilter>().mesh = mesh;
        var materialArray = new Material[] { groundMaterial, groundOutlineMaterial };
        ground.GetComponent<MeshRenderer>().materials = materialArray;
        ground.layer = 6;

        vertiece.Clear();
        sortVertiece.Clear();
        triagles.Clear();
    }

    public void generateMesh(GameObject ground)
    {
        ground.AddComponent<MeshFilter>();
        ground.AddComponent<MeshRenderer>();
        PolygonCollider2D point = ground.GetComponent<PolygonCollider2D>();

        foreach(Vector2 p in point.points)
        {
            Debug.Log(p);
        }

        Vector3 tmp = new Vector3();
        for (int i = 0; i < point.GetTotalPointCount(); i++)
        {
            tmp.x = point.points[i].x;
            tmp.y = point.points[i].y;
            vertiece.Add(tmp);
        }

        sortVertiece = vertiece.OrderBy(x => x.x).ToList();
        Vector3 center = new Vector3(((sortVertiece[0].x + sortVertiece[point.GetTotalPointCount() - 1].x) / 2), sortVertiece[0].y, 0);

        sortVertiece.Insert(0, center);


        triagles.Clear();
        for (int i = 1; i < sortVertiece.Count - 1; i++)
        {
            triagles.Add(0);
            triagles.Add(i);
            triagles.Add(i + 1);
        }


        Mesh mesh = new Mesh();
        mesh.vertices = sortVertiece.ToArray();
        mesh.triangles = triagles.ToArray();

        ground.GetComponent<MeshFilter>().mesh = mesh;
        var materialArray = new Material[] { groundMaterial, groundOutlineMaterial };
        ground.GetComponent<MeshRenderer>().materials = materialArray;
        ground.layer = 6;

        vertiece.Clear();
        sortVertiece.Clear();
        triagles.Clear();
    }
}
