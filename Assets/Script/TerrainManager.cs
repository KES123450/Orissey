using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public TerrainPool terrainPool;

    private Dictionary<TerrainType, GameObject> terrainDatas = new();
    private Queue<GameObject> terrainQueue = new();
    private Vector3 previousTerrainEndPoint=Vector3.zero; // endPoint알아내기용
    private TerrainData previousTerrainData;
    [SerializeField] private Vector3 terrainOffset;
    [SerializeField] private BezierMeshGenerator bezierMeshGenerator;
    private float time;

    void Start()
    {
        InitTerrainData();

        TerrainType selectedTerrainType = (TerrainType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(TerrainType)).Length);
        Vector3 nextTerrainPos = new Vector3(0, -500f, 0);
        GameObject nextTerrain = Instantiate(terrainDatas[selectedTerrainType], nextTerrainPos, Quaternion.identity);
        TerrainData nextTerrainData = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).GetComponent<TerrainData>();

        Vector2[] terrainPoints = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).GetComponent<PolygonCollider2D>().points;
        previousTerrainEndPoint = nextTerrain.transform.TransformDirection(nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).TransformPoint(terrainPoints[terrainPoints.Length - 2]));
        Debug.Log(previousTerrainEndPoint);
        previousTerrainData = nextTerrainData;
    }

    private void InitTerrainData()
    {
        terrainDatas[TerrainType.Terrain1] = Resources.Load("Prefab/Terrain/Terrain/Terrain1") as GameObject;
        terrainDatas[TerrainType.Terrain2] = Resources.Load("Prefab/Terrain/Terrain/Terrain2") as GameObject;
        terrainDatas[TerrainType.Terrain3] = Resources.Load("Prefab/Terrain/Terrain/Terrain3") as GameObject;

        Debug.Log(terrainDatas[TerrainType.Terrain1]);
        
    }

    public void TerrainGenerator()
    {
        //다음 지형 생성
        TerrainType selectedTerrainType = (TerrainType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(TerrainType)).Length);
        Vector3 nextTerrainPos = previousTerrainEndPoint + terrainOffset;
        GameObject nextTerrain= Instantiate(terrainDatas[selectedTerrainType], nextTerrainPos, Quaternion.identity);
        TerrainData nextTerrainData = nextTerrain.transform.GetChild(nextTerrain.transform.childCount-1).GetComponent<TerrainData>();

        //이어주는 지형 생성
        GameObject terrainConnector = bezierMeshGenerator.CreateBezierMesh(
            previousTerrainData.cp3,previousTerrainData.cp4- previousTerrainData.cp3,
            nextTerrainData.cp1-nextTerrainData.cp2, nextTerrainData.cp1);


        terrainQueue.Enqueue(terrainConnector);
        terrainQueue.Enqueue(nextTerrain);


        Vector2[] terrainPoints = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).GetComponent<PolygonCollider2D>().points;
        previousTerrainEndPoint = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).TransformPoint(terrainPoints[terrainPoints.Length-2]);
        previousTerrainData = nextTerrainData;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >=5f)
        {
            if (terrainQueue.Count >= 10)
            {
                terrainQueue.Dequeue();
                terrainQueue.Dequeue();
            }
            TerrainGenerator();
            time = 0;
        }
    }

}
