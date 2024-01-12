using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public ObstacleGenerator obstacleGenerator;
    public TerrainPool terrainPool;

    public List<TerrainData> terrainDatas = new List<TerrainData>();
    public Vector3 previousTerrainEndPoint=Vector3.zero; // endPoint알아내기용
    private float time;

    void Start()
    {
        TerrainGenerator();
    }

    public void TerrainGenerator()
    {
        TerrainType selectedTerrain = (TerrainType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(TerrainType)).Length);


        switch (selectedTerrain)
        {
            case TerrainType.Plane1:
                {
                    Vector3 terrainPoint = previousTerrainEndPoint - terrainDatas[0].terrainStartLocalPoint;
                    Terrain Plane1 = terrainPool.GetTerrain(selectedTerrain);
                    Plane1.transform.position = terrainPoint;
                    previousTerrainEndPoint = Plane1.transform.TransformPoint(terrainDatas[0].terrainEndLocalPoint);
                }
                break;

            case TerrainType.Plane2:
                {
                    Vector3 terrainPoint = previousTerrainEndPoint - terrainDatas[1].terrainStartLocalPoint;
                    Terrain Plane2 = terrainPool.GetTerrain(selectedTerrain);
                    Plane2.transform.position = terrainPoint;
                    previousTerrainEndPoint = Plane2.transform.TransformPoint(terrainDatas[1].terrainEndLocalPoint);
                }
                break;

            case TerrainType.Steep1:
                {
                    Vector3 terrainPoint = previousTerrainEndPoint - terrainDatas[2].terrainStartLocalPoint;
                    Terrain Steep1 = terrainPool.GetTerrain(selectedTerrain);
                    Steep1.transform.position = terrainPoint;
                    previousTerrainEndPoint = Steep1.transform.TransformPoint(terrainDatas[2].terrainEndLocalPoint);
                }
                break;
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
