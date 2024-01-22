using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public ObstacleGenerator obstacleGenerator;
    public TerrainPool terrainPool;

    //public List<TerrainData> terrainDatas = new List<TerrainData>();
    private Dictionary<TerrainType, GameObject> terrainDatas = new();
    private Queue<GameObject> terrainQueue = new();
    private Vector3 previousTerrainEndPoint=Vector3.zero; // endPoint알아내기용
    [SerializeField] private Vector3 terrainOffset;
    private float time;

    void Start()
    {
        TerrainGenerator();
    }
    private void InitTerrainData()
    {
        terrainDatas[TerrainType.Terrain1] = Resources.Load("Prefab/Terrain/Terrain1") as GameObject;
    }

    public void TerrainGenerator()
    {
        TerrainType selectedTerrain = (TerrainType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(TerrainType)).Length);
        Vector3 nextTerrainPos = previousTerrainEndPoint + terrainOffset;


        switch (selectedTerrain)
        {
            /*case TerrainType.Plane1:
                {
                    //Vector3 terrainPoint = previousTerrainEndPoint - terrainDatas[0].terrainStartLocalPoint;
                    Terrain Plane1 = terrainPool.GetTerrain(selectedTerrain);
                   // Plane1.transform.position = terrainPoint;
                    //previousTerrainEndPoint = Plane1.transform.TransformPoint(terrainDatas[0].terrainEndLocalPoint);
                }
                break;*/
        }
        
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >=5f)
        {
            TerrainGenerator();
            time = 0;
        }
    }

}
