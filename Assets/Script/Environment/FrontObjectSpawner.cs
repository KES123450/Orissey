using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontObjectSpawner : MonoBehaviour
{
    private Queue<GameObject> frontObjectQueue = new();
    private Dictionary<FrontObjectType, GameObject> ObjectDatas = new();
    private float playerMovementByX;
    private Vector2[] terrainPoints;
    private GameObject terrain;
    private int prevObjectIdx;
    private Transform frontLayer;
    [SerializeField] private float objectSpaceX;
    [SerializeField] private float objectOffsetY; 

    public void Init()
    {
        frontLayer = GameObject.Find("FrontLayer").transform;
    }
    

    private void SpawnFrontObject()
    {
        terrain= GameManager.Instance.Player.PlayerOnTerrain;
        terrainPoints = terrain.GetComponent<PolygonCollider2D>().points;
        FrontObjectType selectedObjectType = (FrontObjectType)Random.Range(0, System.Enum.GetValues(typeof(FrontObjectType)).Length);
        GameObject frontObject = Instantiate(ObjectDatas[selectedObjectType], FindObjectSpawnPos(), Quaternion.identity, frontLayer);
        frontObjectQueue.Enqueue(frontObject);
    }

    private Vector3 FindObjectSpawnPos()
    {
        float nextObjectPosX = terrainPoints[prevObjectIdx].x + objectSpaceX;
        for(int i= prevObjectIdx; i < terrainPoints.Length; i++)
        {
            if(nextObjectPosX - terrainPoints[i].x < 0)
            {
                prevObjectIdx = i;
                return new Vector3(terrainPoints[i].x, terrainPoints[i].y - objectOffsetY, 0);
            }
        }
        prevObjectIdx = 0;
        return new Vector3(terrainPoints[terrainPoints.Length - 1].x, terrainPoints[terrainPoints.Length - 1].y - objectOffsetY, 0);
    }
}
