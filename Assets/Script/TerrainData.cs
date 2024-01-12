using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabData
{
    public GameObject prefab;
    public Vector3 prefabLocalPos;
    public float parallaxFactorX, parallaxFactorY;
    public int orderLayer;
}

[CreateAssetMenu(fileName = "TerrainData", menuName = "TerrainData")]
public class TerrainData : ScriptableObject
{
    public Vector3 terrainStartLocalPoint;
    public Vector3 terrainEndLocalPoint;

    public List<PrefabData> prefabDataList = new List<PrefabData>();
    
}
