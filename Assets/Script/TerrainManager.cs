using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public TerrainPool terrainPool;

    private Dictionary<TerrainType, GameObject> terrainDatas = new();
    private Queue<GameObject> terrainQueue = new();
    private Vector3 previousTerrainEndPoint=Vector3.zero; // endPoint알아내기용
    private GameObject previousTerrain;
    [SerializeField] private Vector3 terrainOffset;
    [SerializeField] private BezierMeshGenerator bezierMeshGenerator;
    private float time;

    void Start()
    {
        InitTerrainData();

        TerrainType selectedTerrainType = (TerrainType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(TerrainType)).Length);
        Vector3 nextTerrainPos = new Vector3(0, -500f, 0);
        GameObject nextTerrain = Instantiate(terrainDatas[selectedTerrainType], nextTerrainPos, Quaternion.identity);

        Vector2[] terrainPoints = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).GetComponent<PolygonCollider2D>().points;
        previousTerrainEndPoint = nextTerrain.transform.TransformDirection(nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).TransformPoint(terrainPoints[terrainPoints.Length - 2]));
        Debug.Log(previousTerrainEndPoint);
        previousTerrain = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).gameObject;
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
        TerrainData nextTerrainData = nextTerrain.transform.GetChild(0).GetComponent<TerrainData>();

        //이어주는 지형 생성
        TerrainData previousTerrainData = previousTerrain.GetComponent<TerrainData>();
        Vector3 previousCP3ToWorld = previousTerrain.transform.TransformPoint(previousTerrainData.cp3);
        Vector3 previousCP4ToWorld = previousTerrain.transform.TransformPoint(previousTerrainData.cp4);
        Vector3 nextCP1ToWorld = nextTerrain.transform.GetChild(0).TransformPoint(nextTerrainData.cp1);
        Vector3 nextCP2ToWorld = nextTerrain.transform.GetChild(0).TransformPoint(nextTerrainData.cp2);

        GameObject terrainConnector = bezierMeshGenerator.CreateBezierMesh(
            previousCP4ToWorld, 2*previousCP4ToWorld - previousCP3ToWorld,
            2*nextCP1ToWorld - nextCP2ToWorld, nextCP1ToWorld);


        terrainQueue.Enqueue(terrainConnector);
        terrainQueue.Enqueue(nextTerrain);


        Vector2[] terrainPoints = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).GetComponent<PolygonCollider2D>().points;
        previousTerrainEndPoint = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).TransformPoint(terrainPoints[terrainPoints.Length-2]);
        previousTerrain = nextTerrain.transform.GetChild(nextTerrain.transform.childCount - 1).gameObject;
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
